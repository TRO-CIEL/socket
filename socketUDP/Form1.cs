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
        private Socket SSockUDP;

        public Form1()
        {
            InitializeComponent();
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool opened = SSockUDP != null;
            if (this.Controls.Count == 0) return;
            var btnCreate = this.Controls.Find("buttonCreate", true).FirstOrDefault() as Button;
            var btnClose = this.Controls.Find("buttonClose", true).FirstOrDefault() as Button;
            var btnSend = this.Controls.Find("buttonSend", true).FirstOrDefault() as Button;
            var btnReceive = this.Controls.Find("buttonReceive", true).FirstOrDefault() as Button;
            var btnClear = this.Controls.Find("buttonClear", true).FirstOrDefault() as Button;
            var btnStartPolling = this.Controls.Find("buttonStartPolling", true).FirstOrDefault() as Button;
            var btnStopPolling = this.Controls.Find("buttonStopPolling", true).FirstOrDefault() as Button;

            if (btnCreate != null) btnCreate.Enabled = !opened;
            if (btnClose != null) btnClose.Enabled = opened;
            if (btnSend != null) btnSend.Enabled = opened;
            if (btnReceive != null) btnReceive.Enabled = opened;
            if (btnClear != null) btnClear.Enabled = true;
            
            // Gestion des boutons de scrutation
            bool pollingActive = timerPolling.Enabled;
            if (btnStartPolling != null) btnStartPolling.Enabled = opened && !pollingActive;
            if (btnStopPolling != null) btnStopPolling.Enabled = opened && pollingActive;
        }

        private void buttonStartPolling_Click(object sender, EventArgs e)
        {
            if (SSockUDP == null)
            {
                AppendRecvLine("Impossible de démarrer la scrutation : socket non créée.");
                return;
            }
            
            timerPolling.Start();
            AppendRecvLine("Scrutation démarrée (100ms).");
            UpdateButtons();
        }

        private void buttonStopPolling_Click(object sender, EventArgs e)
        {
            timerPolling.Stop();
            AppendRecvLine("Scrutation arrêtée.");
            UpdateButtons();
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
                SSockUDP.ReceiveTimeout = 2000;
                SSockUDP.Bind(localEP);

                AppendRecvLine($"Bind OK sur {localEP}");
            }
            catch (SocketException se)
            {
                AppendRecvLine("Message d'erreur (création/bind) : " + se.ToString());
                SSockUDP = null;
            }
            catch (Exception ex)
            {
                AppendRecvLine("Erreur inconnue (création/bind) : " + ex.Message);
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
                else
                {
                    AppendRecvLine("Socket déjà fermée ou non créée.");
                }
            }
            catch (SocketException se)
            {
                AppendRecvLine("Message d'erreur (fermeture) : " + se.ToString());
            }
            catch (Exception ex)
            {
                AppendRecvLine("Erreur inconnue (fermeture) : " + ex.Message);
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
                AppendRecvLine("Impossible d'envoyer : socket non créée/bindée.");
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

                // Avertissement si IPEr == IPEd
                try
                {
                    string localIpTxt = textBoxLocalIP.Text.Trim();
                    string localPortTxt = textBoxLocalPort.Text.Trim();
                    if (!string.IsNullOrEmpty(localIpTxt) && !string.IsNullOrEmpty(localPortTxt) &&
                        localIpTxt == textBoxDestIP.Text.Trim() && localPortTxt == textBoxDestPort.Text.Trim())
                    {
                        AppendRecvLine("Attention: le point de terminaison de réception (IPEr) est identique à la destination (IPEd). Envoi vers soi-même.");
                    }
                }
                catch { /* avertissement best-effort */ }
            }
            catch (SocketException se)
            {
                AppendRecvLine("Message d'erreur (envoi) : " + se.ToString());
            }
            catch (Exception ex)
            {
                AppendRecvLine("Erreur inconnue (envoi) : " + ex.Message);
            }
        }

        private void buttonReceive_Click(object sender, EventArgs e)
        {
            if (SSockUDP == null)
            {
                AppendRecvLine("Impossible de recevoir : socket non créée/bindée.");
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
                    AppendRecvLine("Message d'erreur (réception) : " + sx.ToString());
                }
            }
            catch (Exception ex)
            {
                AppendRecvLine("Erreur inconnue (réception) : " + ex.Message);
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

        // Gestion de l'événement TextChanged pour éviter les erreurs du Concepteur
        private void textBoxReceive_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxReceive.SelectionStart = textBoxReceive.TextLength;
                textBoxReceive.ScrollToCaret();
            }
            catch { }
        }

        // Scrutation périodique
        private void timerPolling_Tick(object sender, EventArgs e)
        {
            if (SSockUDP == null)
                return;

            try
            {
                // Vérifie données disponibles mémoire tampon
                if (SSockUDP.Available > 0)
                {
                    byte[] buffer = new byte[1024];
                    EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    int len = SSockUDP.ReceiveFrom(buffer, ref remoteEP);
                    string txt = Encoding.ASCII.GetString(buffer, 0, len);
                    AppendRecvLine($"[Auto] De {remoteEP} -> {txt}");
                }
            }
            catch (SocketException se)
            {
                AppendRecvLine("[Auto] Erreur réception : " + se.ToString());
            }
            catch (Exception ex)
            {
                AppendRecvLine("[Auto] Erreur inconnue : " + ex.Message);
            }
        }
    }
}

