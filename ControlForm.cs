//--------------------------------------------------------------------
// FILENAME: ControlForm.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:
//
// NOTES:
//
// 
//--------------------------------------------------------------------


using System;
using System.Drawing;   
using System.Collections;
using System.Windows.Forms;
using System.Data;
using SimpleAgroPrint;
using System.IO;
using System.Text;
//using CS_AudioSample1.ddc;
using CS_AudioSample1.ddcRfc;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;

namespace SimpleAgroPrint
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ControlForm : System.Windows.Forms.Form
    {
        [DllImport("coredll.dll", SetLastError = true)]
        public static extern bool PlaySound(string pszSound, IntPtr hmod, uint fdwSound);
        private const uint SND_ALIAS = 0x00010000; // Indica que pszSound es un alias de sonido del sistema
        private const uint SND_SYNC = 0x00000000; // Reproduce el sonido de forma sincrónica (el programa espera a que termine el sonido)


        Pantalla p;
        responce_DB_ETIQUETA_VENTANA[] response;
        bool tieneBotones = false;

        #region cosas
        private Symbol.Audio.Controller MyAudioController = null;
		private Symbol.ResourceCoordination.TerminalInfo terminalInfo;

        private static bool bPortrait = true;   // The default dispaly orientation 
        // has been set to Portrait.

        private bool bSkipMaxLen = false;    // The restriction on the maximum 
        // physical length is considered by default.

        private bool bInitialScale = true;   // The flag to track whether the 
        // scaling logic is applied for
        // the first time (from scatch) or not.
        // Based on that, the (outer) width/height values
        // of the form will be set or not.
        // Initially set to true.

        private int resWidthReference = 241;   // The (cached) width of the form. 
        // INITIALLY HAS TO BE SET TO THE WIDTH OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private int resHeightReference = 217;
        private Button bt0;
        private Button bt2;
        private Button bt3;
        private Button bt1;
        private Timer timer1;
        private LinkLabel lb00;
        private LinkLabel lb10;
        private LinkLabel lb20;
        private LinkLabel lb01;
        private LinkLabel lb11;
        private LinkLabel lb21;
        private LinkLabel lb02;
        private LinkLabel lb12;
        private LinkLabel lb22;
        private LinkLabel lb03;
        private LinkLabel lb13;
        private LinkLabel lb23;  // The (cached) height of the form.
        // INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private const double maxLength = 5.5;  // The maximum physical width/height of the sample (in inches).
        // The actual value on the device may slightly deviate from this
        // since the calculations based on the (received) DPI & resolution values 
        // would provide only an approximation, so not 100% accurate.

		public ControlForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			// Ensure that the keyboard focus is set on a control otherwise the keyboard will not operate.
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.bt0 = new System.Windows.Forms.Button();
            this.bt2 = new System.Windows.Forms.Button();
            this.bt3 = new System.Windows.Forms.Button();
            this.bt1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.lb00 = new System.Windows.Forms.LinkLabel();
            this.lb10 = new System.Windows.Forms.LinkLabel();
            this.lb20 = new System.Windows.Forms.LinkLabel();
            this.lb01 = new System.Windows.Forms.LinkLabel();
            this.lb11 = new System.Windows.Forms.LinkLabel();
            this.lb21 = new System.Windows.Forms.LinkLabel();
            this.lb02 = new System.Windows.Forms.LinkLabel();
            this.lb12 = new System.Windows.Forms.LinkLabel();
            this.lb22 = new System.Windows.Forms.LinkLabel();
            this.lb03 = new System.Windows.Forms.LinkLabel();
            this.lb13 = new System.Windows.Forms.LinkLabel();
            this.lb23 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // bt0
            // 
            this.bt0.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.bt0.Location = new System.Drawing.Point(0, 0);
            this.bt0.Name = "bt0";
            this.bt0.Size = new System.Drawing.Size(120, 96);
            this.bt0.TabIndex = 0;
            this.bt0.Click += new System.EventHandler(this.button1_Click);
            // 
            // bt2
            // 
            this.bt2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.bt2.Location = new System.Drawing.Point(0, 96);
            this.bt2.Name = "bt2";
            this.bt2.Size = new System.Drawing.Size(120, 96);
            this.bt2.TabIndex = 1;
            this.bt2.Click += new System.EventHandler(this.button1_Click);
            // 
            // bt3
            // 
            this.bt3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.bt3.Location = new System.Drawing.Point(119, 96);
            this.bt3.Name = "bt3";
            this.bt3.Size = new System.Drawing.Size(120, 96);
            this.bt3.TabIndex = 3;
            this.bt3.Click += new System.EventHandler(this.button1_Click);
            // 
            // bt1
            // 
            this.bt1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.bt1.Location = new System.Drawing.Point(119, 0);
            this.bt1.Name = "bt1";
            this.bt1.Size = new System.Drawing.Size(120, 96);
            this.bt1.TabIndex = 2;
            this.bt1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lb00
            // 
            this.lb00.BackColor = System.Drawing.Color.Silver;
            this.lb00.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb00.ForeColor = System.Drawing.Color.Black;
            this.lb00.Location = new System.Drawing.Point(13, 16);
            this.lb00.Name = "lb00";
            this.lb00.Size = new System.Drawing.Size(100, 22);
            this.lb00.TabIndex = 4;
            this.lb00.TabStop = false;
            this.lb00.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb00.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb10
            // 
            this.lb10.BackColor = System.Drawing.Color.Silver;
            this.lb10.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb10.ForeColor = System.Drawing.Color.Black;
            this.lb10.Location = new System.Drawing.Point(13, 36);
            this.lb10.Name = "lb10";
            this.lb10.Size = new System.Drawing.Size(100, 20);
            this.lb10.TabIndex = 5;
            this.lb10.TabStop = false;
            this.lb10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb10.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb20
            // 
            this.lb20.BackColor = System.Drawing.Color.Silver;
            this.lb20.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb20.ForeColor = System.Drawing.Color.Black;
            this.lb20.Location = new System.Drawing.Point(13, 56);
            this.lb20.Name = "lb20";
            this.lb20.Size = new System.Drawing.Size(100, 20);
            this.lb20.TabIndex = 6;
            this.lb20.TabStop = false;
            this.lb20.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb20.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb01
            // 
            this.lb01.BackColor = System.Drawing.Color.Silver;
            this.lb01.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb01.ForeColor = System.Drawing.Color.Black;
            this.lb01.Location = new System.Drawing.Point(126, 18);
            this.lb01.Name = "lb01";
            this.lb01.Size = new System.Drawing.Size(100, 20);
            this.lb01.TabIndex = 7;
            this.lb01.Text = "\r\n";
            this.lb01.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb01.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb11
            // 
            this.lb11.BackColor = System.Drawing.Color.Silver;
            this.lb11.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb11.ForeColor = System.Drawing.Color.Black;
            this.lb11.Location = new System.Drawing.Point(126, 38);
            this.lb11.Name = "lb11";
            this.lb11.Size = new System.Drawing.Size(100, 20);
            this.lb11.TabIndex = 8;
            this.lb11.Text = "\r\n";
            this.lb11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb11.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb21
            // 
            this.lb21.BackColor = System.Drawing.Color.Silver;
            this.lb21.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb21.ForeColor = System.Drawing.Color.Black;
            this.lb21.Location = new System.Drawing.Point(126, 58);
            this.lb21.Name = "lb21";
            this.lb21.Size = new System.Drawing.Size(100, 20);
            this.lb21.TabIndex = 9;
            this.lb21.Text = "\r\n";
            this.lb21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb21.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb02
            // 
            this.lb02.BackColor = System.Drawing.Color.Silver;
            this.lb02.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb02.ForeColor = System.Drawing.Color.Black;
            this.lb02.Location = new System.Drawing.Point(13, 115);
            this.lb02.Name = "lb02";
            this.lb02.Size = new System.Drawing.Size(100, 20);
            this.lb02.TabIndex = 10;
            this.lb02.Text = "\r\n";
            this.lb02.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb02.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb12
            // 
            this.lb12.BackColor = System.Drawing.Color.Silver;
            this.lb12.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb12.ForeColor = System.Drawing.Color.Black;
            this.lb12.Location = new System.Drawing.Point(13, 135);
            this.lb12.Name = "lb12";
            this.lb12.Size = new System.Drawing.Size(100, 20);
            this.lb12.TabIndex = 11;
            this.lb12.Text = "\r\n";
            this.lb12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb12.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb22
            // 
            this.lb22.BackColor = System.Drawing.Color.Silver;
            this.lb22.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb22.ForeColor = System.Drawing.Color.Black;
            this.lb22.Location = new System.Drawing.Point(13, 155);
            this.lb22.Name = "lb22";
            this.lb22.Size = new System.Drawing.Size(100, 20);
            this.lb22.TabIndex = 12;
            this.lb22.Text = "\r\n";
            this.lb22.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb22.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb03
            // 
            this.lb03.BackColor = System.Drawing.Color.Silver;
            this.lb03.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb03.ForeColor = System.Drawing.Color.Black;
            this.lb03.Location = new System.Drawing.Point(126, 115);
            this.lb03.Name = "lb03";
            this.lb03.Size = new System.Drawing.Size(100, 20);
            this.lb03.TabIndex = 13;
            this.lb03.Text = "\r\n";
            this.lb03.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb03.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb13
            // 
            this.lb13.BackColor = System.Drawing.Color.Silver;
            this.lb13.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb13.ForeColor = System.Drawing.Color.Black;
            this.lb13.Location = new System.Drawing.Point(126, 135);
            this.lb13.Name = "lb13";
            this.lb13.Size = new System.Drawing.Size(100, 20);
            this.lb13.TabIndex = 14;
            this.lb13.Text = "\r\n";
            this.lb13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb13.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb23
            // 
            this.lb23.BackColor = System.Drawing.Color.Silver;
            this.lb23.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lb23.ForeColor = System.Drawing.Color.Black;
            this.lb23.Location = new System.Drawing.Point(126, 155);
            this.lb23.Name = "lb23";
            this.lb23.Size = new System.Drawing.Size(100, 20);
            this.lb23.TabIndex = 15;
            this.lb23.Text = "\r\n";
            this.lb23.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb23.Click += new System.EventHandler(this.button1_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(239, 192);
            this.Controls.Add(this.lb23);
            this.Controls.Add(this.lb13);
            this.Controls.Add(this.lb03);
            this.Controls.Add(this.lb22);
            this.Controls.Add(this.lb12);
            this.Controls.Add(this.lb02);
            this.Controls.Add(this.lb21);
            this.Controls.Add(this.lb11);
            this.Controls.Add(this.lb01);
            this.Controls.Add(this.lb20);
            this.Controls.Add(this.lb10);
            this.Controls.Add(this.lb00);
            this.Controls.Add(this.bt3);
            this.Controls.Add(this.bt1);
            this.Controls.Add(this.bt2);
            this.Controls.Add(this.bt0);
            this.MinimizeBox = false;
            this.Name = "ControlForm";
            this.Text = "SimpleAgro_Print" + " V: v_03";
            this.Load += new System.EventHandler(this.ControlForm_Load);
            this.Resize += new System.EventHandler(this.ControlForm_Resize);
            this.ResumeLayout(false);

		}
		#endregion

        /// <summary>
        /// This function does the (initial) scaling of the form
        /// by re-setting the related parameters (if required) &
        /// then calling the Scale(...) internally. 
        /// </summary>
        /// 
        public void DoScale()
        {
            if (Screen.PrimaryScreen.Bounds.Width > Screen.PrimaryScreen.Bounds.Height)
            {
                bPortrait = false; // If the display orientation is not portrait (so it's landscape), set the flag to false.
            }

            if (this.WindowState == FormWindowState.Maximized)    // If the form is maximized by default.
            {
                this.bSkipMaxLen = true; // we need to skip the max. length restriction
            }

            if ((Symbol.Win32.PlatformType.IndexOf("WinCE") != -1) || (Symbol.Win32.PlatformType.IndexOf("WindowsCE") != -1) || (Symbol.Win32.PlatformType.IndexOf("Windows CE") != -1)) // Only on Windows CE devices
            {
                this.resWidthReference = this.Width;   // The width of the form at design time (in pixels) is obtained from the platorm.
                this.resHeightReference = this.Height; // The height of the form at design time (in pixels) is obtained from the platform.
            }

            Scale(this); // Initial scaling of the GUI
        }

        /// <summary>
        /// This function scales the given Form & its child controls in order to
        /// make them completely viewable, based on the screen width & height.
        /// </summary>
        private static void Scale(ControlForm frm)
        {
            int PSWAW = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;    // The width of the working area (in pixels).
            int PSWAH = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;   // The height of the working area (in pixels).

            // The entire screen has been taken in to account below 
            // in order to decide the half (S)VGA settings etc.
            if (!((Screen.PrimaryScreen.Bounds.Width <= (1.5) * (Screen.PrimaryScreen.Bounds.Height))
            && (Screen.PrimaryScreen.Bounds.Height <= (1.5) * (Screen.PrimaryScreen.Bounds.Width))))
            {
                if ((Screen.PrimaryScreen.Bounds.Width) > (Screen.PrimaryScreen.Bounds.Height))
                {
                    PSWAW = (int)((1.33) * PSWAH);  // If the width/height ratio goes beyond 1.5,
                    // the (longer) effective width is made shorter.
                }
            }

            System.Drawing.Graphics graphics = frm.CreateGraphics();
            float dpiX = graphics.DpiX; // Get the horizontal DPI value.
            if (frm.bInitialScale == true) // If an initial scale (from scratch)
            {
                if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1) // If the platform is either Pocket PC or Windows Mobile
                {
                    frm.Width = PSWAW;  // Set the form width. However this setting
                    // would be the ultimate one for Pocket PC (& Windows Mobile)devices.
                    // Just for the sake of consistency, it's explicitely specified here.
                }
                else
                {
                    frm.Width = (int)((frm.Width) * (PSWAW)) / (frm.resWidthReference); // Set the form width for others (Windows CE devices).

                }
            }
            if ((frm.Width <= maxLength * dpiX) || frm.bSkipMaxLen == true) // The calculation of the width & left values for each control
            // without taking the maximum length restriction into consideration.
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {
                    cntrl.Width = ((cntrl.Width) * (frm.Width)) / (frm.resWidthReference);
                    cntrl.Left = ((cntrl.Left) * (frm.Width)) / (frm.resWidthReference);

                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Width = (((cntrl2.Width) * (frm.Width)) / (frm.resWidthReference));
                                cntrl2.Left = (((cntrl2.Left) * (frm.Width)) / (frm.resWidthReference));
                            }
                        }
                    }
                }

            }
            else
            {   // The calculation of the width & left values for each control
                // with the maximum length restriction taken into consideration.
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {
                    cntrl.Width = (int)(((cntrl.Width) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));
                    cntrl.Left = (int)(((cntrl.Left) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));

                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Width = (int)(((cntrl2.Width) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));
                                cntrl2.Left = (int)(((cntrl2.Left) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));
                            }
                        }
                    }
                }

                frm.Width = (int)((frm.Width) * (maxLength * dpiX)) / (frm.Width);

            }

            frm.resWidthReference = frm.Width; // Set the reference width to the new value.


            // A similar calculation is performed below for the height & top values for each control ...

            if (!((Screen.PrimaryScreen.Bounds.Width <= (1.5) * (Screen.PrimaryScreen.Bounds.Height))
            && (Screen.PrimaryScreen.Bounds.Height <= (1.5) * (Screen.PrimaryScreen.Bounds.Width))))
            {
                if ((Screen.PrimaryScreen.Bounds.Height) > (Screen.PrimaryScreen.Bounds.Width))
                {
                    PSWAH = (int)((1.33) * PSWAW);
                }

            }

            float dpiY = graphics.DpiY;

            if (frm.bInitialScale == true)
            {
                if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
                {
                    frm.Height = PSWAH;
                }
                else
                {
                    frm.Height = (int)((frm.Height) * (PSWAH)) / (frm.resHeightReference);

                }
            }

            if ((frm.Height <= maxLength * dpiY) || frm.bSkipMaxLen == true)
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {

                    cntrl.Height = ((cntrl.Height) * (frm.Height)) / (frm.resHeightReference);
                    cntrl.Top = ((cntrl.Top) * (frm.Height)) / (frm.resHeightReference);


                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Height = ((cntrl2.Height) * (frm.Height)) / (frm.resHeightReference);
                                cntrl2.Top = ((cntrl2.Top) * (frm.Height)) / (frm.resHeightReference);
                            }
                        }
                    }

                }

            }
            else
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {

                    cntrl.Height = (int)(((cntrl.Height) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));
                    cntrl.Top = (int)(((cntrl.Top) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));


                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Height = (int)(((cntrl2.Height) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));
                                cntrl2.Top = (int)(((cntrl2.Top) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));
                            }
                        }
                    }

                }

                frm.Height = (int)((frm.Height) * (maxLength * dpiY)) / (frm.Height);

            }

            frm.resHeightReference = frm.Height;

            if (frm.bInitialScale == true)
            {
                frm.bInitialScale = false; // If this was the initial scaling (from scratch), it's now complete.
            }
            if (frm.bSkipMaxLen == true)
            {
                frm.bSkipMaxLen = false; // No need to consider the maximum length restriction now.
            }


        }

        /// <summary>
		/// The main entry point for the application.
		/// </summary>

        private void ControlForm_Resize(object sender, EventArgs e)
        {
            if (bInitialScale == true)
            {
                return; // Return if the initial scaling (from scratch)is not complete.
            }

            if (Screen.PrimaryScreen.Bounds.Width > Screen.PrimaryScreen.Bounds.Height) // If landscape orientation
            {
                if (bPortrait != false) // If an orientation change has occured to landscape
                {
                    bPortrait = false; // Set the orientation flag accordingly.
                    bInitialScale = true; // An initial scaling is required due to orientation change.
                    Scale(this); // Scale the GUI.
                }
                else
                {   // No orientation change has occured
                    bSkipMaxLen = true; // Initial scaling is now complete, so skipping the max. length restriction is now possible.
                    Scale(this); // Scale the GUI.
                }
            }
            else
            {
                // Similarly for the portrait orientation...
                if (bPortrait != true)
                {
                    bPortrait = true;
                    bInitialScale = true;
                    Scale(this);
                }
                else
                {
                    bSkipMaxLen = true;
                    Scale(this);
                }
            }

        }

        #endregion

        static void Main() 
		{
            ControlForm cf = new ControlForm();
            cf.DoScale();
            Application.Run(cf);
		}
		// Handles the user clicking on the Exit button
		private void ExitButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		// Handles the user clicking on the Play Beep button.

       
        private void button1_Click(object sender, EventArgs e)
        {
            if (!tieneBotones)
                return;

            Lib libreria = new Lib();
            int indexBt = -1;
            try
            {
                Button bt = (Button)sender;
                indexBt = int.Parse(bt.Name.Substring(2, 1));
            }
            catch
            {
                try
                {
                    LinkLabel bt = (LinkLabel)sender;
                    indexBt = int.Parse(bt.Name.Substring(3, 1));
                }
                catch 
                {
                    
                    
                }
            }
            string pathTemporal  = Path.GetTempFileName();
            FileStream fs = File.Create(pathTemporal);

            responce_DB_ETIQUETA_VENTANA boton = null;
            foreach (responce_DB_ETIQUETA_VENTANA item in response)
            {
                if(item.Pantalla_zpl == indexBt.ToString())
                {
                    boton = item;
                    break;
                }
 
            }
            
            //consumir servicio que me devuelva correlativo

            //remplazar por valor en boton.ZPL
                 //boton.id es el id de la tabla eiqueta 
            //
            

            Lib l = new Lib();
            rfcNET cliente = new rfcNET();
            request_DB_ETIQUETA_CORRELATIVO req = new request_DB_ETIQUETA_CORRELATIVO();

            responce_DB_ETIQUETA_CORRELATIVO resp_corr = new responce_DB_ETIQUETA_CORRELATIVO();
            
            req.Ip = l.GetIP();//"192.168.0.13";

            
            try
            {
                req.Id = boton.id;
            }
            catch 
            {

                return;
            }

           
            string Line = "";
            
            try
            {
                resp_corr = cliente.DB_ETIQUETA_CORRELATIVO(req);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("No se puede conectar al servicio SimpleAgro. Ex :" + e.Message);
                
            }

            //aca esta el valor del correlativo response.correlativo


            boton.ZPL = boton.ZPL.Replace("@corrrelativosmanzana", resp_corr.correlativo);
            int posicion_error1 = boton.ZPL.IndexOf("undefined");
            int posicion_error2 = boton.ZPL.IndexOf("@");

            // Verificar si se detectaron errores en las posiciones
            if (posicion_error1 > 0 || posicion_error2 > 0)
            {
                // Reproducir sonido de advertencia
                PlaySound("SystemExclamation", IntPtr.Zero, SND_ALIAS | SND_SYNC);

                // Construir mensaje de error con las posiciones encontradas
                string mensaje = "";

                if (posicion_error1 > 0)
                    mensaje += "undefined en posición: " + posicion_error1 + ". ";

                if (posicion_error2 > 0)
                    mensaje += "@ en posición: " + posicion_error2 + ". ";

                // Mostrar mensaje de advertencia
                MessageBox.Show("Se detectaron problemas en la etiqueta, favor revisar. Lineas de ERROR: " + mensaje);
            }
            if (resp_corr.tipo != "si")
            return;


            Byte[] info = new UTF8Encoding(true).GetBytes(boton.ZPL);
            // Add some information to the file.
            fs.Write(info, 0, info.Length);
            fs.Close();
            //fs.Dispose();
            //RawPrinterHelper.SendFileToPrinter("10.99.99.148", pathTemporal);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //clientSocket.NoDelay = true;
            //"10.99.99.120"
            IPAddress ip = IPAddress.Parse(boton.Ip_Zebra);
            IPEndPoint ipep = new IPEndPoint(ip, 9100);
            try
            {
                clientSocket.Connect(ipep);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("No se puede conectar a Impresora "+ip+". Ex :" + ex.Message);
                return;
            }

            byte[] fileBytes1 = info;//File.ReadAllBytes(@"c:\temp\dc1.txt");
            clientSocket.Send(fileBytes1);
            //Thread.Sleep(3000);
            clientSocket.Close();

            EnviaDatos(boton.ip_Pantalla, boton.Kilos_Materiales.Replace(",","."), boton.Pantalla_zpl, boton.Proceso);
            
            
            //string zplTemp = p.Botones[indexBt].ZPL;
            //libreria.UpdateKilosEtiquetas(p.Proceso, p.Botones[indexBt].Kg);
            
            //WebService1 ws = new WebService1();

            //ws.printPantalla(1, zplTemp, p.Printer);

            

        }

        private void ControlForm_Load(object sender, System.EventArgs e)
        {
            bt0.Enabled = false;
            bt1.Enabled = false;
            bt2.Enabled = false;
            bt3.Enabled = false;
            Refresca();
        }

        public void EnviaDatos(string ipPantalla, string kilos, string posicion, string proceso)
        {
            rfcNET cliente = new rfcNET();
            request_DB_PROCESO_MOD req = new request_DB_PROCESO_MOD();
            req.ip_pantalla = ipPantalla;
            req.kilos = kilos;
            req.posicion = posicion;
            req.Proceso = proceso;


            responce_DB_PROCESO_MOD response = cliente.DB_PROCESO_MOD(req);

            
 
        }

        public void Refresca()
        {
            Lib l = new Lib();
            rfcNET cliente = new rfcNET();
            request_DB_ETIQUETA_VENTANA req = new request_DB_ETIQUETA_VENTANA();

            req.Ip = l.GetIP();//"192.168.0.13";
            //req.Ip = "10.100.21.10";
            string sal = "";
            string Line = "";
            try
            {
                response = cliente.DB_ETIQUETA_VENTANA(req);
            }
            catch (Exception e)
            {
                //MessageBox.Show("No se puede conectar al servicio SimpleAgro. Ex :" + e.Message);
                this.Text = " Sin Conexion" + " V: v_03"; 
                if (bt0.Enabled || bt1.Enabled || bt2.Enabled || bt3.Enabled)
                {
                    //MessageBox.Show("Pantalla no se encuentra configurada");
                    this.Text = " Sin Conexion" + " V: v_03"; 

                    bt0.Enabled = false;
                    lb00.Enabled = false;
                    lb10.Enabled = false;
                    lb20.Enabled = false;
                    bt1.Enabled = false;
                    lb01.Enabled = false;
                    lb11.Enabled = false;
                    lb21.Enabled = false;
                    bt2.Enabled = false;
                    lb02.Enabled = false;
                    lb12.Enabled = false;
                    lb22.Enabled = false;
                    bt3.Enabled = false;
                    lb03.Enabled = false;
                    lb13.Enabled = false;
                    lb23.Enabled = false;

                    lb00.Text = "";
                    lb10.Text = "";
                    lb20.Text = "";

                    lb01.Text = "";
                    lb11.Text = "";
                    lb21.Text = "";

                    lb02.Text = "";
                    lb12.Text = "";
                    lb22.Text = "";


                    lb03.Text = "";
                    lb13.Text = "";
                    lb23.Text = "";
                }
                return;
            }
            foreach (responce_DB_ETIQUETA_VENTANA item in response)
            {
                sal = item.Salida;
                Line = item.Linea;
                //timer1.Interval = item.Refresco * 1000; // cambiar 1 por nuevo parametro

                //if (item.Refresco == -1)
                //{
                //    timer1.Enabled = false;
                //}
            }
            int line = -1;
            try
            {
                line = Convert.ToInt32(Line);
                line = line + 1;
            }
            catch 
            {
                line = 0;
            }
            Line = line.ToString();
            this.Text = "L:"+Line+" S:"+sal+" "+req.Ip + " V: v_03";
            tieneBotones= false;
            foreach (responce_DB_ETIQUETA_VENTANA item in response)
            {
                tieneBotones = true;
                int stock = int.Parse(item.Stock);
                switch (int.Parse(item.Pantalla_zpl))
                {   
                    case 0:
                        if (stock == -1 || stock > 0)
                        {
                            //string textoPantalla = item.Calibre + "-" + item.Calidad + "-" + item.Kilos_Materiales;
                            //if (stock > 0)
                            //    textoPantalla += "\n" + item.Stock;

                            //bt0.Text = textoPantalla;
                            lb00.Text = item.Calibre;
                            lb10.Text = item.Calidad;
                            lb20.Text = item.Kilos_Materiales;
                            bt0.Enabled = true;
                            lb00.Enabled = true;
                            lb10.Enabled = true;
                            lb20.Enabled = true;

                        }
                        else
                        {
                            bt0.Enabled = false;
                            lb00.Text = "";
                            lb10.Text = "";
                            lb20.Text = "";
                            lb00.Enabled = false;
                            lb10.Enabled = false;
                            lb20.Enabled = false;
                        }
                        break;
                    case 1:
                        if (stock == -1 || stock > 0)
                        {
                            //string textoPantalla = item.Calibre + "-" + item.Calidad + "-" + item.Kilos_Materiales;
                            //if (stock > 0)
                            //    textoPantalla += "\n" + item.Stock;

                           // bt1.Text = textoPantalla;
                            lb01.Text = item.Calibre;
                            lb11.Text = item.Calidad;
                            lb21.Text = item.Kilos_Materiales;
                            bt1.Enabled = true;
                            lb01.Enabled = true;
                            lb11.Enabled = true;
                            lb21.Enabled = true;

                        }
                        else
                        {
                            bt1.Enabled = false;
                            lb01.Text = "";
                            lb11.Text = "";
                            lb21.Text = "";
                            lb01.Enabled = false;
                            lb11.Enabled = false;
                            lb21.Enabled = false;
                        }
                        break;
                    case 2:
                        if (stock == -1 || stock > 0)
                        {
                            //string textoPantalla = item.Calibre + "-" + item.Calidad + "-" + item.Kilos_Materiales;
                            //if (stock > 0)
                            //    textoPantalla += "\n" + item.Stock;

                            //bt2.Text = textoPantalla;
                            lb02.Text = item.Calibre;
                            lb12.Text = item.Calidad;
                            lb22.Text = item.Kilos_Materiales;
                            bt2.Enabled = true;
                            lb02.Enabled = true;
                            lb12.Enabled = true;
                            lb22.Enabled = true;
                        }
                        else
                        {
                            bt2.Enabled = false;
                            lb02.Text = "";
                            lb12.Text = "";
                            lb22.Text = "";
                            lb02.Enabled = false;
                            lb12.Enabled = false;
                            lb22.Enabled = false;
                        }
                        break;
                    case 3:
                        if (stock == -1 || stock > 0)
                        {
                            //string textoPantalla = item.Calibre + "-" + item.Calidad + "-" + item.Kilos_Materiales;
                            //if (stock > 0)
                            //    textoPantalla += "\n" + item.Stock;

                            //bt3.Text = textoPantalla;
                            lb03.Text = item.Calibre;
                            lb13.Text = item.Calidad;
                            lb23.Text = item.Kilos_Materiales;
                            bt3.Enabled = true;
                            lb03.Enabled = true;
                            lb13.Enabled = true;
                            lb23.Enabled = true;

                        }
                        else
                        {
                            bt3.Enabled = false;
                            lb03.Text = "";
                            lb13.Text = "";
                            lb23.Text = "";
                            lb03.Enabled = false;
                            lb13.Enabled = false;
                            lb23.Enabled = false;
                        }
                        break;

                    default:
                        

                        break;
                }


            }

            if (!tieneBotones)
            {
                if (bt0.Enabled || bt1.Enabled || bt2.Enabled || bt3.Enabled)
                {
                    //MessageBox.Show("Pantalla no se encuentra configurada");
                    this.Text = " Sin Conexion" + " V: v_03"; 
                    bt0.Enabled = false;
                    lb00.Enabled = false;
                    lb10.Enabled = false;
                    lb20.Enabled = false;
                    bt1.Enabled = false;
                    lb01.Enabled = false;
                    lb11.Enabled = false;
                    lb21.Enabled = false;
                    bt2.Enabled = false;
                    lb02.Enabled = false;
                    lb12.Enabled = false;
                    lb22.Enabled = false;
                    bt3.Enabled = false;
                    lb03.Enabled = false;
                    lb13.Enabled = false;
                    lb23.Enabled = false;
                }
            }


            
            /*
            Lib libreria = new Lib();

            p = libreria.GetPantalla();

            bt0.Enabled = false;
            bt1.Enabled = false;
            bt2.Enabled = false;
            bt3.Enabled = false;

            switch (p.CantBotones)
            {
                case 1:
                    bt0.Text = "10";//p.Botones[0].Nombre;

                    bt0.Enabled = true;
                    break;
                case 2:
                    bt0.Text = "10";//p.Botones[0].Nombre;
                    bt1.Text = "11";//p.Botones[1].Nombre;

                    bt0.Enabled = true;
                    bt1.Enabled = true;
                    break;
                case 3:
                    bt0.Text = "10";//p.Botones[0].Nombre;
                    bt1.Text = "11";//p.Botones[1].Nombre;
                    bt2.Text = "12";//p.Botones[2].Nombre;

                    bt0.Enabled = true;
                    bt1.Enabled = true;
                    bt2.Enabled = true;
                    break;
                case 4:
                    bt0.Text = "10";//p.Botones[0].Nombre;
                    bt1.Text = "11";//p.Botones[1].Nombre;
                    bt2.Text = "12";//p.Botones[2].Nombre;
                    bt3.Text = "13";//p.Botones[2].Nombre;

                    bt0.Enabled = true;
                    bt1.Enabled = true;
                    bt2.Enabled = true;
                    bt3.Enabled = true;
                    break;
                default:
                    MessageBox.Show("Pantalla no se encuentra configurada");
                    bt0.Enabled = false;
                    bt1.Enabled = false;
                    bt2.Enabled = false;
                    bt3.Enabled = false;

                    break;
            }
             * */

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresca();
        }
	}
}
