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
        private Socket _socket;

        public Form1()
        {
            InitializeComponent();
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool opened = _socket != null;
            if (this.Controls.Count == 0) return; // designer-time safeguard
            // Enable/disable buttons based on socket state
            var btnCreate = this.Controls.Find("btnCreateBind", true).FirstOrDefault() as Button;
            var btnClose = this.Controls.Find("btnClose", true).FirstOrDefault() as Button;
            var btnSend = this.Controls.Find("btnSend", true).FirstOrDefault() as Button;
            var btnReceive = this.Controls.Find("btnReceive", true).FirstOrDefault() as Button;

            if (btnCreate != null) btnCreate.Enabled = !opened;
            if (btnClose != null) btnClose.Enabled = opened;
            if (btnSend != null) btnSend.Enabled = opened;
            if (btnReceive != null) btnReceive.Enabled = opened;
        }

        private void btnCreateBind_Click(object sender, EventArgs e)
        {
            try
            {
                if (_socket != null)
                {
                    AppendRecvLine("Socket déjà créé.");
                    return;
                }

                var localIP = IPAddress.Parse(txtLocalIP.Text.Trim());
                int localPort = int.Parse(txtLocalPort.Text.Trim());
                IPEndPoint localEP = new IPEndPoint(localIP, localPort);

                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _socket.ReceiveTimeout = 2000; // 2s timeout for blocking ReceiveFrom
                _socket.Bind(localEP);

                AppendRecvLine($"Bind OK sur {localEP}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erreur création/bind", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _socket = null;
            }
            finally
            {
                UpdateButtons();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (_socket != null)
                {
                    _socket.Close();
                    _socket = null;
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (_socket == null)
            {
                MessageBox.Show(this, "Créez et bindez d'abord la socket.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                var destIP = IPAddress.Parse(txtDestIP.Text.Trim());
                int destPort = int.Parse(txtDestPort.Text.Trim());
                EndPoint destEP = new IPEndPoint(destIP, destPort);

                byte[] msg = Encoding.ASCII.GetBytes(txtSend.Text ?? string.Empty);
                int sent = _socket.SendTo(msg, destEP);
                AppendRecvLine($"Envoyé {sent} octets à {destEP}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erreur envoi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            if (_socket == null)
            {
                MessageBox.Show(this, "Créez et bindez d'abord la socket.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                byte[] buffer = new byte[1024];
                EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                int len = _socket.ReceiveFrom(buffer, ref remoteEP);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtReceive.Clear();
        }

        private void AppendRecvLine(string text)
        {
            txtReceive.AppendText(text + Environment.NewLine);
        }
    }
}
