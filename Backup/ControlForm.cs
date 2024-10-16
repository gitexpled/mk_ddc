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

namespace CS_AudioSample1
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ControlForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label BeeperVolumelabel;
		private Symbol.Audio.Controller MyAudioController=null;
		private System.Windows.Forms.DomainUpDown EarSaveDelaydomainUpDown;
		private System.Windows.Forms.Button AboutButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label EarSaveDelaylabel;
		private System.Windows.Forms.Button PlayWAVButton;
		private System.Windows.Forms.TextBox WaveFiletextBox;
		private System.Windows.Forms.Label WaveFileNameLabel;
		private System.Windows.Forms.Button PlayBeepButton;
		private System.Windows.Forms.TrackBar BeeperVolumetrackBar;
		private System.Windows.Forms.Button ExitButton;
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

        private int resHeightReference = 217;  // The (cached) height of the form.
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
			this.PlayBeepButton.Focus();
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
            this.BeeperVolumelabel = new System.Windows.Forms.Label();
            this.EarSaveDelaydomainUpDown = new System.Windows.Forms.DomainUpDown();
            this.AboutButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EarSaveDelaylabel = new System.Windows.Forms.Label();
            this.PlayWAVButton = new System.Windows.Forms.Button();
            this.WaveFiletextBox = new System.Windows.Forms.TextBox();
            this.WaveFileNameLabel = new System.Windows.Forms.Label();
            this.PlayBeepButton = new System.Windows.Forms.Button();
            this.BeeperVolumetrackBar = new System.Windows.Forms.TrackBar();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BeeperVolumelabel
            // 
            this.BeeperVolumelabel.Location = new System.Drawing.Point(20, 7);
            this.BeeperVolumelabel.Name = "BeeperVolumelabel";
            this.BeeperVolumelabel.Size = new System.Drawing.Size(134, 16);
            this.BeeperVolumelabel.Text = "Beeper Volume";
            // 
            // EarSaveDelaydomainUpDown
            // 
            this.EarSaveDelaydomainUpDown.Location = new System.Drawing.Point(20, 131);
            this.EarSaveDelaydomainUpDown.Name = "EarSaveDelaydomainUpDown";
            this.EarSaveDelaydomainUpDown.Size = new System.Drawing.Size(144, 24);
            this.EarSaveDelaydomainUpDown.TabIndex = 3;
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(19, 160);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(88, 24);
            this.AboutButton.TabIndex = 4;
            this.AboutButton.Text = "About";
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            this.AboutButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutButton_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(123, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            // 
            // EarSaveDelaylabel
            // 
            this.EarSaveDelaylabel.Location = new System.Drawing.Point(20, 114);
            this.EarSaveDelaylabel.Name = "EarSaveDelaylabel";
            this.EarSaveDelaylabel.Size = new System.Drawing.Size(144, 16);
            this.EarSaveDelaylabel.Text = "Ear Save Delay";
            // 
            // PlayWAVButton
            // 
            this.PlayWAVButton.Location = new System.Drawing.Point(148, 87);
            this.PlayWAVButton.Name = "PlayWAVButton";
            this.PlayWAVButton.Size = new System.Drawing.Size(72, 24);
            this.PlayWAVButton.TabIndex = 2;
            this.PlayWAVButton.Text = "Play .WAV";
            this.PlayWAVButton.Click += new System.EventHandler(this.PlayWAVButton_Click);
            this.PlayWAVButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PlayWAVButton_KeyDown);
            // 
            // WaveFiletextBox
            // 
            this.WaveFiletextBox.Location = new System.Drawing.Point(20, 87);
            this.WaveFiletextBox.Name = "WaveFiletextBox";
            this.WaveFiletextBox.Size = new System.Drawing.Size(112, 23);
            this.WaveFiletextBox.TabIndex = 1;
            this.WaveFiletextBox.Text = "\\Windows\\Alarm1.wav";
            // 
            // WaveFileNameLabel
            // 
            this.WaveFileNameLabel.Location = new System.Drawing.Point(20, 71);
            this.WaveFileNameLabel.Name = "WaveFileNameLabel";
            this.WaveFileNameLabel.Size = new System.Drawing.Size(136, 16);
            this.WaveFileNameLabel.Text = ".WAV File Name";
            // 
            // PlayBeepButton
            // 
            this.PlayBeepButton.Location = new System.Drawing.Point(148, 29);
            this.PlayBeepButton.Name = "PlayBeepButton";
            this.PlayBeepButton.Size = new System.Drawing.Size(72, 24);
            this.PlayBeepButton.TabIndex = 0;
            this.PlayBeepButton.Text = "Play Beep";
            this.PlayBeepButton.Click += new System.EventHandler(this.PlayBeepButton_Click);
            this.PlayBeepButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PlayBeepButton_KeyDown);
            // 
            // BeeperVolumetrackBar
            // 
            this.BeeperVolumetrackBar.LargeChange = 1;
            this.BeeperVolumetrackBar.Location = new System.Drawing.Point(12, 21);
            this.BeeperVolumetrackBar.Name = "BeeperVolumetrackBar";
            this.BeeperVolumetrackBar.Size = new System.Drawing.Size(131, 45);
            this.BeeperVolumetrackBar.TabIndex = 11;
            this.BeeperVolumetrackBar.ValueChanged += new System.EventHandler(this.BeeperVolumetrackBar_ValueChanged);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(134, 160);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(88, 24);
            this.ExitButton.TabIndex = 5;
            this.ExitButton.Text = "Exit";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            this.ExitButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExitButton_KeyDown);
            // 
            // ControlForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(239, 192);
            this.Controls.Add(this.PlayBeepButton);
            this.Controls.Add(this.WaveFiletextBox);
            this.Controls.Add(this.PlayWAVButton);
            this.Controls.Add(this.EarSaveDelaydomainUpDown);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EarSaveDelaylabel);
            this.Controls.Add(this.WaveFileNameLabel);
            this.Controls.Add(this.BeeperVolumelabel);
            this.Controls.Add(this.BeeperVolumetrackBar);
            this.MinimizeBox = false;
            this.Name = "ControlForm";
            this.Text = "CS_AudioSample1";
            this.Load += new System.EventHandler(this.ControlForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ControlForm_Closing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ControlForm_KeyUp);
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

		static void Main() 
		{
            ControlForm cf = new ControlForm();
            cf.DoScale();
            Application.Run(cf);
		}

		
		private void ControlForm_Load(object sender, System.EventArgs e)
		{
			//Select Device from device list
			Symbol.Audio.Device MyDevice=(Symbol.Audio.Device)Symbol.StandardForms.SelectDevice.Select(
				Symbol.Audio.Controller.Title,
				Symbol.Audio.Device.AvailableDevices);
            
			if(MyDevice ==null)
			{
				MessageBox.Show("No Device Selected", "SelectDevice");

				//close the form
				this.Close();

				return;
			}

			//check the device type
			switch (MyDevice.AudioType)
			{
				//if standard device
				case Symbol.Audio.AudioType.StandardAudio:
					MyAudioController = new Symbol.Audio.StandardAudio(MyDevice);
					break;
					
				//if simulated device
				case Symbol.Audio.AudioType.SimulatedAudio:
					MyAudioController = new Symbol.Audio.SimulatedAudio(MyDevice);
					break;

				default :
					throw new Symbol.Exceptions.InvalidDataTypeException("Unknown Device Type");

			}

			// check for the presence of speaker
			terminalInfo = new Symbol.ResourceCoordination.TerminalInfo();

			// hide the wave file options if the device supports only a beeper
			if(terminalInfo.ConfigData != null)
			{
				if(terminalInfo.ConfigData.AUDIO == (int)Symbol.ResourceCoordination.AudioTypes.BEEPER)
				{
					this.WaveFileNameLabel.Hide();
					this.WaveFiletextBox.Hide();
					this.PlayWAVButton.Hide();
				}
			}

			//add event handler   to handle event change
			this.MyAudioController.ChangeNotify+=new EventHandler(MyAudioController_ChangeNotify);

			if(this.MyAudioController.BeeperVolumeLevels>0)
			{
				this.BeeperVolumetrackBar.Minimum=0;
				this.BeeperVolumetrackBar.Maximum=this.MyAudioController.BeeperVolumeLevels-1;
                this.BeeperVolumetrackBar.Value=this.MyAudioController.BeeperVolume;
				
				this.label1.Text=this.BeeperVolumetrackBar.Minimum.ToString();
				this.label2.Text=this.BeeperVolumetrackBar.Maximum.ToString();
				this.BeeperVolumelabel.Show();
				this.BeeperVolumetrackBar.Show();
				this.label1.Show();
				this.label2.Show();
			}
			else
			{
				this.BeeperVolumelabel.Hide();
				this.BeeperVolumetrackBar.Hide();
				this.label1.Hide();
				this.label2.Hide();
			}

			if(this.MyAudioController.EarSaveDelay>0)
			{
				this.EarSaveDelaydomainUpDown.Text=this.MyAudioController.EarSaveDelay.ToString();
				this.EarSaveDelaylabel.Show();
				this.EarSaveDelaydomainUpDown.Show();
			}
			else
			{
				this.EarSaveDelaylabel.Hide();
				this.EarSaveDelaydomainUpDown.Hide();
			}

			// Add MainMenu if Pocket PC
			if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
			{
				this.Menu = new MainMenu();
			}
		}

		private void MyAudioController_ChangeNotify(object sender, System.EventArgs  e)
		{
			//Get the next status of audio controller
			Symbol.Audio.AudioStatus MyStatus=this.MyAudioController.GetNextStatus ();

			if(MyStatus != null)
			{
				switch(MyStatus.Change)
				{
					case Symbol.Audio.ChangeType.BeeperVolume://Beeper volume change
						this.BeeperVolumetrackBar.Value=this.MyAudioController.BeeperVolume;
						break;

					case Symbol.Audio.ChangeType.EarSaveDelay://ESD Change
						this.EarSaveDelaydomainUpDown.Text=this.MyAudioController.EarSaveDelay.ToString();
						break;
				}
			}
		}

		// Handles the user clicking on the About button.
		private void AboutButton_Click(object sender, System.EventArgs e)
		{
			string sVerInfo="CS_AudioSample1 C# Audio Control Sample 1"+"\r\n"+"\n"+
				"CAPI Version\t"+this.MyAudioController.Version.CAPIVersion+"\r\n"+
				"Notify Version\t"+this.MyAudioController.Version.NotifyAPIVersion +"\r\n"+
				"Assembly Version\t"+this.MyAudioController.Version.AssemblyVersion;
 
			//show the about dialog
			MessageBox.Show(sVerInfo, "About");

			// we need to ensure that the keyboard focus is retained after the dismissal of the about box.
			this.AboutButton.Focus();
		}

		// Handles the user clicking on the Exit button
		private void ExitButton_Click(object sender, System.EventArgs e)
		{
			this.MyAudioController.Dispose();
			this.Close();
		}

		// Handles the user clicking on the Play Beep button.
		private void PlayBeepButton_Click(object sender, System.EventArgs e)
		{
			int Duration=1500;//millisec
			int Frequency=2670;//hz
			
			try
			{
                this.MyAudioController.PlayAudio(Duration, Frequency);//play Default beep
            }
			catch
			{
                MessageBox.Show("PlayAudio failed. Hardware may not be present", "PlayAudio");
            }
		}

		// Handles the user clicking on the PlayWAV button.
		private void PlayWAVButton_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(this.WaveFiletextBox.Text.Length!=0)
					this.MyAudioController.PlayWaveFile(this.WaveFiletextBox.Text);
				else
					MessageBox.Show("Empty WaveFileName","WaveFileName");
			}
			catch
			{
				MessageBox.Show("PlayWaveFile failed. Wave file or speaker may not be present.", "PlayWaveFile");
			}
		}

		private void BeeperVolumetrackBar_ValueChanged(object sender, System.EventArgs e)
		{
			this.MyAudioController.BeeperVolume=this.BeeperVolumetrackBar.Value;
		}

		// Handling the PlayBeepButton on KeyUp in case the enter button is held down for
		// a long period.
		private void PlayBeepButton_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == (char)13)
				PlayBeepButton_Click(this, e);
		}

		// Handling AboutButton on KeyDown so that when the about dialog is dismissed (by
		// clicking enter) the handler is not triggered 
		// This would happen if the trigger was on KeyUp since the About button immediately
		// given focus after dismissing the about dialog
		private void AboutButton_KeyDown(object sender, KeyEventArgs e)
		{
			// Checks if the key pressed was an enter button (character code 13)
			if (e.KeyValue == (char)13)
				AboutButton_Click(this, e);
		}

		// Handling the PlayWAVButton on KeyUp in case the enter button is held down for
		// a long period.
		private void PlayWAVButton_KeyDown(object sender, KeyEventArgs e)
		{
			// Checks if the key pressed was an enter button (character code 13)
			if (e.KeyValue == (char)13)
				this.PlayWAVButton_Click(this, e);
		}

		// Handles the user selecting the Exit Button with the keyboard
		private void ExitButton_KeyDown(object sender, KeyEventArgs e)
		{
			// Checks if the key pressed was an enter button (character code 13)
			if (e.KeyValue == (char)13)
				ExitButton_Click(this, e);
		}

		// This is necessary in the case where the keyboard focus is lost.
		// This can occur if another application is activated.
		private void ControlForm_KeyUp(object sender, KeyEventArgs e)
		{
			this.PlayBeepButton.Focus();
		}

        private void ControlForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.MyAudioController.Dispose();
            this.Close();
        }

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
	}
}
