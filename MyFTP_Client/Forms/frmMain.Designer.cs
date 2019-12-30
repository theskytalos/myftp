namespace MyFTP_Client
{
    partial class frmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblTitle = new System.Windows.Forms.Label();
            this.gpbServer = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lstServerFiles = new System.Windows.Forms.ListView();
            this.imglstIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnList = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.pgbFileTransfer = new System.Windows.Forms.ProgressBar();
            this.msControls = new System.Windows.Forms.MenuStrip();
            this.usuárioToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.apagarUsuárioToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçõsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.usuárioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mudarSenhaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apagarUsuárioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stsFileTransfer = new System.Windows.Forms.StatusStrip();
            this.tssFileTransferSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmFileTransferSpeed = new System.Windows.Forms.Timer(this.components);
            this.gpbServer.SuspendLayout();
            this.msControls.SuspendLayout();
            this.stsFileTransfer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(15, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(634, 60);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "MyFTP - Client";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gpbServer
            // 
            this.gpbServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbServer.Controls.Add(this.btnDelete);
            this.gpbServer.Controls.Add(this.lstServerFiles);
            this.gpbServer.Controls.Add(this.btnList);
            this.gpbServer.Controls.Add(this.btnUpload);
            this.gpbServer.Controls.Add(this.btnDownload);
            this.gpbServer.Location = new System.Drawing.Point(12, 87);
            this.gpbServer.Name = "gpbServer";
            this.gpbServer.Size = new System.Drawing.Size(637, 283);
            this.gpbServer.TabIndex = 3;
            this.gpbServer.TabStop = false;
            this.gpbServer.Text = "Servidor";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(311, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Deletar";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // lstServerFiles
            // 
            this.lstServerFiles.AllowDrop = true;
            this.lstServerFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstServerFiles.HideSelection = false;
            this.lstServerFiles.LargeImageList = this.imglstIcons;
            this.lstServerFiles.Location = new System.Drawing.Point(6, 48);
            this.lstServerFiles.Name = "lstServerFiles";
            this.lstServerFiles.Size = new System.Drawing.Size(625, 229);
            this.lstServerFiles.TabIndex = 0;
            this.lstServerFiles.UseCompatibleStateImageBehavior = false;
            this.lstServerFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstServerFiles_DragDrop);
            this.lstServerFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstServerFiles_DragEnter);
            this.lstServerFiles.DoubleClick += new System.EventHandler(this.LstServerFiles_DoubleClick);
            // 
            // imglstIcons
            // 
            this.imglstIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imglstIcons.ImageSize = new System.Drawing.Size(32, 32);
            this.imglstIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnList
            // 
            this.btnList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnList.Location = new System.Drawing.Point(554, 19);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(77, 23);
            this.btnList.TabIndex = 2;
            this.btnList.Text = "Atualizar";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.BtnList_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.Location = new System.Drawing.Point(392, 19);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Location = new System.Drawing.Point(473, 19);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // pgbFileTransfer
            // 
            this.pgbFileTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbFileTransfer.Location = new System.Drawing.Point(12, 376);
            this.pgbFileTransfer.Name = "pgbFileTransfer";
            this.pgbFileTransfer.Size = new System.Drawing.Size(637, 23);
            this.pgbFileTransfer.TabIndex = 4;
            // 
            // msControls
            // 
            this.msControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuárioToolStripMenuItem1,
            this.configuraçõsToolStripMenuItem,
            this.fecharToolStripMenuItem1});
            this.msControls.Location = new System.Drawing.Point(0, 0);
            this.msControls.Name = "msControls";
            this.msControls.Size = new System.Drawing.Size(661, 24);
            this.msControls.TabIndex = 5;
            this.msControls.Text = "menuStrip1";
            // 
            // usuárioToolStripMenuItem1
            // 
            this.usuárioToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apagarUsuárioToolStripMenuItem1,
            this.sairToolStripMenuItem1});
            this.usuárioToolStripMenuItem1.Name = "usuárioToolStripMenuItem1";
            this.usuárioToolStripMenuItem1.Size = new System.Drawing.Size(59, 20);
            this.usuárioToolStripMenuItem1.Text = "Usuário";
            // 
            // apagarUsuárioToolStripMenuItem1
            // 
            this.apagarUsuárioToolStripMenuItem1.Name = "apagarUsuárioToolStripMenuItem1";
            this.apagarUsuárioToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.apagarUsuárioToolStripMenuItem1.Text = "Apagar Usuário";
            this.apagarUsuárioToolStripMenuItem1.Click += new System.EventHandler(this.ApagarUsuárioToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem1
            // 
            this.sairToolStripMenuItem1.Name = "sairToolStripMenuItem1";
            this.sairToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.sairToolStripMenuItem1.Text = "Sair";
            this.sairToolStripMenuItem1.Click += new System.EventHandler(this.SairToolStripMenuItem_Click);
            // 
            // configuraçõsToolStripMenuItem
            // 
            this.configuraçõsToolStripMenuItem.Name = "configuraçõsToolStripMenuItem";
            this.configuraçõsToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.configuraçõsToolStripMenuItem.Text = "Configurações";
            this.configuraçõsToolStripMenuItem.Click += new System.EventHandler(this.ConfiguraçõesToolStripMenuItem_Click);
            // 
            // fecharToolStripMenuItem1
            // 
            this.fecharToolStripMenuItem1.Name = "fecharToolStripMenuItem1";
            this.fecharToolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.fecharToolStripMenuItem1.Text = "Fechar";
            this.fecharToolStripMenuItem1.Click += new System.EventHandler(this.FecharToolStripMenuItem_Click);
            // 
            // usuárioToolStripMenuItem
            // 
            this.usuárioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mudarSenhaToolStripMenuItem,
            this.apagarUsuárioToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.usuárioToolStripMenuItem.Name = "usuárioToolStripMenuItem";
            this.usuárioToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.usuárioToolStripMenuItem.Text = "Usuário";
            // 
            // mudarSenhaToolStripMenuItem
            // 
            this.mudarSenhaToolStripMenuItem.Name = "mudarSenhaToolStripMenuItem";
            this.mudarSenhaToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.mudarSenhaToolStripMenuItem.Text = "Mudar Senha";
            // 
            // apagarUsuárioToolStripMenuItem
            // 
            this.apagarUsuárioToolStripMenuItem.Name = "apagarUsuárioToolStripMenuItem";
            this.apagarUsuárioToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.apagarUsuárioToolStripMenuItem.Text = "Apagar Usuário";
            this.apagarUsuárioToolStripMenuItem.Click += new System.EventHandler(this.ApagarUsuárioToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.SairToolStripMenuItem_Click);
            // 
            // configuraçõesToolStripMenuItem
            // 
            this.configuraçõesToolStripMenuItem.Name = "configuraçõesToolStripMenuItem";
            this.configuraçõesToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.configuraçõesToolStripMenuItem.Text = "Configurações";
            this.configuraçõesToolStripMenuItem.Click += new System.EventHandler(this.ConfiguraçõesToolStripMenuItem_Click);
            // 
            // fecharToolStripMenuItem
            // 
            this.fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            this.fecharToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fecharToolStripMenuItem.Text = "Fechar";
            this.fecharToolStripMenuItem.Click += new System.EventHandler(this.FecharToolStripMenuItem_Click);
            // 
            // stsFileTransfer
            // 
            this.stsFileTransfer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssFileTransferSpeed});
            this.stsFileTransfer.Location = new System.Drawing.Point(0, 402);
            this.stsFileTransfer.Name = "stsFileTransfer";
            this.stsFileTransfer.Size = new System.Drawing.Size(661, 22);
            this.stsFileTransfer.TabIndex = 6;
            this.stsFileTransfer.Text = "statusStrip1";
            // 
            // tssFileTransferSpeed
            // 
            this.tssFileTransferSpeed.Name = "tssFileTransferSpeed";
            this.tssFileTransferSpeed.Size = new System.Drawing.Size(43, 17);
            this.tssFileTransferSpeed.Text = "Pronto";
            // 
            // tmFileTransferSpeed
            // 
            this.tmFileTransferSpeed.Enabled = true;
            this.tmFileTransferSpeed.Interval = 1000;
            this.tmFileTransferSpeed.Tick += new System.EventHandler(this.TmFileTransferSpeed_Tick);
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 424);
            this.Controls.Add(this.stsFileTransfer);
            this.Controls.Add(this.pgbFileTransfer);
            this.Controls.Add(this.gpbServer);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.msControls);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msControls;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyFTP - Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.gpbServer.ResumeLayout(false);
            this.msControls.ResumeLayout(false);
            this.msControls.PerformLayout();
            this.stsFileTransfer.ResumeLayout(false);
            this.stsFileTransfer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ListView lstServerFiles;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.MenuStrip msControls;
        private System.Windows.Forms.ToolStripMenuItem usuárioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mudarSenhaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apagarUsuárioToolStripMenuItem;
        private System.Windows.Forms.ImageList imglstIcons;
        private System.Windows.Forms.ToolStripMenuItem usuárioToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem apagarUsuárioToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configuraçõsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem1;
        public System.Windows.Forms.GroupBox gpbServer;
        public System.Windows.Forms.ProgressBar pgbFileTransfer;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.StatusStrip stsFileTransfer;
        private System.Windows.Forms.ToolStripStatusLabel tssFileTransferSpeed;
        private System.Windows.Forms.Timer tmFileTransferSpeed;
    }
}

