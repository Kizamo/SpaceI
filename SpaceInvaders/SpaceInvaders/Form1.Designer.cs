namespace SpaceInvaders
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.newGame = new System.Windows.Forms.Button();
            this.titleScreen = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.exit = new System.Windows.Forms.Button();
            this.pause = new System.Windows.Forms.Button();
            this.mute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newGame
            // 
            this.newGame.Image = global::SpaceInvaders.Properties.Resources.newGame;
            this.newGame.Location = new System.Drawing.Point(881, 649);
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(115, 45);
            this.newGame.TabIndex = 0;
            this.newGame.UseVisualStyleBackColor = true;
            this.newGame.Visible = false;
            this.newGame.Click += new System.EventHandler(this.newGame_Click);
            // 
            // titleScreen
            // 
            this.titleScreen.Tick += new System.EventHandler(this.titleScreen_Tick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // exit
            // 
            this.exit.Image = global::SpaceInvaders.Properties.Resources.exit;
            this.exit.Location = new System.Drawing.Point(881, 704);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(115, 45);
            this.exit.TabIndex = 1;
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Visible = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // pause
            // 
            this.pause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pause.Image = ((System.Drawing.Image)(resources.GetObject("pause.Image")));
            this.pause.Location = new System.Drawing.Point(881, 649);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(115, 45);
            this.pause.TabIndex = 2;
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Visible = false;
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // mute
            // 
            this.mute.BackgroundImage = global::SpaceInvaders.Properties.Resources.speaker;
            this.mute.Location = new System.Drawing.Point(816, 704);
            this.mute.Name = "mute";
            this.mute.Size = new System.Drawing.Size(45, 45);
            this.mute.TabIndex = 3;
            this.mute.UseVisualStyleBackColor = true;
            this.mute.Visible = false;
            this.mute.Click += new System.EventHandler(this.mute_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SpaceInvaders.Properties.Resources.title;
            this.ClientSize = new System.Drawing.Size(1008, 761);
            this.Controls.Add(this.mute);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.newGame);
            this.Name = "Form1";
            this.Text = "Space Invaders";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newGame;
        private System.Windows.Forms.Timer titleScreen;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button pause;
        private System.Windows.Forms.Button mute;
    }
}

