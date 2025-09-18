using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace socketUDP
{
    public partial class Form1 : Form
    {
        // Handle demandé par l'énoncé
        private Socket SSockUDP;

        public Form1()
        {
            InitializeComponent();
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool opened = SSockUDP != null;
            if (this.Controls.Count == 0) return; // designer-time safeguard
            // Enable/disable buttons based on socket state
            var btnCreate = this.Controls.Find("buttonCreate", true).FirstOrDefault() as Button;
            var btnClose = this.Controls.Find("buttonClose", true).FirstOrDefault() as Button;
            var btnSend = this.Controls.Find("buttonSend", true).FirstOrDefault() as Button;
            var btnReceive = this.Controls.Find("buttonReceive", true).FirstOrDefault() as Button;

            if (btnCreate != null) btnCreate.Enabled = !opened;
            if (btnClose != null) btnClose.Enabled = opened;
            if (btnSend != null) btnSend.Enabled = opened;
            if (btnReceive != null) btnReceive.Enabled = opened;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (SSockUDP != null)
                {
                    AppendRecvLine("Socket déjà créé.");
                    return;
                }

                var localIP = IPAddress.Parse(textBoxLocalIP.Text.Trim());
                int localPort = int.Parse(textBoxLocalPort.Text.Trim());
                IPEndPoint localEP = new IPEndPoint(localIP, localPort);

                SSockUDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                SSockUDP.ReceiveTimeout = 2000; // 2s timeout for blocking ReceiveFrom
                SSockUDP.Bind(localEP);

                AppendRecvLine($"Bind OK sur {localEP}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erreur création/bind", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SSockUDP = null;
            }
            finally
            {
                UpdateButtons();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (SSockUDP != null)
                {
                    SSockUDP.Close();
                    SSockUDP = null;
                    AppendRecvLine("Socket fermé.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erreur fermeture", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateButtons();
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (SSockUDP == null)
            {
                MessageBox.Show(this, "Créez et bindez d'abord la socket.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                var destIP = IPAddress.Parse(textBoxDestIP.Text.Trim());
                int destPort = int.Parse(textBoxDestPort.Text.Trim());
                EndPoint destEP = new IPEndPoint(destIP, destPort);

                byte[] msg = Encoding.ASCII.GetBytes(textBoxSend.Text ?? string.Empty);
                int sent = SSockUDP.SendTo(msg, destEP);
                AppendRecvLine($"Envoyé {sent} octets à {destEP}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erreur envoi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonReceive_Click(object sender, EventArgs e)
        {
            if (SSockUDP == null)
            {
                MessageBox.Show(this, "Créez et bindez d'abord la socket.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                byte[] buffer = new byte[1024];
                EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                int len = SSockUDP.ReceiveFrom(buffer, ref remoteEP);
                string txt = Encoding.ASCII.GetString(buffer, 0, len);
                AppendRecvLine($"De {remoteEP} -> {txt}");
            }
            catch (SocketException sx)
            {
                if (sx.SocketErrorCode == SocketError.TimedOut)
                {
                    AppendRecvLine("Réception: timeout.");
                }
                else
                {
                    MessageBox.Show(this, sx.Message, "Erreur réception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erreur réception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxReceive.Clear();
        }

        private void AppendRecvLine(string text)
        {
            textBoxReceive.AppendText(text + Environment.NewLine);
        }
    }
}

