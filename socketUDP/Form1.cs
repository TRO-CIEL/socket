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
        // Sockets TCP pour le serveur (écoute + client connecté)
        private Socket listenTcp;
        private Socket clientTcp;
        private IPEndPoint clientTcpEP;
        // Client TCP
        private Socket tcpClient;
        private Socket asyncListen;
        private System.Threading.CancellationTokenSource asyncCts;
        private System.Threading.Tasks.Task asyncSrvTask;

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
            var btnStartTcpServer = this.Controls.Find("buttonStartTcpServer", true).FirstOrDefault() as Button;
            var btnConnectTcp = this.Controls.Find("buttonConnectTcp", true).FirstOrDefault() as Button;
            var btnDisconnectTcp = this.Controls.Find("buttonDisconnectTcp", true).FirstOrDefault() as Button;
            var btnClientSend = this.Controls.Find("buttonClientSend", true).FirstOrDefault() as Button;

            if (btnCreate != null) btnCreate.Enabled = !opened;
            if (btnClose != null) btnClose.Enabled = opened;
            if (btnSend != null) btnSend.Enabled = opened;
            if (btnReceive != null) btnReceive.Enabled = opened;
            if (btnClear != null) btnClear.Enabled = true;
            
            // Gestion des boutons de scrutation
            bool pollingActive = timerPolling.Enabled;
            if (btnStartPolling != null) btnStartPolling.Enabled = opened && !pollingActive;
            if (btnStopPolling != null) btnStopPolling.Enabled = opened && pollingActive;

            // Démarrage serveur TCP : activable si pas déjà en écoute
            if (btnStartTcpServer != null) btnStartTcpServer.Enabled = (listenTcp == null);

            // Boutons client TCP
            bool clientConnected = tcpClient != null;
            if (btnConnectTcp != null) btnConnectTcp.Enabled = !clientConnected;
            if (btnDisconnectTcp != null) btnDisconnectTcp.Enabled = clientConnected;
            if (btnClientSend != null) btnClientSend.Enabled = clientConnected;
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
                SSockUDP.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);
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

                // Arrêt et fermeture propre du serveur TCP s'il est actif
                try { if (clientTcp != null) { clientTcp.Close(); clientTcp = null; } } catch { }
                try { if (listenTcp != null) { listenTcp.Close(); listenTcp = null; } } catch { }
                try { if (timerTcp != null) timerTcp.Stop(); } catch { }
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

        // Journalisation pour le serveur TCP
        private void AppendServerLine(string text)
        {
            var tb = this.Controls.Find("textBoxServerLog", true).FirstOrDefault() as TextBox;
            if (tb != null)
                tb.AppendText(text + Environment.NewLine);
            else
                AppendRecvLine("[TCP] " + text);
        }

        // Démarrage du serveur TCP (écoute et Poll via timer)
        private void buttonStartTcpServer_Click(object sender, EventArgs e)
        {
            try
            {
                if (listenTcp != null)
                {
                    AppendServerLine("Serveur TCP déjà démarré.");
                    return;
                }

                var localIP = IPAddress.Parse(textBoxLocalIP.Text.Trim());
                int localPort = int.Parse(textBoxLocalPort.Text.Trim());
                IPEndPoint localEP = new IPEndPoint(localIP, localPort);

                listenTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listenTcp.Bind(localEP);
                listenTcp.Listen(10);

                AppendServerLine($"Serveur TCP en écoute sur {localEP}");
                timerTcp.Start();
                UpdateButtons();
            }
            catch (SocketException se)
            {
                AppendServerLine("Erreur démarrage serveur TCP : " + se.Message);
                try { if (listenTcp != null) { listenTcp.Close(); listenTcp = null; } } catch { }
            }
            catch (Exception ex)
            {
                AppendServerLine("Erreur inconnue (serveur TCP) : " + ex.Message);
                try { if (listenTcp != null) { listenTcp.Close(); listenTcp = null; } } catch { }
            }
        }

        // Boucle scrutation TCP Timer
        private void timerTcp_Tick(object sender, EventArgs e)
        {
            try
            {
                if (listenTcp != null && listenTcp.Poll(0, SelectMode.SelectRead))
                {
                    Socket s = listenTcp.Accept();
                    clientTcp = s;
                    try { clientTcpEP = s.RemoteEndPoint as IPEndPoint; } catch { clientTcpEP = null; }
                    AppendServerLine($"Client connecté: {clientTcpEP}");
                }

                if (clientTcp != null)
                {
                    // Détection déconnexion
                    if (clientTcp.Poll(0, SelectMode.SelectRead))
                    {
                        if (clientTcp.Available == 0)
                        {
                            AppendServerLine("Client déconnecté.");
                            clientTcp.Close();
                            clientTcp = null;
                            clientTcpEP = null;
                        }
                        else
                        {
                            byte[] buf = new byte[4096];
                            int n = clientTcp.Receive(buf);
                            if (n > 0)
                            {
                                string txt = Encoding.ASCII.GetString(buf, 0, n);
                                AppendServerLine($"De {clientTcpEP} -> {txt}");
                                // Echo
                                clientTcp.Send(buf, n, SocketFlags.None);
                            }
                        }
                    }
                }
            }
            catch (SocketException se)
            {
                AppendServerLine("Erreur TCP : " + se.SocketErrorCode + " - " + se.Message);
            }
            catch (Exception ex)
            {
                AppendServerLine("Erreur inconnue TCP : " + ex.Message);
            }
        }

        // Journalisation pour le client TCP
        private void AppendClientLine(string text)
        {
            var tb = this.Controls.Find("textBoxClientLog", true).FirstOrDefault() as TextBox;
            if (tb != null)
                tb.AppendText(text + Environment.NewLine);
            else
                AppendRecvLine("[TCP-Client] " + text);
        }

        private void buttonConnectTcp_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient != null)
                {
                    AppendClientLine("Déjà connecté.");
                    return;
                }

                var destIP = IPAddress.Parse(textBoxDestIP.Text.Trim());
                int destPort = int.Parse(textBoxDestPort.Text.Trim());
                var destEP = new IPEndPoint(destIP, destPort);

                tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tcpClient.Connect(destEP);
                AppendClientLine($"Connecté à {destEP}");
                timerClient.Start();
            }
            catch (SocketException se)
            {
                AppendClientLine("Erreur connexion: " + se.SocketErrorCode + " - " + se.Message);
                try { if (tcpClient != null) { tcpClient.Close(); tcpClient = null; } } catch { }
            }
            catch (Exception ex)
            {
                AppendClientLine("Erreur inconnue (connexion): " + ex.Message);
                try { if (tcpClient != null) { tcpClient.Close(); tcpClient = null; } } catch { }
            }
            finally
            {
                UpdateButtons();
            }
        }

        private void buttonDisconnectTcp_Click(object sender, EventArgs e)
        {
            try
            {
                timerClient.Stop();
                if (tcpClient != null)
                {
                    tcpClient.Close();
                    tcpClient = null;
                    AppendClientLine("Déconnecté.");
                }
            }
            catch { }
            finally
            {
                UpdateButtons();
            }
        }

        private void buttonClientSend_Click(object sender, EventArgs e)
        {
            if (tcpClient == null)
            {
                AppendClientLine("Non connecté.");
                return;
            }
            try
            {
                var txtBox = this.Controls.Find("textBoxClientSend", true).FirstOrDefault() as TextBox;
                string msg = txtBox != null ? (txtBox.Text ?? string.Empty) : string.Empty;
                byte[] data = Encoding.ASCII.GetBytes(msg);
                tcpClient.Send(data);
                AppendClientLine($"Envoyé {data.Length} octets");
            }
            catch (SocketException se)
            {
                AppendClientLine("Erreur envoi: " + se.SocketErrorCode + " - " + se.Message);
            }
            catch (Exception ex)
            {
                AppendClientLine("Erreur inconnue (envoi): " + ex.Message);
            }
        }

        private async void buttonStartAsyncServer_Click(object sender, EventArgs e)
        {
            try
            {
                if (asyncSrvTask != null && !asyncSrvTask.IsCompleted) return;
                var ip = IPAddress.Parse(textBoxLocalIP.Text.Trim());
                int port = int.Parse(textBoxLocalPort.Text.Trim());
                var ep = new IPEndPoint(ip, port);
                asyncListen = new Socket(ep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                asyncListen.Bind(ep);
                asyncListen.Listen(100);
                asyncCts = new System.Threading.CancellationTokenSource();
                asyncSrvTask = RunAsyncEcho(asyncListen, asyncCts.Token);
                AppendServerLine($"[Async] En écoute sur {ep}");
            }
            catch (Exception ex)
            {
                AppendServerLine("[Async] Erreur: " + ex.Message);
                try { asyncListen?.Close(); asyncListen = null; } catch { }
            }
        }

        private async System.Threading.Tasks.Task RunAsyncEcho(Socket listener, System.Threading.CancellationToken ct)
        {
            try
            {
                var handler = await listener.AcceptAsync();
                var rep = handler.RemoteEndPoint as IPEndPoint;
                AppendServerLine($"[Async] Client {rep}");
                while (!ct.IsCancellationRequested)
                {
                    var buf = new byte[1024];
                    var received = await handler.ReceiveAsync(new ArraySegment<byte>(buf), SocketFlags.None);
                    if (received == 0) break;
                    var txt = Encoding.UTF8.GetString(buf, 0, received);
                    AppendServerLine($"[Async] -> {txt}");
                    if (txt.Contains("<|EOM|>"))
                    {
                        var ack = Encoding.UTF8.GetBytes("<|ACK|>");
                        await handler.SendAsync(new ArraySegment<byte>(ack), SocketFlags.None);
                        AppendServerLine("[Async] ACK envoyé");
                        break;
                    }
                }
                try { handler.Shutdown(SocketShutdown.Both); } catch { }
                handler.Close();
            }
            catch (Exception ex)
            {
                AppendServerLine("[Async] Erreur boucle: " + ex.Message);
            }
            finally
            {
                try { listener.Close(); } catch { }
                asyncListen = null;
            }
        }

        private async void buttonTcpClientExample_Click(object sender, EventArgs e)
        {
            try
            {
                string host = textBoxDestIP.Text.Trim();
                int port = int.Parse(textBoxDestPort.Text.Trim());
                using (var client = new System.Net.Sockets.TcpClient(host, port))
                using (var ns = client.GetStream())
                {
                    var req = Encoding.ASCII.GetBytes("GET /\r\n\r\n");
                    await ns.WriteAsync(req, 0, req.Length);
                    var buf = new byte[4096];
                    int n = await ns.ReadAsync(buf, 0, buf.Length);
                    AppendClientLine($"[TcpClient] nbcarecu {n}\r\n" + Encoding.ASCII.GetString(buf, 0, Math.Max(0, n)));
                }
            }
            catch (Exception ex)
            {
                AppendClientLine("[TcpClient] Erreur: " + ex.Message);
            }
        }

        private void timerClient_Tick(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient == null) return;

                if (tcpClient.Poll(0, SelectMode.SelectRead))
                {
                    if (tcpClient.Available == 0)
                    {
                        AppendClientLine("Serveur fermé.");
                        timerClient.Stop();
                        tcpClient.Close();
                        tcpClient = null;
                        UpdateButtons();
                        return;
                    }
                    byte[] buf = new byte[4096];
                    int n = tcpClient.Receive(buf);
                    if (n > 0)
                    {
                        string txt = Encoding.ASCII.GetString(buf, 0, n);
                        AppendClientLine($"Reçu -> {txt}");
                    }
                }
            }
            catch (SocketException se)
            {
                AppendClientLine("Erreur réception: " + se.SocketErrorCode + " - " + se.Message);
            }
            catch (Exception ex)
            {
                AppendClientLine("Erreur inconnue (réception): " + ex.Message);
            }
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

