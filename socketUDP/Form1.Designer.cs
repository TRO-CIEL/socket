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
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
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
            this.lblLocal = new System.Windows.Forms.Label();
            this.txtLocalIP = new System.Windows.Forms.TextBox();
            this.txtLocalPort = new System.Windows.Forms.TextBox();
            this.lblDest = new System.Windows.Forms.Label();
            this.txtDestIP = new System.Windows.Forms.TextBox();
            this.txtDestPort = new System.Windows.Forms.TextBox();
            this.lblEnvoi = new System.Windows.Forms.Label();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.lblRecv = new System.Windows.Forms.Label();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.btnCreateBind = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnReceive = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblLocalPort = new System.Windows.Forms.Label();
            this.lblDestPort = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLocal
            // 
            this.lblLocal.AutoSize = true;
            this.lblLocal.Location = new System.Drawing.Point(12, 15);
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Size = new System.Drawing.Size(37, 13);
            this.lblLocal.TabIndex = 0;
            this.lblLocal.Text = "Recp.";
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.Location = new System.Drawing.Point(70, 12);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.Size = new System.Drawing.Size(120, 20);
            this.txtLocalIP.TabIndex = 1;
            this.txtLocalIP.Text = "127.0.0.1";
            // 
            // txtLocalPort
            // 
            this.txtLocalPort.Location = new System.Drawing.Point(228, 12);
            this.txtLocalPort.Name = "txtLocalPort";
            this.txtLocalPort.Size = new System.Drawing.Size(60, 20);
            this.txtLocalPort.TabIndex = 2;
            this.txtLocalPort.Text = "3031";
            // 
            // lblLocalPort
            // 
            this.lblLocalPort.AutoSize = true;
            this.lblLocalPort.Location = new System.Drawing.Point(196, 15);
            this.lblLocalPort.Name = "lblLocalPort";
            this.lblLocalPort.Size = new System.Drawing.Size(26, 13);
            this.lblLocalPort.TabIndex = 3;
            this.lblLocalPort.Text = "Port";
            // 
            // lblDest
            // 
            this.lblDest.AutoSize = true;
            this.lblDest.Location = new System.Drawing.Point(12, 45);
            this.lblDest.Name = "lblDest";
            this.lblDest.Size = new System.Drawing.Size(37, 13);
            this.lblDest.TabIndex = 4;
            this.lblDest.Text = "Dest.";
            // 
            // txtDestIP
            // 
            this.txtDestIP.Location = new System.Drawing.Point(70, 42);
            this.txtDestIP.Name = "txtDestIP";
            this.txtDestIP.Size = new System.Drawing.Size(120, 20);
            this.txtDestIP.TabIndex = 5;
            this.txtDestIP.Text = "127.0.0.1";
            // 
            // txtDestPort
            // 
            this.txtDestPort.Location = new System.Drawing.Point(228, 42);
            this.txtDestPort.Name = "txtDestPort";
            this.txtDestPort.Size = new System.Drawing.Size(60, 20);
            this.txtDestPort.TabIndex = 6;
            this.txtDestPort.Text = "3032";
            // 
            // lblDestPort
            // 
            this.lblDestPort.AutoSize = true;
            this.lblDestPort.Location = new System.Drawing.Point(196, 45);
            this.lblDestPort.Name = "lblDestPort";
            this.lblDestPort.Size = new System.Drawing.Size(26, 13);
            this.lblDestPort.TabIndex = 7;
            this.lblDestPort.Text = "Port";
            // 
            // lblEnvoi
            // 
            this.lblEnvoi.AutoSize = true;
            this.lblEnvoi.Location = new System.Drawing.Point(12, 80);
            this.lblEnvoi.Name = "lblEnvoi";
            this.lblEnvoi.Size = new System.Drawing.Size(34, 13);
            this.lblEnvoi.TabIndex = 8;
            this.lblEnvoi.Text = "Envoi";
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(70, 77);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(218, 20);
            this.txtSend.TabIndex = 9;
            this.txtSend.Text = "Bonjour UDP";
            // 
            // lblRecv
            // 
            this.lblRecv.AutoSize = true;
            this.lblRecv.Location = new System.Drawing.Point(12, 113);
            this.lblRecv.Name = "lblRecv";
            this.lblRecv.Size = new System.Drawing.Size(33, 13);
            this.lblRecv.TabIndex = 10;
            this.lblRecv.Text = "Recp.";
            // 
            // txtReceive
            // 
            this.txtReceive.Location = new System.Drawing.Point(70, 110);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceive.Size = new System.Drawing.Size(218, 180);
            this.txtReceive.TabIndex = 11;
            // 
            // btnCreateBind
            // 
            this.btnCreateBind.Location = new System.Drawing.Point(315, 10);
            this.btnCreateBind.Name = "btnCreateBind";
            this.btnCreateBind.Size = new System.Drawing.Size(200, 25);
            this.btnCreateBind.TabIndex = 12;
            this.btnCreateBind.Text = "Créer Socket et Bind(IPEr)";
            this.btnCreateBind.UseVisualStyleBackColor = true;
            this.btnCreateBind.Click += new System.EventHandler(this.btnCreateBind_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(315, 45);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 25);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Fermer Close()";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(315, 75);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(200, 25);
            this.btnSend.TabIndex = 14;
            this.btnSend.Text = "Envoyer SendTo(IPEd)";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnReceive
            // 
            this.btnReceive.Location = new System.Drawing.Point(315, 110);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(200, 25);
            this.btnReceive.TabIndex = 15;
            this.btnReceive.Text = "Recevoir ReceiveFrom()";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(315, 265);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 25);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "CLS";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 311);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnReceive);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateBind);
            this.Controls.Add(this.txtReceive);
            this.Controls.Add(this.lblRecv);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.lblEnvoi);
            this.Controls.Add(this.lblDestPort);
            this.Controls.Add(this.txtDestPort);
            this.Controls.Add(this.txtDestIP);
            this.Controls.Add(this.lblDest);
            this.Controls.Add(this.lblLocalPort);
            this.Controls.Add(this.txtLocalPort);
            this.Controls.Add(this.txtLocalIP);
            this.Controls.Add(this.lblLocal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Communication par socket UDP";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.TextBox txtLocalIP;
        private System.Windows.Forms.TextBox txtLocalPort;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.TextBox txtDestIP;
        private System.Windows.Forms.TextBox txtDestPort;
        private System.Windows.Forms.Label lblEnvoi;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Label lblRecv;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.Button btnCreateBind;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblLocalPort;
        private System.Windows.Forms.Label lblDestPort;
    }
}

