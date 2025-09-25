namespace socketUDP
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelLocal = new System.Windows.Forms.Label();
            this.textBoxLocalIP = new System.Windows.Forms.TextBox();
            this.textBoxLocalPort = new System.Windows.Forms.TextBox();
            this.labelDest = new System.Windows.Forms.Label();
            this.textBoxDestIP = new System.Windows.Forms.TextBox();
            this.textBoxDestPort = new System.Windows.Forms.TextBox();
            this.labelEnvoi = new System.Windows.Forms.Label();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.labelRecv = new System.Windows.Forms.Label();
            this.textBoxReceive = new System.Windows.Forms.TextBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonReceive = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonStartPolling = new System.Windows.Forms.Button();
            this.buttonStopPolling = new System.Windows.Forms.Button();
            this.buttonStartTcpServer = new System.Windows.Forms.Button();
            this.timerTcp = new System.Windows.Forms.Timer(this.components);
            this.buttonConnectTcp = new System.Windows.Forms.Button();
            this.buttonDisconnectTcp = new System.Windows.Forms.Button();
            this.textBoxClientSend = new System.Windows.Forms.TextBox();
            this.buttonClientSend = new System.Windows.Forms.Button();
            this.textBoxClientLog = new System.Windows.Forms.TextBox();
            this.timerClient = new System.Windows.Forms.Timer(this.components);
            this.labelLocalPort = new System.Windows.Forms.Label();
            this.labelDestPort = new System.Windows.Forms.Label();
            this.timerPolling = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelLocal
            // 
            this.labelLocal.AutoSize = true;
            this.labelLocal.Location = new System.Drawing.Point(12, 15);
            this.labelLocal.Name = "labelLocal";
            this.labelLocal.Size = new System.Drawing.Size(36, 13);
            this.labelLocal.TabIndex = 0;
            this.labelLocal.Text = "Recp.";
            // 
            // textBoxLocalIP
            // 
            this.textBoxLocalIP.Location = new System.Drawing.Point(70, 12);
            this.textBoxLocalIP.Name = "textBoxLocalIP";
            this.textBoxLocalIP.Size = new System.Drawing.Size(120, 20);
            this.textBoxLocalIP.TabIndex = 1;
            this.textBoxLocalIP.Text = "127.0.0.1";
            // 
            // textBoxLocalPort
            // 
            this.textBoxLocalPort.Location = new System.Drawing.Point(228, 12);
            this.textBoxLocalPort.Name = "textBoxLocalPort";
            this.textBoxLocalPort.Size = new System.Drawing.Size(60, 20);
            this.textBoxLocalPort.TabIndex = 2;
            this.textBoxLocalPort.Text = "3031";
            // 
            // labelDest
            // 
            this.labelDest.AutoSize = true;
            this.labelDest.Location = new System.Drawing.Point(12, 45);
            this.labelDest.Name = "labelDest";
            this.labelDest.Size = new System.Drawing.Size(32, 13);
            this.labelDest.TabIndex = 4;
            this.labelDest.Text = "Dest.";
            // 
            // textBoxDestIP
            // 
            this.textBoxDestIP.Location = new System.Drawing.Point(70, 42);
            this.textBoxDestIP.Name = "textBoxDestIP";
            this.textBoxDestIP.Size = new System.Drawing.Size(120, 20);
            this.textBoxDestIP.TabIndex = 5;
            this.textBoxDestIP.Text = "127.0.0.1";
            // 
            // textBoxDestPort
            // 
            this.textBoxDestPort.Location = new System.Drawing.Point(228, 42);
            this.textBoxDestPort.Name = "textBoxDestPort";
            this.textBoxDestPort.Size = new System.Drawing.Size(60, 20);
            this.textBoxDestPort.TabIndex = 6;
            this.textBoxDestPort.Text = "3032";
            // 
            // labelEnvoi
            // 
            this.labelEnvoi.AutoSize = true;
            this.labelEnvoi.Location = new System.Drawing.Point(12, 80);
            this.labelEnvoi.Name = "labelEnvoi";
            this.labelEnvoi.Size = new System.Drawing.Size(34, 13);
            this.labelEnvoi.TabIndex = 8;
            this.labelEnvoi.Text = "Envoi";
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(70, 77);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(218, 20);
            this.textBoxSend.TabIndex = 9;
            this.textBoxSend.Text = "Bonjour UDP";
            // 
            // labelRecv
            // 
            this.labelRecv.AutoSize = true;
            this.labelRecv.Location = new System.Drawing.Point(12, 113);
            this.labelRecv.Name = "labelRecv";
            this.labelRecv.Size = new System.Drawing.Size(36, 13);
            this.labelRecv.TabIndex = 10;
            this.labelRecv.Text = "Recp.";
            // 
            // textBoxReceive
            // 
            this.textBoxReceive.Location = new System.Drawing.Point(70, 110);
            this.textBoxReceive.Multiline = true;
            this.textBoxReceive.Name = "textBoxReceive";
            this.textBoxReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReceive.Size = new System.Drawing.Size(218, 180);
            this.textBoxReceive.TabIndex = 11;
            this.textBoxReceive.TextChanged += new System.EventHandler(this.textBoxReceive_TextChanged);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(315, 10);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(200, 25);
            this.buttonCreate.TabIndex = 12;
            this.buttonCreate.Text = "Créer Socket et Bind(IPEr)";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(315, 45);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(200, 25);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "Fermer Close()";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(315, 75);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(200, 25);
            this.buttonSend.TabIndex = 14;
            this.buttonSend.Text = "Envoyer SendTo(IPEd)";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonReceive
            // 
            this.buttonReceive.Location = new System.Drawing.Point(315, 110);
            this.buttonReceive.Name = "buttonReceive";
            this.buttonReceive.Size = new System.Drawing.Size(200, 25);
            this.buttonReceive.TabIndex = 15;
            this.buttonReceive.Text = "Recevoir ReceiveFrom()";
            this.buttonReceive.UseVisualStyleBackColor = true;
            this.buttonReceive.Click += new System.EventHandler(this.buttonReceive_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(315, 265);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(80, 25);
            this.buttonClear.TabIndex = 16;
            this.buttonClear.Text = "CLS";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonStartPolling
            // 
            this.buttonStartPolling.Location = new System.Drawing.Point(315, 140);
            this.buttonStartPolling.Name = "buttonStartPolling";
            this.buttonStartPolling.Size = new System.Drawing.Size(200, 25);
            this.buttonStartPolling.TabIndex = 17;
            this.buttonStartPolling.Text = "Démarrer scrutation";
            this.buttonStartPolling.UseVisualStyleBackColor = true;
            this.buttonStartPolling.Click += new System.EventHandler(this.buttonStartPolling_Click);
            // 
            // buttonStopPolling
            // 
            this.buttonStopPolling.Location = new System.Drawing.Point(315, 170);
            this.buttonStopPolling.Name = "buttonStopPolling";
            this.buttonStopPolling.Size = new System.Drawing.Size(200, 25);
            this.buttonStopPolling.TabIndex = 18;
            this.buttonStopPolling.Text = "Arrêter scrutation";
            this.buttonStopPolling.UseVisualStyleBackColor = true;
            this.buttonStopPolling.Click += new System.EventHandler(this.buttonStopPolling_Click);
            // 
            // buttonStartTcpServer
            // 
            this.buttonStartTcpServer.Location = new System.Drawing.Point(315, 320);
            this.buttonStartTcpServer.Name = "buttonStartTcpServer";
            this.buttonStartTcpServer.Size = new System.Drawing.Size(200, 25);
            this.buttonStartTcpServer.TabIndex = 19;
            this.buttonStartTcpServer.Text = "Démarrer serveur TCP";
            this.buttonStartTcpServer.UseVisualStyleBackColor = true;
            this.buttonStartTcpServer.Click += new System.EventHandler(this.buttonStartTcpServer_Click);
            // 
            // timerTcp
            // 
            this.timerTcp.Interval = 100;
            this.timerTcp.Tick += new System.EventHandler(this.timerTcp_Tick);
            // 
            
            // 
            // buttonConnectTcp
            // 
            this.buttonConnectTcp.Location = new System.Drawing.Point(315, 350);
            this.buttonConnectTcp.Name = "buttonConnectTcp";
            this.buttonConnectTcp.Size = new System.Drawing.Size(200, 25);
            this.buttonConnectTcp.TabIndex = 21;
            this.buttonConnectTcp.Text = "Client: Connexion TCP";
            this.buttonConnectTcp.UseVisualStyleBackColor = true;
            this.buttonConnectTcp.Click += new System.EventHandler(this.buttonConnectTcp_Click);
            // 
            // buttonDisconnectTcp
            // 
            this.buttonDisconnectTcp.Location = new System.Drawing.Point(315, 380);
            this.buttonDisconnectTcp.Name = "buttonDisconnectTcp";
            this.buttonDisconnectTcp.Size = new System.Drawing.Size(200, 25);
            this.buttonDisconnectTcp.TabIndex = 22;
            this.buttonDisconnectTcp.Text = "Client: Déconnexion";
            this.buttonDisconnectTcp.UseVisualStyleBackColor = true;
            this.buttonDisconnectTcp.Click += new System.EventHandler(this.buttonDisconnectTcp_Click);
            // 
            // textBoxClientSend
            // 
            this.textBoxClientSend.Location = new System.Drawing.Point(12, 510);
            this.textBoxClientSend.Name = "textBoxClientSend";
            this.textBoxClientSend.Size = new System.Drawing.Size(426, 20);
            this.textBoxClientSend.TabIndex = 23;
            this.textBoxClientSend.Text = "Hello TCP";
            // 
            // buttonClientSend
            // 
            this.buttonClientSend.Location = new System.Drawing.Point(444, 508);
            this.buttonClientSend.Name = "buttonClientSend";
            this.buttonClientSend.Size = new System.Drawing.Size(76, 23);
            this.buttonClientSend.TabIndex = 24;
            this.buttonClientSend.Text = "Envoyer";
            this.buttonClientSend.UseVisualStyleBackColor = true;
            this.buttonClientSend.Click += new System.EventHandler(this.buttonClientSend_Click);
            // 
            // textBoxClientLog
            // 
            this.textBoxClientLog.Location = new System.Drawing.Point(12, 410);
            this.textBoxClientLog.Multiline = true;
            this.textBoxClientLog.Name = "textBoxClientLog";
            this.textBoxClientLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxClientLog.Size = new System.Drawing.Size(508, 90);
            this.textBoxClientLog.TabIndex = 25;
            // 
            // timerClient
            // 
            this.timerClient.Interval = 100;
            this.timerClient.Tick += new System.EventHandler(this.timerClient_Tick);
            // 
            // labelLocalPort
            // 
            this.labelLocalPort.AutoSize = true;
            this.labelLocalPort.Location = new System.Drawing.Point(196, 15);
            this.labelLocalPort.Name = "labelLocalPort";
            this.labelLocalPort.Size = new System.Drawing.Size(26, 13);
            this.labelLocalPort.TabIndex = 3;
            this.labelLocalPort.Text = "Port";
            // 
            // labelDestPort
            // 
            this.labelDestPort.AutoSize = true;
            this.labelDestPort.Location = new System.Drawing.Point(196, 45);
            this.labelDestPort.Name = "labelDestPort";
            this.labelDestPort.Size = new System.Drawing.Size(26, 13);
            this.labelDestPort.TabIndex = 7;
            this.labelDestPort.Text = "Port";
            // 
            // timerPolling
            // 
            this.timerPolling.Interval = 100;
            this.timerPolling.Tick += new System.EventHandler(this.timerPolling_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 600);
            this.Controls.Add(this.textBoxClientLog);
            this.Controls.Add(this.buttonStopPolling);
            this.Controls.Add(this.buttonStartPolling);
            this.Controls.Add(this.buttonStartTcpServer);
            this.Controls.Add(this.buttonConnectTcp);
            this.Controls.Add(this.buttonDisconnectTcp);
            this.Controls.Add(this.textBoxClientSend);
            this.Controls.Add(this.buttonClientSend);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonReceive);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.textBoxReceive);
            this.Controls.Add(this.labelRecv);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.labelEnvoi);
            this.Controls.Add(this.labelDestPort);
            this.Controls.Add(this.textBoxDestPort);
            this.Controls.Add(this.textBoxDestIP);
            this.Controls.Add(this.labelDest);
            this.Controls.Add(this.labelLocalPort);
            this.Controls.Add(this.textBoxLocalPort);
            this.Controls.Add(this.textBoxLocalIP);
            this.Controls.Add(this.labelLocal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Communication par socket UDP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLocal;
        private System.Windows.Forms.TextBox textBoxLocalIP;
        private System.Windows.Forms.TextBox textBoxLocalPort;
        private System.Windows.Forms.Label labelDest;
        private System.Windows.Forms.TextBox textBoxDestIP;
        private System.Windows.Forms.TextBox textBoxDestPort;
        private System.Windows.Forms.Label labelEnvoi;
        private System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.Label labelRecv;
        private System.Windows.Forms.TextBox textBoxReceive;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonReceive;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonStartPolling;
        private System.Windows.Forms.Button buttonStopPolling;
        private System.Windows.Forms.Button buttonStartTcpServer;
        private System.Windows.Forms.Button buttonConnectTcp;
        private System.Windows.Forms.Button buttonDisconnectTcp;
        private System.Windows.Forms.TextBox textBoxClientSend;
        private System.Windows.Forms.Button buttonClientSend;
        private System.Windows.Forms.TextBox textBoxClientLog;
        private System.Windows.Forms.Label labelLocalPort;
        private System.Windows.Forms.Label labelDestPort;
        private System.Windows.Forms.Timer timerPolling;
        private System.Windows.Forms.Timer timerTcp;
        private System.Windows.Forms.Timer timerClient;
    }
}
