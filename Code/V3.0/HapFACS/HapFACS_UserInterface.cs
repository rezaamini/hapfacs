/*
 * 
 * HapFACS Open Source Licence
 * ===========================
 * Copyright 2014 Florida International University Board of Trustees
 *
 * All HapFACS users per the software license must cite the following article in any work derived from HapFACS (available at http://ascl.cs.fiu.edu/publications.html):
 * R. Amini, and C. Lisetti, (2013). HapFACS: an Open Source API/Software to Generate FACS-Based Expressions for ECAs Animation and for Corpus Generation. In Proceedings of the Affective Computing and Intelligent Interactions, Geneva, 2013. 
 * 
 * Based on a work at haptek.com.
 *
 */

using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging; // For saving the snapshots to image files
using Microsoft.Expression; // For saving video
using Microsoft.Expression.Encoder; // For saving video
using Microsoft.Expression.Encoder.ScreenCapture; // For saving video
using Microsoft.Expression.Encoder.Live; // For saving video & audio
using Microsoft.Expression.Encoder.Devices;
using System.Runtime.InteropServices;
using Microsoft.Expression.Encoder.Profiles;
using System.Threading;
using System.Reflection; // For calling the methods by string name
using System.Timers; 

namespace HapFACS
{
    public partial class HapFACS_UserInterface : Form
	{
		#region Local variables

		HapFACS hapFACS = new HapFACS(); // HapFACS object which activates the AUs
        HapFACSEmotion emFACS = new HapFACSEmotion(); // HapFACSEmotion object which activates a combination of AUs to express different emotions

        private string path = Application.StartupPath;
        private string lastAU1_Intensity = "0";
        private string lastAU2_LeftIntensity = "0"; private string lastAU2_RightIntensity = "0";
        private string lastAU4_Intensity = "0";
        private string lastAU5_Intensity = "0";
        private string lastAU6_Intensity = "0";
        private string lastAU7_Intensity = "0";
		private string lastAU8_Intensity = "0";
        private string lastAU9_Intensity = "0";
        private string lastAU10_Intensity = "0";
        private string lastAU11_Intensity = "0";
        private string lastAU12_Intensity = "0";
        private string lastAU13_LeftIntensity = "0"; private string lastAU13_RightIntensity = "0";
        private string lastAU14_LeftIntensity = "0"; private string lastAU14_RightIntensity = "0";
        private string lastAU15_LeftIntensity = "0"; private string lastAU15_RightIntensity = "0";
        private string lastAU16_Intensity = "0";
        private string lastAU17_Intensity = "0";
        private string lastAU18_Intensity = "0";
        private string lastAU20_Intensity = "0";
        private string lastAU22_Intensity = "0";
        private string lastAU23_Intensity = "0";
        private string lastAU24_Intensity = "0";
        private string lastAU25_Intensity = "0";
        private string lastAU26_Intensity = "0";
        private string lastAU27_Intensity = "0";
        private string lastAU28_Intensity = "0";
        private string lastAU38_LeftIntensity = "0"; private string lastAU38_RightIntensity = "0";
        private string lastAU39_LeftIntensity = "0"; private string lastAU39_RightIntensity = "0";
        private string lastAU41_Intensity = "0";
        private string lastAU42_Intensity = "0";
        private string lastAU43_Intensity = "0";
        private string lastAU44_Intensity = "0";
        private string lastAU51_Intensity = "0";
        private string lastAU52_Intensity = "0";
        private string lastAU53_Intensity = "0";
        private string lastAU54_Intensity = "0";
        private string lastAU55_Intensity = "0";
        private string lastAU56_Intensity = "0";
        private string lastAU57_Intensity = "0";
        private string lastAU58_Intensity = "0";
        private string lastAU61_LeftIntensity = "0"; private string lastAU61_RightIntensity = "0";
        private string lastAU62_LeftIntensity = "0"; private string lastAU62_RightIntensity = "0";
        private string lastAU63_LeftIntensity = "0"; private string lastAU63_RightIntensity = "0";
        private string lastAU64_LeftIntensity = "0"; private string lastAU64_RightIntensity = "0";
        private string lastAU65_LeftIntensity = "0"; private string lastAU65_RightIntensity = "0";
        private string lastAU66_LeftIntensity = "0"; private string lastAU66_RightIntensity = "0";
        private string lastHappiness_Intensity = "0";
        private string lastSadness_Intensity = "0";
        private string lastAnger_Intensity = "0";
        private string lastFear_Intensity = "0";
        private string lastDisgust_Intensity = "0";
        private string lastContempt_Intensity = "0";
        private string lastPride_Intensity = "0";
        private string lastEmbarrassment_Intensity = "0";
        private string lastSurprise_Intensity = "0";

		private ArrayList characterList = new ArrayList();
		private ArrayList hairList = new ArrayList();

        ScreenCaptureJob screenCapture = new ScreenCaptureJob( );
		LiveDeviceSource deviceSource = null;
		LiveJob job = new LiveJob( );

		System.Timers.Timer myTimer; // The timer for the checking the speaking status of the character (it is used when we want to stop video recording).

		#endregion

		#region Constructor
		// Constructor
		public HapFACS_UserInterface()
        {
            InitializeComponent();
			System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)1;
			processCharacterDirectory( path + @"\Character" ); // Import all the character files in the Character folder
			processHairDirectory( path + @"\Hair" ); // Import all the hair files in the Haor folder
			Character.Sorted = true;
			Hair.Sorted = true;
			Character.SelectedItem = "Lola_torso.haptar"; //Character.SelectedItem = "Default.haptar";
			axActiveHaptekX1.HyperText = @"\load[file=[" + path + @"\Character\Torso\Lola_torso.haptar]]"; //@"\Character\Head\Default.haptar]]"; // Load the Default character
			axActiveHaptekX1.HyperText = @"\LoadBackGrnd[ file= [" + path + @"\Images\back.jpg]]"; // Load the default white background
            initializeCharacter();
        }

		#endregion

		#region Methods to initialize the form
		// Process all files in the Character directory and load the character files to the character list. 
		private void processCharacterDirectory( string targetDirectory )
		{
			// Process the list of files found in the directory. 
			string [ ] fileEntries = Directory.GetFiles( targetDirectory );
			foreach( string filePath in fileEntries )
			{
				if( filePath.ToLower( ).EndsWith( ".htr" ) || filePath.ToLower( ).EndsWith( ".haptar" ) )
				{
					if( Path.GetDirectoryName( filePath ).ToLower( ).Contains( "torso" ) )
					{
						if( !Path.GetFileName( filePath ).ToLower( ).Contains( "torso" ) )
						{
							System.IO.File.Move( filePath,
								Path.GetDirectoryName( filePath ) + @"\" + Path.GetFileNameWithoutExtension( filePath ) + "_torso" + Path.GetExtension( filePath ) );
						}
					}
					
					characterList.Add( filePath ); // Keep the complete path of the character files in a list
					Character.Items.Add( Path.GetFileName( filePath ) );
				}
			}
			string [ ] directoryEntries = Directory.GetDirectories( targetDirectory );
			foreach( string directoryPath in directoryEntries )
			{
				processCharacterDirectory( directoryPath );
			}
		}

		// Process all files in the Hair directory and load the hair files to the character list. 
		private void processHairDirectory( string targetDirectory )
		{
			// Process the list of files found in the directory. 
			string [ ] fileEntries = Directory.GetFiles( targetDirectory );
			foreach( string filePath in fileEntries )
			{
				if( filePath.ToLower( ).EndsWith( ".htr" ) || filePath.ToLower( ).EndsWith( ".haptar" ) )
				{
					hairList.Add(filePath); // Keep the complete path of the hair files in a list
					Hair.Items.Add( Path.GetFileName( filePath ) );
				}
			}
		}

		// Initialize the position of the character
		private void initializeCharacter()
        {
			string currentCharacter = Character.SelectedItem.ToString( ).Trim( );

			if( currentCharacter.ToLower( ).Contains( "torso" ) )
			{
				axActiveHaptekX1.HyperText = @"\Translate[x= 0 y=-10 z=3]";
				axActiveHaptekX1.HyperText = @"\Rotate[y= 10]";
			}
			else
				axActiveHaptekX1.HyperText = @"\Translate[x= 0 y=-5 z=-20]";
        }

		#endregion

		#region Helper methods
		// Loads the chracter and its hair files
		private void SetCharacterAndHair( )
		{
			if( Character.SelectedIndex >= 0 && Character.SelectedItem.ToString( ).Trim( ) != "No Character" )
			{
				foreach( string character in characterList )
				{
					if( character.EndsWith( Character.SelectedItem.ToString( ).Trim( ) ) )
					{
						axActiveHaptekX1.HyperText = @"\load [file=[" + character + "]]";
						break;
					}
				}
			}

			if( Hair.SelectedIndex >= 0 && Hair.SelectedItem.ToString( ).Trim( ) != "No Hair" )
			{
				foreach( string hair in hairList )
				{
					if( hair.EndsWith( Hair.SelectedItem.ToString( ).Trim( ) ) )
					{
						axActiveHaptekX1.HyperText = @"\load [file=[" + hair + "]]";
						break;
					}
				}
			}

			initializeCharacter( );
			axActiveHaptekX1.HyperText = @"\LoadBackGrnd[ file= [" + path + @"\Images\back.jpg]]";
		}

		// Enables the AU intensity combo box, AU intensity text box, AU intensity-level label, and AU intensity label
		private void enableIntensityControls( ComboBox AUIntensity, TrackBar AUIntensityTB, Label AUIntensityLevelLbl, Label AUIntensityLbl )
		{
			AUIntensity.Enabled = true;
			AUIntensityTB.Enabled = true;
			AUIntensityLevelLbl.Enabled = true;
			AUIntensityLbl.Enabled = true;
		}

		// Disables the AU intensity combo box, AU intensity text box, AU intensity-level label, and AU intensity label
		private void disableIntensityControls( ComboBox AUIntensity, TrackBar AUIntensityTB, Label AUIntensityLevelLbl, Label AUIntensityLbl )
		{
			AUIntensity.Enabled = false;
			AUIntensityTB.Enabled = false;
			AUIntensityLevelLbl.Enabled = false;
			AUIntensityLbl.Enabled = false;
		}

		// Makes visible the AU intensity combo box, AU intensity text box, AU intensity-level label, and AU intensity label
		private void visibleIntensityControls( ComboBox AUIntensity, TrackBar AUIntensityTB, Label AUIntensityLevelLbl, Label AUIntensityLbl )
		{
			AUIntensity.Visible = true;
			AUIntensityTB.Visible = true;
			AUIntensityLevelLbl.Visible = true;
			AUIntensityLbl.Visible = true;
		}

		// Makes invisible the AU intensity combo box, AU intensity text box, AU intensity-level label, and AU intensity label
		private void invisibleIntensityControls( ComboBox AUIntensity, TrackBar AUIntensityTB, Label AUIntensityLevelLbl, Label AUIntensityLbl )
		{
			AUIntensity.Visible = false;
			AUIntensityTB.Visible = false;
			AUIntensityLevelLbl.Visible = false;
			AUIntensityLbl.Visible = false;
		}

		// Switches the visibility of the intensity controls on and off
		private void switchIntensityControlsVisibility( ComboBox AUIntensity, TrackBar AUIntensityTB, Label AUIntensityLevelLbl, Label AUIntensityLbl )
		{
			if( AUIntensity.Enabled )
			{
				disableIntensityControls( AUIntensity, AUIntensityTB, AUIntensityLevelLbl, AUIntensityLbl );
			}
			else
			{
				enableIntensityControls( AUIntensity, AUIntensityTB, AUIntensityLevelLbl, AUIntensityLbl );
			}
		}

		// Locates the controls in the appropriate positions
		private void locateControls( TrackBar AUIntensityTB, Label AUIntensityLevelLbl, Label AUIntensityLbl, int location )
		{
			AUIntensityTB.Location = new Point( location, AUIntensityTB.Location.Y );
			AUIntensityLbl.Location = new Point( location + 37, AUIntensityLbl.Location.Y );
			AUIntensityLevelLbl.Location = new Point( location + 127, AUIntensityLevelLbl.Location.Y );
		}

		// Sets the intensity labels of the track bars based on the selected items of the combo boxes
		private void setIntensityLabel( ComboBox comboBox, TrackBar trackbar, Label label )
		{
			switch( comboBox.SelectedItem.ToString( ) )
			{
				case ( "A" ):
					trackbar.Value = ( int )( ( 1 / Constants.AIntensity ) * 100 );
					label.Text = ( int )( ( 1 / Constants.AIntensity ) * 100 ) + "%";
					break;
				case ( "B" ):
					trackbar.Value = ( int )( ( 1 / Constants.BIntensity ) * 100 );
					label.Text = ( int )( ( 1 / Constants.BIntensity ) * 100 ) + "%";
					break;
				case ( "C" ):
					trackbar.Value = ( int )( ( 1 / Constants.CIntensity ) * 100 );
					label.Text = ( int )( ( 1 / Constants.CIntensity ) * 100 ) + "%";
					break;
				case ( "D" ):
					trackbar.Value = ( int )( ( 1 / Constants.DIntensity ) * 100 );
					label.Text = ( int )( ( 1 / Constants.DIntensity ) * 100 ) + "%";
					break;
				case ( "E" ):
					trackbar.Value = 100;
					label.Text = "100%";
					break;
				default:
					trackbar.Value = 0;
					label.Text = "0%";
					break;
			}
		}

		// Removes an AU from the video list
		private void removeVideoAU( ComboBox cb, ComboBox sideCB, ComboBox startCB, ComboBox endCB, TextBox startTB, TextBox endTB, Button rb, Label lbl1, Label lbl2 )
		{
			cb.Visible = false; sideCB.Visible = false; startCB.Visible = false; endCB.Visible = false;
			startTB.Visible = false; endTB.Visible = false; rb.Visible = false; lbl1.Visible = false; lbl2.Visible = false;

			bool allAUsRemoved = true;
			for( int i = 1; i <= 50; i++ )
			{
				Control [ ] controls = this.Controls.Find( "videoAUcombo" + i, true );
				if( controls.Length == 1 ) // 0 means not found, more - there are several controls with the same name
				{
					ComboBox control = controls [ 0 ] as ComboBox;
					if( control != null && control.Visible )
						allAUsRemoved = false;
				}
			}

			if( allAUsRemoved )
			{
				GenerateVideo.Visible = false;
				stop.Visible = false;
			}
		}

		// Records the video in a given path
		private void RecordVideo( string path )
		{
			Point p = this.Bounds.Location;
			p.X = p.X + 18;
			p.Y = p.Y + 80;

			Rectangle haptekRectangle = this.axActiveHaptekX1.Bounds;

			haptekRectangle.X = p.X;
			haptekRectangle.Y = p.Y;
			haptekRectangle.Width = 480;

			EncoderDevice videoDevice = null;
			EncoderDevice audioDevice = null;

			if( EncoderDevices.FindDevices( EncoderDeviceType.Video ).Count > 0 )
				videoDevice = EncoderDevices.FindDevices( EncoderDeviceType.Video ) [ 0 ];

			// Select the audio device
			if( EncoderDevices.FindDevices( EncoderDeviceType.Audio ).Count > 0 )
				audioDevice = EncoderDevices.FindDevices( EncoderDeviceType.Audio ) [ 0 ];

			if( audioDevice != null )
				screenCapture.AddAudioDeviceSource( audioDevice );

			// Setup the audio profile
			screenCapture.ScreenCaptureAudioProfile.BitsPerSample = 16;
			screenCapture.ScreenCaptureAudioProfile.Channels = 1;
			screenCapture.ScreenCaptureAudioProfile.SamplesPerSecond = 32000;
			screenCapture.ScreenCaptureAudioProfile.Bitrate = new ConstantBitrate( 20 );

			// Setup the video profile
			screenCapture.CaptureRectangle = haptekRectangle;
			screenCapture.ShowFlashingBoundary = true;
			screenCapture.ScreenCaptureVideoProfile.FrameRate = 30; // You can change frame rate, if you think files are too big.
			screenCapture.ScreenCaptureVideoProfile.Quality = 100; // You can change the quality (1-100), if you think files are too big.
			screenCapture.CaptureMouseCursor = true;

			screenCapture.OutputScreenCaptureFileName = path;
			screenCapture.CaptureMouseCursor = false;
			screenCapture.Start( );
		}

		// Stops recording a video immediately
		private void StopRecordingImmediately( )
		{
			screenCapture.Stop( );
		}

		// Stops recording a video when the character stops speaking
		private void StopRecording( )
		{
			// Check every 1000ms to see if the character has stoped speaking. If it is finished with its speech, stop the recording.
			// If we do not use a timer, and just check the speaking status in a loop, the thread will be so busy with "checking", and cannot animate the character properly.
			myTimer = new System.Timers.Timer( );
			myTimer.Elapsed += new ElapsedEventHandler( stopIfNotSpeaking );
			myTimer.Interval = 1000;
			myTimer.Enabled = true;
		}

		// Stops the video recording if character is not speaking
		private void stopIfNotSpeaking( object source, ElapsedEventArgs e )
		{
			axActiveHaptekX1.Query = "status tts busy";
			string isCharacterSpeaking = axActiveHaptekX1.QueryReturn; // 0:no, 1:yes

			if( isCharacterSpeaking == "0" )
			{
				myTimer.Dispose( );
				screenCapture.Stop( );
			}
		}

		// Starts recording the video
		private void StartRecording( )
		{
			string txt;

			SaveFileDialog browseFile = new SaveFileDialog( );
			browseFile.Filter = "Wmv Video (.wmv)|*.wmv|AVI Video (.avi)|*.avi|Mpeg video (.mpeg)|*.mpeg|Mpg Vodeo (.mpg)|*.mpg|XESC Files (*.xesc)|*.xesc";
			browseFile.Title = "Browse video files";
			if( browseFile.ShowDialog( ) == DialogResult.Cancel )
				return;
			try
			{
				txt = browseFile.FileName;
			}
			catch( Exception )
			{
				MessageBox.Show( "Error opening file", "File Error",
				MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			}

			RecordVideo( browseFile.FileName );
		}

		// Activates an AU which is involved in the video generation
		private void activateVideoAU( ref HapFACSVideo hfv, string au, string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			MethodInfo methodInfo = typeof( HapFACSVideo ).GetMethod( au + "Video" );
			String [ ] unilateralAUs = { "AU2", "AU13", "AU14", "AU15", "AU38", "AU39", "AU61", "AU62", "AU63", "AU64", "AU65", "AU66" };
			String [ ] instantAUs = { "AU45", "AUM59", "AUM60" };

			if( unilateralAUs.Contains( au ) )
			{
				object [ ] arguments = { side, startIntensity, endIntensity, startTime, endTime, currentTime };
				methodInfo.Invoke( hfv, arguments );
			}
			else if( instantAUs.Contains( au ) )
			{
				object [ ] arguments = { startTime };
				methodInfo.Invoke( hfv, arguments );
			}
			else if( au == "AU46" )
			{
				object [ ] arguments = { side, startTime };
				methodInfo.Invoke( hfv, arguments );
			}
			else
			{
				object [ ] arguments = { startIntensity, endIntensity, startTime, endTime, currentTime };
				methodInfo.Invoke( hfv, arguments );
			}
		}

		// Add speaking to the video
		private void activateVideoAU( ref HapFACSVideo hfv, string au, double startTime, string textToSpeak )
		{
			MethodInfo methodInfo = typeof( HapFACSVideo ).GetMethod( au + "Video" );
			if( au == "Speak" )
			{
				object [ ] arguments = { startTime, textToSpeak }; // here, the end time plays the rle of the text to be spoken
				methodInfo.Invoke( hfv, arguments );
			}
		}

		// Makes an AU set of controls visible
		private void makeVisible( ComboBox cb, ComboBox sideCB, ComboBox startCB, ComboBox endCB, TextBox startTB, TextBox endTB, Button rb, Label lbl1, Label lbl2 )
		{
			String [ ] unilateralAUs = { "AU2", "AU13", "AU14", "AU15", "AU38", "AU39", "AU61", "AU62", "AU63", "AU64", "AU65", "AU66" };
			String [ ] instantAUs = { "AU45", "AUM59", "AUM60" };
			if( unilateralAUs.Contains( cb.SelectedItem.ToString( ) ) )
			{
				sideCB.Visible = true;
				startCB.Visible = true;
				endCB.Visible = true;
				startTB.Visible = true;
				endTB.Visible = true;
				rb.Visible = true;
				lbl1.Visible = true;
				lbl2.Visible = true;
				// Default Positions 
				startTB.Left = 363;
				startTB.Width = 53;
				lbl1.Left = 416;
				lbl1.Width = 29;
				endTB.Left = 448;
				endTB.Width = 53;
			}
			else if( instantAUs.Contains( cb.SelectedItem.ToString( ) ) )
			{
				sideCB.Visible = false;
				startCB.Visible = false;
				endCB.Visible = false;
				startTB.Text = "Time";
				startTB.Visible = true;
				endTB.Visible = false;
				lbl1.Visible = true;
				lbl2.Visible = false;
				rb.Visible = true;
				// Default Positions 
				startTB.Left = 363;
				startTB.Width = 53;
				lbl1.Left = 416;
				lbl1.Width = 29;
				endTB.Left = 448;
				endTB.Width = 53;
			}
			else if( cb.SelectedItem.ToString( ) == "AU46" )
			{
				sideCB.Visible = true;
				sideCB.Items.Remove( sideCB.Items [ 2 ] ); // Remove the bilateral item
				sideCB.Items [ 0 ] = "Left"; // Add the left item
				sideCB.Items [ 1 ] = "Right"; // Add the right item
				startCB.Visible = false;
				endCB.Visible = false;
				startTB.Text = "Time";
				startTB.Visible = true;
				endTB.Visible = false;
				lbl1.Visible = false;
				lbl2.Visible = false;
				rb.Visible = true;
				// Default Positions 
				startTB.Left = 363;
				startTB.Width = 53;
				lbl1.Left = 416;
				lbl1.Width = 29;
				endTB.Left = 448;
				endTB.Width = 53;
			}
			else if( cb.SelectedItem.ToString( ) == "Speak" )
			{
				sideCB.Visible = false;
				startCB.Visible = false;
				endCB.Visible = false;
				startTB.Visible = true;
				endTB.Text = "Text to be spoken";
				endTB.Visible = true;
				lbl1.Visible = true;
				lbl2.Visible = false;
				rb.Visible = true;
				// Changed Positions for speak
				// Here, the "video end-time text box" palys the role of the "text to be spoken".
				startTB.Left = 92;
				startTB.Width = 53;
				lbl1.Left = 145;
				lbl1.Width = 29;
				endTB.Left = 177;
				endTB.Width = 324;
			}
			else if( cb.SelectedItem.ToString( ) == "Reset All" )
			{
				sideCB.Visible = false;
				startCB.Visible = false;
				endCB.Visible = false;
				startTB.Visible = false;
				endTB.Visible = false;
				lbl1.Visible = false;
				lbl2.Visible = false;
				rb.Visible = true;
			}
			else
			{
				sideCB.Visible = false;
				startCB.Visible = true;
				endCB.Visible = true;
				startTB.Visible = true;
				endTB.Visible = true;
				rb.Visible = true;
				lbl1.Visible = true;
				lbl2.Visible = true;
				// Default Positions 
				startTB.Left = 363;
				startTB.Width = 53;
				lbl1.Left = 416;
				lbl1.Width = 29;
				endTB.Left = 448;
				endTB.Width = 53;
			}

			int y = cb.Location.Y;
			sideCB.Location = new Point( sideCB.Location.X, y );
			startCB.Location = new Point( startCB.Location.X, y );
			endCB.Location = new Point( endCB.Location.X, y );
			startTB.Location = new Point( startTB.Location.X, y );
			endTB.Location = new Point( endTB.Location.X, y );
			rb.Location = new Point( rb.Location.X, y );
			lbl1.Location = new Point( lbl1.Location.X, y + 5 );
			lbl2.Location = new Point( lbl2.Location.X, y + 5 );
		}

		// The character reads out the text passed to the method
		private void SpeakTheText( String txtToSpeak )
		{
			axActiveHaptekX1.HyperText = @"\Q2[s0= [" + txtToSpeak + "]]";
		}

		// Resets upper face AUs
		private void ResetUpperAUs( )
		{
			String reset = hapFACS.resetUpperAUs( );
			axActiveHaptekX1.HyperText = reset;

			AU1.Checked = false;
			AU2.Checked = false;
			AU4.Checked = false;
			AU5.Checked = false;
			AU6.Checked = false;
			AU7.Checked = false;
			AU8.Checked = false;
			AU41.Checked = false;
			AU42.Checked = false;
			AU43.Checked = false;
			AU44.Checked = false;
			AU46.Checked = false;
		}

		// Resets the lower face AUs
		private void ResetLowerAUs( )
		{
			String reset = hapFACS.resetLowerAUs( );
			axActiveHaptekX1.HyperText = reset;

			AU9.Checked = false;
			AU10.Checked = false;
			AU11.Checked = false;
			AU12.Checked = false;
			AU13.Checked = false;
			AU14.Checked = false;
			AU15.Checked = false;
			AU16.Checked = false;
			AU17.Checked = false;
			AU18.Checked = false;
			AU20.Checked = false;
			AU22.Checked = false;
			AU23.Checked = false;
			AU24.Checked = false;
			AU25.Checked = false;
			AU26.Checked = false;
			AU27.Checked = false;
			AU28.Checked = false;
			AU38.Checked = false;
			AU39.Checked = false;
		}

		// Resets the head and eye AUs
		private void ResetHeadAndEyeAUs( )
		{
			String reset = hapFACS.resetHeadAndEyeAUs( );
			axActiveHaptekX1.HyperText = reset;

			AU51.Checked = false;
			AU52.Checked = false;
			AU53.Checked = false;
			AU54.Checked = false;
			AU55.Checked = false;
			AU56.Checked = false;
			AU57.Checked = false;
			AU58.Checked = false;
			AU61.Checked = false;
			AU62.Checked = false;
			AU63.Checked = false;
			AU64.Checked = false;
			AU65.Checked = false;
			AU66.Checked = false;
		}

		// Loads the appropriate hypertext to the character to apply the selected background color
		private void updateBackgroundColor( )
		{
			double red;
			double green;
			double blue;
			bool isRedNum = Double.TryParse( RedTB.Text, out red );
			bool isGreenNum = Double.TryParse( GreenTB.Text, out green );
			bool isBlueNum = Double.TryParse( BlueTB.Text, out blue );

			if( isRedNum && isGreenNum && isBlueNum )
				axActiveHaptekX1.HyperText = @"\LoadBackGrnd[ r= " + red + " g= " + green + " b= " + blue + "]";
		}

		// Loads the appropriate hypertext to the character to apply the selected light
		private void setLight( )
		{
			axActiveHaptekX1.HyperText = @"\SetLight[ i0= 2 i1= 4 f0= 0]"; // Set the light off

			// Set light color
			int redIntensity = RedIntensityTrB.Value / 20;
			int greenIntensity = GreenIntensityTrB.Value / 20;
			int blueIntensity = BlueIntensityTrB.Value / 20;

			axActiveHaptekX1.HyperText = @"\SetLight[ i0= 2 i1= 0 r= " + Convert.ToDouble( RedLightTB.Text ) * redIntensity +
				" g= " + Convert.ToDouble( GreenLightTB.Text ) * greenIntensity + " b= " + Convert.ToDouble( BlueLightTB.Text ) * blueIntensity + "]";

			short lightType = 5; // Set default light type as "Point"
			if( AmbientLightRB.Checked )
			{
				lightType = 1;
				axActiveHaptekX1.HyperText = @"\SetLight[ i0= 2 i1= 3 i2= " + lightType + "]"; // Set light type
			}
			else if( DirectionalLightRB.Checked )
			{
				lightType = 3;
				axActiveHaptekX1.HyperText = @"\SetLight[ i0= 2 i1= 3 i2= " + lightType + "]"; // Set light type
				// Set light direction
				axActiveHaptekX1.HyperText = @"\SetLight[ i0= 2 i1= 2 f0= " + Convert.ToDouble( XTB.Text ) +
				" f1= " + Convert.ToDouble( YTB.Text ) + " f2= " + Convert.ToDouble( ZTB.Text ) + " i2= " + lightType + "]";
			}
			else
			{
				axActiveHaptekX1.HyperText = @"\SetLight[ i0= 2 i1= 3 i2= " + lightType + "]"; // Set light type
				// Set light position
				axActiveHaptekX1.HyperText = @"\SetLight[ i0= 2 i1= 1 f0= " + Convert.ToDouble( XTB.Text ) +
				" f1= " + Convert.ToDouble( YTB.Text ) + " f2= " + Convert.ToDouble( ZTB.Text ) + " i2= " + lightType + "]";
			}
		}

		// Updates the light type of the character
		private void updateLightType( )
		{
			if( PointLightRB.Checked )
			{
				LightPosition.Text = "Light Point Position";
				InstructionLbl.Text = "Values change between -180 to 180";
				LightPosition.Visible = true;
			}
			else if( DirectionalLightRB.Checked )
			{
				LightPosition.Text = "Light Direction Along Each Axis";
				InstructionLbl.Text = "Values can be 0, 1, or -1";
				LightPosition.Visible = true;
			}
			else
			{
				LightPosition.Visible = false;
			}
		}

		// Updates the light color as typed in the text boxes
		private void updateLightColor( )
		{
			double red;
			double green;
			double blue;
			double x;
			double y;
			double z;
			double redIntensity;
			double greenIntensity;
			double blueIntensity;
			bool isRedNum = Double.TryParse( RedLightTB.Text, out red );
			bool isGreenNum = Double.TryParse( GreenLightTB.Text, out green );
			bool isBlueNum = Double.TryParse( BlueLightTB.Text, out blue );
			bool isXNum = Double.TryParse( XTB.Text, out x );
			bool isYNum = Double.TryParse( YTB.Text, out y );
			bool isZNum = Double.TryParse( ZTB.Text, out z );
			bool isRedIntNum = Double.TryParse( RedIntensityTB.Text, out redIntensity );
			bool isGreenIntNum = Double.TryParse( GreenIntensityTB.Text, out greenIntensity );
			bool isBlueIntNum = Double.TryParse( BlueIntensityTB.Text, out blueIntensity );

			if( isRedNum && isGreenNum && isBlueNum && isXNum && isYNum && isZNum && isRedIntNum && isGreenIntNum && isBlueIntNum )
			{
				RedIntensityTrB.Value = ( int )redIntensity;
				GreenIntensityTrB.Value = ( int )greenIntensity;
				BlueIntensityTrB.Value = ( int )blueIntensity;
				setLight( );
			}
		}

		#endregion

		#region Methods that run when different buttons are clicked

		// Runs when character is changed
		private void Character_SelectedIndexChanged(object sender, EventArgs e)
        {
            axActiveHaptekX1.Query = "current figurename";
            string currentFigure = axActiveHaptekX1.QueryReturn;
            axActiveHaptekX1.HyperText = @"\DelPerson[figure= " + currentFigure + " i0= 1]";
			initializeCharacter( );
			SetCharacterAndHair();
        }

		// Runs when save photo button is clicked
		private void SavePhoto_Click( object sender, EventArgs e )
		{
			string txt;

			SaveFileDialog browseFile = new SaveFileDialog( );
			browseFile.Filter = "Png Image (.png)|*.png|Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
			browseFile.Title = "Browse Image files";
			if( browseFile.ShowDialog( ) == DialogResult.Cancel )
				return;
			try
			{
				txt = browseFile.FileName;
			}
			catch( Exception )
			{
				MessageBox.Show( "Error opening file", "File Error",
				MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			}

			Thread.Sleep( 500 ); // Wait until the file dialog box is removed from the page, so we can make sure it does not mask the character's face.

			Bitmap bmpScreenshot;
			Graphics gfxScreenshot;

			bmpScreenshot = new Bitmap( this.axActiveHaptekX1.Bounds.Width, this.axActiveHaptekX1.Bounds.Height, PixelFormat.Format32bppArgb );
			gfxScreenshot = Graphics.FromImage( bmpScreenshot );

			Point p = this.Bounds.Location;
			p.X = p.X + 18;
			p.Y = p.Y + 80;
			Point whereToDraw = new Point( 0, 0 );

			gfxScreenshot.CopyFromScreen( p, whereToDraw, this.axActiveHaptekX1.Bounds.Size );
			browseFile.AddExtension = true;
			bmpScreenshot.Save( browseFile.FileName, ImageFormat.Png );
		}

		// Runs when character hair is changed
		private void Hair_SelectedIndexChanged( object sender, EventArgs e )
		{
			axActiveHaptekX1.Query = "current figurename";
			string currentFigure = axActiveHaptekX1.QueryReturn;
			axActiveHaptekX1.HyperText = @"\DelPerson[figure= " + currentFigure + " i0= 1]";
			initializeCharacter( );
			SetCharacterAndHair( );
		}

		// Runs when Add AU button is clicked
		private void addAUButton_Click( object sender, EventArgs e )
		{
			for( int i = 1; i <= 100; i++ )
			{
				Control [ ] controls = this.Controls.Find( "videoAUcombo" + i, true );
				if( controls.Length == 1 ) // 0 means not found, more - there are several controls with the same name
				{
					ComboBox control = controls [ 0 ] as ComboBox;
					if( control != null && !control.Visible )
					{
						panel1.VerticalScroll.Value = 20; // Deals with the auto scroll problems of the panel
						control.Visible = true;
						break;
					}
					else if( control != null && control.Visible && i == 100 )
						MessageBox.Show( "The current version of HapFACS accepts at most 100 AUs for video generation!\nIf you need more than 100 AUs, use the file import button.", "Warning..." );
				}
			}

			GenerateVideo.Visible = true;
		}

		// Runs when the stop video recording is clicked
		private void stop_Click( object sender, EventArgs e )
		{
			StopRecordingImmediately( );
			stop.Visible = false;
		}

		// Runs when the generate video button is clicked
		private void GenerateVideo_Click( object sender, EventArgs e )
		{
			stop.Visible = true;

			string txt;

			SaveFileDialog browseFile = new SaveFileDialog( );
			browseFile.Filter = "HAP Files (*.hap)|*.hap";
			browseFile.Title = "Browse HAP files";
			if( browseFile.ShowDialog( ) == DialogResult.Cancel )
				return;
			try
			{
				txt = browseFile.FileName;
			}
			catch( Exception )
			{
				MessageBox.Show( "Error opening file", "File Error",
				MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			}

			HapFACSVideo hfv = new HapFACSVideo( browseFile.FileName );
			ComboBox AUCombo = null, sideCombo = null, startIntenityCombo = null, endIntenityCombo = null;
			TextBox startTimeTxt = null; TextBox endTimeTxt = null;
			double animationDuration = 0;
			double videoStartTime = 0;
			Animation animation = new Animation( );

			for( int i = 1; i <= 100; i++ ) // 100 is the maximum number of movements that fit in video generation tab. You can increase them by making them smaller in the form.
			{
				Control [ ] controls = this.Controls.Find( "videoAUcombo" + i, true );
				if( controls.Length == 1 ) // 0 means not found, more - there are several controls with the same name
				{
					AUCombo = controls [ 0 ] as ComboBox;
				}

				controls = this.Controls.Find( "videoSidecombo" + i, true );
				if( controls.Length == 1 ) // 0 means not found, more - there are several controls with the same name
				{
					sideCombo = controls [ 0 ] as ComboBox;
				}

				controls = this.Controls.Find( "videoStartcombo" + i, true );
				if( controls.Length == 1 ) // 0 means not found, more - there are several controls with the same name
				{
					startIntenityCombo = controls [ 0 ] as ComboBox;
				}

				controls = this.Controls.Find( "videoEndcombo" + i, true );
				if( controls.Length == 1 ) // 0 means not found, more - there are several controls with the same name
				{
					endIntenityCombo = controls [ 0 ] as ComboBox;
				}

				controls = this.Controls.Find( "videoStartTxt" + i, true );
				if( controls.Length == 1 ) // 0 means not found, more - there are several controls with the same name
				{
					startTimeTxt = controls [ 0 ] as TextBox;
				}

				controls = this.Controls.Find( "videoEndTxt" + i, true );
				if( controls.Length == 1 ) // 0 means not found, more - there are several controls with the same name
				{
					endTimeTxt = controls [ 0 ] as TextBox;
				}

				if( AUCombo.Visible && startTimeTxt.Visible )
				{
					animation.addToAnimation(
						new AnimationSector( AUCombo.Text.ToString( ),
							sideCombo.Text.ToString( ),
							startIntenityCombo.Text.ToString( ),
							endIntenityCombo.Text.ToString( ),
							startTimeTxt.Text.ToString( ),
							endTimeTxt.Text.ToString( ) ) );
				}
			}

			animationDuration = animation.animationLengthIncludingStops( );
			hfv.setAnimationDuration( animationDuration );
			videoStartTime = animation.animationStartTime( );
			int numberOfSectors = animation.getAnimation( ).Count;

			for( double i = videoStartTime; i < videoStartTime + animationDuration; i = i + ( animationDuration / 100 ) )
			{
				for( int j = 0; j < numberOfSectors; j++ )
				{
					AnimationSector sector = ( AnimationSector )( animation.getAnimation( ) [ j ] );
					if( i < sector.getEndTime() && i >= sector.getStartTime() )
						activateVideoAU( ref hfv, sector.getAU( ), sector.getSide(), sector.getStartIntensity(), sector.getEndIntensity(), sector.getStartTime(), sector.getEndTime(), i );
				}
			}

			// Add the speaking times and hypertexts to the end of the file
			for( int s = 0; s < numberOfSectors; s++ )
			{
				AnimationSector sector = ( AnimationSector )( animation.getAnimation( ) [ s ] );
				if( sector.getAU() == "Speak" )
				{
					activateVideoAU( ref hfv, sector.getAU( ), sector.getStartTime(), sector.getTextToSpeak() );
				}
				else if( sector.getAU() == "Reset All" )
				{
					hfv.resetAllAUs( );
				}
			}

			String [ ] output = ( String [ ] )( hfv.generateVideo( ).ToArray( typeof( String ) ) );

			System.IO.File.WriteAllLines( browseFile.FileName, output );
			StartRecording( );

			axActiveHaptekX1.HyperText = @"\load [file=[" + browseFile.FileName + "]]";

			// For generating experiment videos
			// It stops the video recording 100 ms after all the last AU is animated.
			System.Threading.Thread.Sleep( ( int )( animationDuration * 1000 ) + 100 );
			StopRecording( );
			stop.Visible = false;

		}

		// The following methods run when different reset buttons are selected

		private void ResetAUs_Click( object sender, EventArgs e )
		{
			ResetUpperAUs( ); ResetUpperAUs( );
			ResetLowerAUs( ); ResetLowerAUs( );
			ResetHeadAndEyeAUs( ); ResetHeadAndEyeAUs( );
			Neutral.Checked = true;
		}

		private void ResetUpperAUs_Click( object sender, EventArgs e )
		{
			ResetUpperAUs( );
			ResetUpperAUs( );
		}

		private void ResetLowAUs_Click( object sender, EventArgs e )
		{
			ResetLowerAUs( );
			ResetLowerAUs( );
		}

		private void ResetHeadEyeAUs_Click( object sender, EventArgs e )
		{
			ResetHeadAndEyeAUs( );
			ResetHeadAndEyeAUs( );
		}

		// The following methods are activated when AUs are selected, when their intensity is changed, and when their side is changed

		private void AU1_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU1Intensity, AU1IntensityTB, AU1IntensityLevelLbl, AU1IntensityLbl );
			if( !AU1.Checked )
			{
				AU1IntensityLevelLbl.Text = "0%";
				AU1IntensityTB.Value = 0;
				AU1Intensity.Text = "Intensity";
				hapFACS.AU1( "0", lastAU1_Intensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU1_Intensity = "0";
			}
        }

		private void AU1Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU1Intensity, AU1IntensityTB, AU1IntensityLevelLbl );
			hapFACS.AU1( AU1Intensity.SelectedItem.ToString( ), lastAU1_Intensity );
			lastAU1_Intensity = AU1Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU2_CheckedChanged(object sender, EventArgs e)
        {
			if (AU2.Checked)
            {
                AU2LeftIntensity.Text = "Intensity";
                AU2Side.Enabled = true; AU2Side.Text = "Side";
            }
            else
            {
				disableIntensityControls(AU2LeftIntensity, AU2LeftIntensityTB, AU2LeftIntensityLevelLbl, AU2LeftIntensityLbl);
				disableIntensityControls( AU2RightIntensity, AU2RightIntensityTB, AU2RightIntensityLevelLbl, AU2RightIntensityLbl );
				locateControls( AU2LeftIntensityTB, AU2LeftIntensityLevelLbl, AU2LeftIntensityLbl, 368 );
				visibleIntensityControls( AU2RightIntensity, AU2RightIntensityTB, AU2RightIntensityLevelLbl, AU2RightIntensityLbl );
				AU2LeftIntensityLevelLbl.Text = "0%";
				AU2LeftIntensityTB.Value = 0;
                AU2LeftIntensity.Text = "Intensity";
				AU2LeftIntensityLbl.Text = "Left intensity";
                AU2Side.Enabled = false; AU2Side.Text = "Side";
                AU2RightIntensity.Visible = false; AU2RightIntensity.Text = "Right Intensity";
				hapFACS.AU2("0", "0", lastAU2_LeftIntensity, lastAU2_RightIntensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU2_LeftIntensity = "0";
                lastAU2_RightIntensity = "0";
            }
        }

		private void AU2LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU2Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU2RightIntensity.SelectedIndex < 0 )
				{
					AU2RightIntensity.SelectedItem = "0"; AU2RightIntensity.Text = "Right Intensity";
					AU2RightIntensityTB.Value = 0; AU2RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU2RightIntensity, AU2RightIntensityTB, AU2RightIntensityLevelLbl, AU2RightIntensityLbl );
				AU2RightIntensity.Visible = true;
				setIntensityLabel( AU2LeftIntensity, AU2LeftIntensityTB, AU2LeftIntensityLevelLbl );
				hapFACS.AU2( AU2LeftIntensityTB.Value.ToString( ), AU2RightIntensityTB.Value.ToString( ), lastAU2_LeftIntensity, lastAU2_RightIntensity );
				lastAU2_LeftIntensity = AU2LeftIntensityTB.Value.ToString( );
				lastAU2_RightIntensity = AU2RightIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU2RightIntensity, AU2RightIntensityTB, AU2RightIntensityLevelLbl, AU2RightIntensityLbl );
				AU2RightIntensity.Visible = false;
				setIntensityLabel( AU2LeftIntensity, AU2LeftIntensityTB, AU2LeftIntensityLevelLbl );
				hapFACS.AU2( AU2LeftIntensityTB.Value.ToString( ), AU2LeftIntensityTB.Value.ToString( ), lastAU2_LeftIntensity, lastAU2_RightIntensity );
				lastAU2_LeftIntensity = AU2LeftIntensityTB.Value.ToString( );
				lastAU2_RightIntensity = AU2LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU2RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU2LeftIntensity.SelectedIndex < 0 )
			{
				AU2LeftIntensity.SelectedItem = "0"; AU2LeftIntensity.Text = "Left Intensity";
				AU2LeftIntensityTB.Value = 0; AU2LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU2RightIntensity, AU2RightIntensityTB, AU2RightIntensityLevelLbl );
			hapFACS.AU2( AU2LeftIntensityTB.Value.ToString( ), AU2RightIntensityTB.Value.ToString( ), lastAU2_LeftIntensity, lastAU2_RightIntensity );
			lastAU2_LeftIntensity = AU2LeftIntensityTB.Value.ToString( );
			lastAU2_RightIntensity = AU2RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU2Side_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU2LeftIntensity, AU2LeftIntensityTB, AU2LeftIntensityLevelLbl, AU2LeftIntensityLbl );
			AU2RightIntensityTB.Value = 0; AU2RightIntensityLevelLbl.Text = "0%";
			if( AU2Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU2LeftIntensityTB, AU2LeftIntensityLevelLbl, AU2LeftIntensityLbl, 368 );
				enableIntensityControls( AU2RightIntensity, AU2RightIntensityTB, AU2RightIntensityLevelLbl, AU2RightIntensityLbl );
				visibleIntensityControls( AU2RightIntensity, AU2RightIntensityTB, AU2RightIntensityLevelLbl, AU2RightIntensityLbl );
				AU2LeftIntensity.Text = "Left Intensity";
				AU2LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU2LeftIntensityTB, AU2LeftIntensityLevelLbl, AU2LeftIntensityLbl, 264 );
				disableIntensityControls( AU2RightIntensity, AU2RightIntensityTB, AU2RightIntensityLevelLbl, AU2RightIntensityLbl );
				invisibleIntensityControls( AU2RightIntensity, AU2RightIntensityTB, AU2RightIntensityLevelLbl, AU2RightIntensityLbl );
				AU2LeftIntensity.Text = "Intensity";
				AU2LeftIntensityLbl.Text = "Intensity";
			}
		}

        private void AU4_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU4Intensity, AU4IntensityTB, AU4IntensityLevelLbl, AU4IntensityLbl );
			if( !AU4.Checked )
			{
				AU4IntensityLevelLbl.Text = "0%";
				AU4IntensityTB.Value = 0;
				AU4Intensity.Text = "Intensity";
				hapFACS.AU4( "0", lastAU4_Intensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU4_Intensity = "0";
			}
        }

		private void AU4Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU4Intensity, AU4IntensityTB, AU4IntensityLevelLbl );
			hapFACS.AU4( AU4Intensity.SelectedItem.ToString( ), lastAU4_Intensity );
			lastAU4_Intensity = AU4Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU5_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU5Intensity, AU5IntensityTB, AU5IntensityLevelLbl, AU5IntensityLbl );
			if( !AU5.Checked )
			{
				AU5IntensityLevelLbl.Text = "0%";
				AU5IntensityTB.Value = 0;
				AU5Intensity.Text = "Intensity";
                hapFACS.AU5("0", lastAU5_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU5_Intensity = "0";
            }
        }

		private void AU5Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU5Intensity, AU5IntensityTB, AU5IntensityLevelLbl );
			hapFACS.AU5( AU5Intensity.SelectedItem.ToString( ), lastAU5_Intensity );
			lastAU5_Intensity = AU5Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU6_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU6Intensity, AU6IntensityTB, AU6IntensityLevelLbl, AU6IntensityLbl );
			if( !AU6.Checked )
			{
				AU6IntensityLevelLbl.Text = "0%";
				AU6IntensityTB.Value = 0;
				AU6Intensity.Text = "Intensity";
                hapFACS.AU6("0", lastAU6_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU6_Intensity = "0";
            }
        }

		private void AU6Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU6Intensity, AU6IntensityTB, AU6IntensityLevelLbl );
			hapFACS.AU6( AU6Intensity.SelectedItem.ToString( ), lastAU6_Intensity );
			lastAU6_Intensity = AU6Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU7_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU7Intensity, AU7IntensityTB, AU7IntensityLevelLbl, AU7IntensityLbl );
			if( !AU7.Checked )
			{
				AU7IntensityLevelLbl.Text = "0%";
				AU7IntensityTB.Value = 0;
				AU7Intensity.Text = "Intensity";
                hapFACS.AU7("0", lastAU7_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU7_Intensity = "0";
            }
        }

		private void AU7Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU7Intensity, AU7IntensityTB, AU7IntensityLevelLbl );
			hapFACS.AU7( AU7Intensity.SelectedItem.ToString( ), lastAU7_Intensity );
			lastAU7_Intensity = AU7Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU8_CheckedChanged( object sender, EventArgs e )
		{
			switchIntensityControlsVisibility( AU8Intensity, AU8IntensityTB, AU8IntensityLevelLbl, AU8IntensityLbl );
			if( !AU8.Checked )
			{
				AU8IntensityLevelLbl.Text = "0%";
				AU8IntensityTB.Value = 0;
				AU8Intensity.Text = "Intensity";
				hapFACS.AU8( "0", lastAU8_Intensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU8_Intensity = "0";
				AU8Comments.Visible = false;
			}
			
		}

		private void AU8Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU8Intensity, AU8IntensityTB, AU8IntensityLevelLbl );
			hapFACS.AU8( AU8Intensity.SelectedItem.ToString( ), lastAU8_Intensity );
			lastAU8_Intensity = AU8Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU9_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU9Intensity, AU9IntensityTB, AU9IntensityLevelLbl, AU9IntensityLbl );
			if( !AU9.Checked )
			{
				AU9IntensityLevelLbl.Text = "0%";
				AU9IntensityTB.Value = 0;
				AU9Intensity.Text = "Intensity"; 
				hapFACS.AU9("0", lastAU9_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU9_Intensity = "0";
            }
        }

		private void AU9LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU9Intensity, AU9IntensityTB, AU9IntensityLevelLbl );
			hapFACS.AU9( AU9Intensity.SelectedItem.ToString( ), lastAU9_Intensity );
			lastAU9_Intensity = AU9Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU10_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU10Intensity, AU10IntensityTB, AU10IntensityLevelLbl, AU10IntensityLbl );
			if( !AU10.Checked )
			{
				AU10IntensityLevelLbl.Text = "0%";
				AU10IntensityTB.Value = 0; 
				AU10Intensity.Text = "Intensity";
                hapFACS.AU10("0", lastAU10_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU10_Intensity = "0";
            }
        }

		private void AU10Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU10Intensity, AU10IntensityTB, AU10IntensityLevelLbl );
			hapFACS.AU10( AU10Intensity.SelectedItem.ToString( ), lastAU10_Intensity );
			lastAU10_Intensity = AU10Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU11_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU11Intensity, AU11IntensityTB, AU11IntensityLevelLbl, AU11IntensityLbl );
			if( !AU11.Checked )
			{
				AU11IntensityLevelLbl.Text = "0%";
				AU11IntensityTB.Value = 0; 
				AU11Intensity.Text = "Intensity";
                hapFACS.AU11("0", lastAU11_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU11_Intensity = "0";
            }
        }

		private void AU11Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU11Intensity, AU11IntensityTB, AU11IntensityLevelLbl );
			hapFACS.AU11( AU11Intensity.SelectedItem.ToString( ), lastAU11_Intensity );
			lastAU11_Intensity = AU11Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU12_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU12Intensity, AU12IntensityTB, AU12IntensityLevelLbl, AU12IntensityLbl );
			if( !AU8.Checked )
			{
				AU12IntensityLevelLbl.Text = "0%";
				AU12IntensityTB.Value = 0; 
				AU12Intensity.Text = "Intensity";
                hapFACS.AU12("0", lastAU12_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU12_Intensity = "0";
            }
        }

		private void AU12Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU12Intensity, AU12IntensityTB, AU12IntensityLevelLbl );
			hapFACS.AU12( AU12Intensity.SelectedItem.ToString( ), lastAU12_Intensity );
			lastAU12_Intensity = AU12Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU13_CheckedChanged(object sender, EventArgs e)
        {
            if( AU13.Checked )
			{
				AU13LeftIntensity.Text = "Intensity";
				AU13Side.Enabled = true; AU13Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU13LeftIntensity, AU13LeftIntensityTB, AU13LeftIntensityLevelLbl, AU13LeftIntensityLbl );
				disableIntensityControls( AU13RightIntensity, AU13RightIntensityTB, AU13RightIntensityLevelLbl, AU13RightIntensityLbl );
				locateControls( AU13LeftIntensityTB, AU13LeftIntensityLevelLbl, AU13LeftIntensityLbl, 368 );
				visibleIntensityControls( AU13RightIntensity, AU13RightIntensityTB, AU13RightIntensityLevelLbl, AU13RightIntensityLbl );
				AU13LeftIntensityLevelLbl.Text = "0%";
				AU13LeftIntensityTB.Value = 0;
				AU13LeftIntensity.Text = "Intensity";
				AU13LeftIntensityLbl.Text = "Left intensity";
				AU13Side.Enabled = false; AU13Side.Text = "Side";
				AU13RightIntensity.Visible = false; AU13RightIntensity.Text = "Right Intensity";
				hapFACS.AU13( "0", "0", lastAU13_LeftIntensity, lastAU13_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU13_LeftIntensity = "0";
				lastAU13_RightIntensity = "0";
			}
        }

		private void AU13LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU13Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU13RightIntensity.SelectedIndex < 0 )
				{
					AU13RightIntensity.SelectedItem = "0"; AU13RightIntensity.Text = "Right Intensity";
					AU13RightIntensityTB.Value = 0; AU13RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU13RightIntensity, AU13RightIntensityTB, AU13RightIntensityLevelLbl, AU13RightIntensityLbl );
				AU13RightIntensity.Visible = true;
				setIntensityLabel( AU13LeftIntensity, AU13LeftIntensityTB, AU13LeftIntensityLevelLbl );
				hapFACS.AU13( AU13LeftIntensity.SelectedItem.ToString( ), AU13RightIntensity.SelectedItem.ToString( ), lastAU13_LeftIntensity, lastAU13_RightIntensity );
				lastAU13_LeftIntensity = AU13LeftIntensity.SelectedItem.ToString( );
				lastAU13_RightIntensity = AU13RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU13RightIntensity, AU13RightIntensityTB, AU13RightIntensityLevelLbl, AU13RightIntensityLbl );
				AU13RightIntensity.Visible = false;
				setIntensityLabel( AU13LeftIntensity, AU13LeftIntensityTB, AU13LeftIntensityLevelLbl );
				hapFACS.AU13( AU13LeftIntensityTB.Value.ToString( ), AU13LeftIntensityTB.Value.ToString( ), lastAU13_LeftIntensity, lastAU13_RightIntensity );
				lastAU13_LeftIntensity = AU13LeftIntensityTB.Value.ToString( );
				lastAU13_RightIntensity = AU13LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU13RighttIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU13LeftIntensity.SelectedIndex < 0 )
			{
				AU13LeftIntensity.SelectedItem = "0"; AU13LeftIntensity.Text = "Left Intensity";
				AU13LeftIntensityTB.Value = 0; AU13LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU13RightIntensity, AU13RightIntensityTB, AU13RightIntensityLevelLbl );
			hapFACS.AU13( AU13LeftIntensityTB.Value.ToString( ), AU13RightIntensityTB.Value.ToString( ), lastAU13_LeftIntensity, lastAU13_RightIntensity );
			lastAU13_LeftIntensity = AU13LeftIntensityTB.Value.ToString( );
			lastAU13_RightIntensity = AU13RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU13Side_SelectedIndexChanged_1( object sender, EventArgs e )
		{
			enableIntensityControls( AU13LeftIntensity, AU13LeftIntensityTB, AU13LeftIntensityLevelLbl, AU13LeftIntensityLbl );
			AU13RightIntensityTB.Value = 0; AU13RightIntensityLevelLbl.Text = "0%";
			if( AU13Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU13LeftIntensityTB, AU13LeftIntensityLevelLbl, AU13LeftIntensityLbl, 368 );
				enableIntensityControls( AU13RightIntensity, AU13RightIntensityTB, AU13RightIntensityLevelLbl, AU13RightIntensityLbl );
				visibleIntensityControls( AU13RightIntensity, AU13RightIntensityTB, AU13RightIntensityLevelLbl, AU13RightIntensityLbl );
				AU13LeftIntensity.Text = "Left Intensity";
				AU13LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU13LeftIntensityTB, AU13LeftIntensityLevelLbl, AU13LeftIntensityLbl, 264 );
				disableIntensityControls( AU13RightIntensity, AU13RightIntensityTB, AU13RightIntensityLevelLbl, AU13RightIntensityLbl );
				invisibleIntensityControls( AU13RightIntensity, AU13RightIntensityTB, AU13RightIntensityLevelLbl, AU13RightIntensityLbl );
				AU13LeftIntensity.Text = "Intensity";
				AU13LeftIntensityLbl.Text = "Intensity";
			}
		}

        private void AU14_CheckedChanged(object sender, EventArgs e)
        {
			if( AU14.Checked )
			{
				AU14LeftIntensity.Text = "Intensity";
				AU14Side.Enabled = true; AU14Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU14LeftIntensity, AU14LeftIntensityTB, AU14LeftIntensityLevelLbl, AU14LeftIntensityLbl );
				disableIntensityControls( AU14RightIntensity, AU14RightIntensityTB, AU14RightIntensityLevelLbl, AU14RightIntensityLbl );
				locateControls( AU14LeftIntensityTB, AU14LeftIntensityLevelLbl, AU14LeftIntensityLbl, 368 );
				visibleIntensityControls( AU14RightIntensity, AU14RightIntensityTB, AU14RightIntensityLevelLbl, AU14RightIntensityLbl );
				AU14LeftIntensityLevelLbl.Text = "0%";
				AU14LeftIntensityTB.Value = 0;
				AU14LeftIntensity.Text = "Intensity";
				AU14LeftIntensityLbl.Text = "Left intensity";
				AU14Side.Enabled = false; AU14Side.Text = "Side";
				AU14RightIntensity.Visible = false; AU14RightIntensity.Text = "Right Intensity";
				hapFACS.AU14( "0", "0", lastAU14_LeftIntensity, lastAU14_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU14_LeftIntensity = "0";
				lastAU14_RightIntensity = "0";
			}
        }

		private void AU14LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU14Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU14RightIntensity.SelectedIndex < 0 )
				{
					AU14RightIntensity.SelectedItem = "0"; AU14RightIntensity.Text = "Right Intensity";
					AU14RightIntensityTB.Value = 0; AU14RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU14RightIntensity, AU14RightIntensityTB, AU14RightIntensityLevelLbl, AU14RightIntensityLbl );
				AU14RightIntensity.Visible = true;
				setIntensityLabel( AU14LeftIntensity, AU14LeftIntensityTB, AU14LeftIntensityLevelLbl );
				hapFACS.AU14( AU14LeftIntensity.SelectedItem.ToString( ), AU14RightIntensity.SelectedItem.ToString( ), lastAU14_LeftIntensity, lastAU14_RightIntensity );
				lastAU14_LeftIntensity = AU14LeftIntensity.SelectedItem.ToString( );
				lastAU14_RightIntensity = AU14RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU14RightIntensity, AU14RightIntensityTB, AU14RightIntensityLevelLbl, AU14RightIntensityLbl );
				AU14RightIntensity.Visible = false;
				setIntensityLabel( AU14LeftIntensity, AU14LeftIntensityTB, AU14LeftIntensityLevelLbl );
				hapFACS.AU14( AU14LeftIntensityTB.Value.ToString( ), AU14LeftIntensityTB.Value.ToString( ), lastAU14_LeftIntensity, lastAU14_RightIntensity );
				lastAU14_LeftIntensity = AU14LeftIntensityTB.Value.ToString( );
				lastAU14_RightIntensity = AU14LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU14RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU14LeftIntensity.SelectedIndex < 0 )
			{
				AU14LeftIntensity.SelectedItem = "0"; AU14LeftIntensity.Text = "Left Intensity";
				AU14LeftIntensityTB.Value = 0; AU14LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU14RightIntensity, AU14RightIntensityTB, AU14RightIntensityLevelLbl );
			hapFACS.AU14( AU14LeftIntensityTB.Value.ToString( ), AU14RightIntensityTB.Value.ToString( ), lastAU14_LeftIntensity, lastAU14_RightIntensity );
			lastAU14_LeftIntensity = AU14LeftIntensityTB.Value.ToString( );
			lastAU14_RightIntensity = AU14RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU14Side_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU14LeftIntensity, AU14LeftIntensityTB, AU14LeftIntensityLevelLbl, AU14LeftIntensityLbl );
			AU14RightIntensityTB.Value = 0; AU14RightIntensityLevelLbl.Text = "0%";
			if( AU14Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU14LeftIntensityTB, AU14LeftIntensityLevelLbl, AU14LeftIntensityLbl, 368 );
				enableIntensityControls( AU14RightIntensity, AU14RightIntensityTB, AU14RightIntensityLevelLbl, AU14RightIntensityLbl );
				visibleIntensityControls( AU14RightIntensity, AU14RightIntensityTB, AU14RightIntensityLevelLbl, AU14RightIntensityLbl );
				AU14LeftIntensity.Text = "Left Intensity";
				AU14LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU14LeftIntensityTB, AU14LeftIntensityLevelLbl, AU14LeftIntensityLbl, 264 );
				disableIntensityControls( AU14RightIntensity, AU14RightIntensityTB, AU14RightIntensityLevelLbl, AU14RightIntensityLbl );
				invisibleIntensityControls( AU14RightIntensity, AU14RightIntensityTB, AU14RightIntensityLevelLbl, AU14RightIntensityLbl );
				AU14LeftIntensity.Text = "Intensity";
				AU14LeftIntensityLbl.Text = "Intensity";
			}
		}

		private void AU15_CheckedChanged(object sender, EventArgs e)
        {
			if( AU15.Checked )
			{
				AU15LeftIntensity.Text = "Intensity";
				AU15Side.Enabled = true; AU15Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU15LeftIntensity, AU15LeftIntensityTB, AU15LeftIntensityLevelLbl, AU15LeftIntensityLbl );
				disableIntensityControls( AU15RightIntensity, AU15RightIntensityTB, AU15RightIntensityLevelLbl, AU15RightIntensityLbl );
				locateControls( AU15LeftIntensityTB, AU15LeftIntensityLevelLbl, AU15LeftIntensityLbl, 368 );
				visibleIntensityControls( AU15RightIntensity, AU15RightIntensityTB, AU15RightIntensityLevelLbl, AU15RightIntensityLbl );
				AU15LeftIntensityLevelLbl.Text = "0%";
				AU15LeftIntensityTB.Value = 0;
				AU15LeftIntensity.Text = "Intensity";
				AU15LeftIntensityLbl.Text = "Left intensity";
				AU15Side.Enabled = false; AU15Side.Text = "Side";
				AU15RightIntensity.Visible = false; AU15RightIntensity.Text = "Right Intensity";
				hapFACS.AU15( "0", "0", lastAU15_LeftIntensity, lastAU15_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU15_LeftIntensity = "0";
				lastAU15_RightIntensity = "0";
			}
        }

		private void AU15LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU15Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU15RightIntensity.SelectedIndex < 0 )
				{
					AU15RightIntensity.SelectedItem = "0"; AU15RightIntensity.Text = "Right Intensity";
					AU15RightIntensityTB.Value = 0; AU15RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU15RightIntensity, AU15RightIntensityTB, AU15RightIntensityLevelLbl, AU15RightIntensityLbl );
				AU15RightIntensity.Visible = true;
				setIntensityLabel( AU15LeftIntensity, AU15LeftIntensityTB, AU15LeftIntensityLevelLbl );
				hapFACS.AU15( AU15LeftIntensity.SelectedItem.ToString( ), AU15RightIntensity.SelectedItem.ToString( ), lastAU15_LeftIntensity, lastAU15_RightIntensity );
				lastAU15_LeftIntensity = AU15LeftIntensity.SelectedItem.ToString( );
				lastAU15_RightIntensity = AU15RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU15RightIntensity, AU15RightIntensityTB, AU15RightIntensityLevelLbl, AU15RightIntensityLbl );
				AU15RightIntensity.Visible = false;
				setIntensityLabel( AU15LeftIntensity, AU15LeftIntensityTB, AU15LeftIntensityLevelLbl );
				hapFACS.AU15( AU15LeftIntensityTB.Value.ToString( ), AU15LeftIntensityTB.Value.ToString( ), lastAU15_LeftIntensity, lastAU15_RightIntensity );
				lastAU15_LeftIntensity = AU15LeftIntensityTB.Value.ToString( );
				lastAU15_RightIntensity = AU15LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU15RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU15LeftIntensity.SelectedIndex < 0 )
			{
				AU15LeftIntensity.SelectedItem = "0"; AU15LeftIntensity.Text = "Left Intensity";
				AU15LeftIntensityTB.Value = 0; AU15LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU15RightIntensity, AU15RightIntensityTB, AU15RightIntensityLevelLbl );
			hapFACS.AU15( AU15LeftIntensityTB.Value.ToString( ), AU15RightIntensityTB.Value.ToString( ), lastAU15_LeftIntensity, lastAU15_RightIntensity );
			lastAU15_LeftIntensity = AU15LeftIntensityTB.Value.ToString( );
			lastAU15_RightIntensity = AU15RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU15Side_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU15LeftIntensity, AU15LeftIntensityTB, AU15LeftIntensityLevelLbl, AU15LeftIntensityLbl );
			AU15RightIntensityTB.Value = 0; AU15RightIntensityLevelLbl.Text = "0%";
			if( AU15Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU15LeftIntensityTB, AU15LeftIntensityLevelLbl, AU15LeftIntensityLbl, 368 );
				enableIntensityControls( AU15RightIntensity, AU15RightIntensityTB, AU15RightIntensityLevelLbl, AU15RightIntensityLbl );
				visibleIntensityControls( AU15RightIntensity, AU15RightIntensityTB, AU15RightIntensityLevelLbl, AU15RightIntensityLbl );
				AU15LeftIntensity.Text = "Left Intensity";
				AU15LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU15LeftIntensityTB, AU15LeftIntensityLevelLbl, AU15LeftIntensityLbl, 264 );
				disableIntensityControls( AU15RightIntensity, AU15RightIntensityTB, AU15RightIntensityLevelLbl, AU15RightIntensityLbl );
				invisibleIntensityControls( AU15RightIntensity, AU15RightIntensityTB, AU15RightIntensityLevelLbl, AU15RightIntensityLbl );
				AU15LeftIntensity.Text = "Intensity";
				AU15LeftIntensityLbl.Text = "Intensity";
			}
		}

        private void AU16_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU16Intensity, AU16IntensityTB, AU16IntensityLevelLbl, AU16IntensityLbl );
			if( !AU16.Checked )
			{
				AU16IntensityLevelLbl.Text = "0%";
				AU16IntensityTB.Value = 0;
				AU16Intensity.Text = "Intensity";
                hapFACS.AU16("0", lastAU16_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU16_Intensity = "0";
            }
        }

		private void AU16Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU16Intensity, AU16IntensityTB, AU16IntensityLevelLbl );
			hapFACS.AU16( AU16Intensity.SelectedItem.ToString( ), lastAU16_Intensity );
			lastAU16_Intensity = AU16Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU17_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU17Intensity, AU17IntensityTB, AU17IntensityLevelLbl, AU17IntensityLbl );
			if( !AU17.Checked )
			{
				AU17IntensityLevelLbl.Text = "0%";
				AU17IntensityTB.Value = 0; 
				AU17Intensity.Text = "Intensity";
                hapFACS.AU17("0", lastAU17_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU17_Intensity = "0";
            }
        }

		private void AU17Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU17Intensity, AU17IntensityTB, AU17IntensityLevelLbl );
			hapFACS.AU17( AU17Intensity.SelectedItem.ToString( ), lastAU17_Intensity );
			lastAU17_Intensity = AU17Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU18_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU18Intensity, AU18IntensityTB, AU18IntensityLevelLbl, AU18IntensityLbl );
			if( !AU18.Checked )
			{
				AU18IntensityLevelLbl.Text = "0%";
				AU18IntensityTB.Value = 0;
                AU18Intensity.Text = "Intensity";
                hapFACS.AU18("0", lastAU18_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU18_Intensity = "0";
            }
        }

		private void AU18Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU18Intensity, AU18IntensityTB, AU18IntensityLevelLbl );
			hapFACS.AU18( AU18Intensity.SelectedItem.ToString( ), lastAU18_Intensity );
			lastAU18_Intensity = AU18Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU20_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU20Intensity, AU20IntensityTB, AU20IntensityLevelLbl, AU20IntensityLbl );
			if( !AU20.Checked )
			{
				AU20IntensityLevelLbl.Text = "0%";
				AU20IntensityTB.Value = 0;
                AU20Intensity.Text = "Intensity";
                hapFACS.AU20("0", lastAU20_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU20_Intensity = "0";
            }
        }

		private void AU20Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU20Intensity, AU20IntensityTB, AU20IntensityLevelLbl );
			hapFACS.AU20( AU20Intensity.SelectedItem.ToString( ), lastAU20_Intensity );
			lastAU20_Intensity = AU20Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU22_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU22Intensity, AU22IntensityTB, AU22IntensityLevelLbl, AU22IntensityLbl );
			if( !AU22.Checked )
			{
				AU22IntensityLevelLbl.Text = "0%";
				AU22IntensityTB.Value = 0;
                AU22Intensity.Text = "Intensity";
                hapFACS.AU22("0", lastAU22_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU22_Intensity = "0";
            }
        }

		private void AU22Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU22Intensity, AU22IntensityTB, AU22IntensityLevelLbl );
			hapFACS.AU22( AU22Intensity.SelectedItem.ToString( ), lastAU22_Intensity );
			lastAU22_Intensity = AU22Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU23_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU23Intensity, AU23IntensityTB, AU23IntensityLevelLbl, AU23IntensityLbl );
			if( !AU23.Checked )
			{
				AU23IntensityLevelLbl.Text = "0%";
				AU23IntensityTB.Value = 0;
                AU23Intensity.Text = "Intensity";
                hapFACS.AU23("0", lastAU23_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU23_Intensity = "0";
            }
        }

		private void AU23Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU23Intensity, AU23IntensityTB, AU23IntensityLevelLbl );
			hapFACS.AU23( AU23Intensity.SelectedItem.ToString( ), lastAU23_Intensity );
			lastAU23_Intensity = AU23Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU24_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU24Intensity, AU24IntensityTB, AU24IntensityLevelLbl, AU24IntensityLbl );
			if( !AU24.Checked )
			{
				AU24IntensityLevelLbl.Text = "0%";
				AU24IntensityTB.Value = 0;
                AU24Intensity.Text = "Intensity";
                hapFACS.AU24("0", lastAU24_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU24_Intensity = "0";
            }
        }

		private void AU24Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU24Intensity, AU24IntensityTB, AU24IntensityLevelLbl );
			hapFACS.AU24( AU24Intensity.SelectedItem.ToString( ), lastAU24_Intensity );
			lastAU24_Intensity = AU24Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU25_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU25Intensity, AU25IntensityTB, AU25IntensityLevelLbl, AU25IntensityLbl );
			if( !AU25.Checked )
			{
				AU25IntensityLevelLbl.Text = "0%";
				AU25IntensityTB.Value = 0;
                AU25Intensity.Text = "Intensity";
                hapFACS.AU25("0", lastAU25_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU25_Intensity = "0";
            }
        }

		private void AU25Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU25Intensity, AU25IntensityTB, AU25IntensityLevelLbl );
			hapFACS.AU25( AU25Intensity.SelectedItem.ToString( ), lastAU25_Intensity );
			lastAU25_Intensity = AU25Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU26_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU26Intensity, AU26IntensityTB, AU26IntensityLevelLbl, AU26IntensityLbl );
			if( !AU26.Checked )
			{
				AU26IntensityLevelLbl.Text = "0%";
				AU26IntensityTB.Value = 0;
                AU26Intensity.Text = "Intensity";
                hapFACS.AU26("0", lastAU26_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU26_Intensity = "0";
            }
        }

		private void AU26Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU26Intensity, AU26IntensityTB, AU26IntensityLevelLbl );
			hapFACS.AU26( AU26Intensity.SelectedItem.ToString( ), lastAU26_Intensity );
			lastAU26_Intensity = AU26Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU27_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU27Intensity, AU27IntensityTB, AU27IntensityLevelLbl, AU27IntensityLbl );
			if( !AU27.Checked )
			{
				AU27IntensityLevelLbl.Text = "0%";
				AU27IntensityTB.Value = 0;
                AU27Intensity.Text = "Intensity";
                hapFACS.AU27("0", lastAU27_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU27_Intensity = "0";
            }
        }

		private void AU27Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU27Intensity, AU27IntensityTB, AU27IntensityLevelLbl );
			hapFACS.AU27( AU27Intensity.SelectedItem.ToString( ), lastAU27_Intensity );
			lastAU27_Intensity = AU27Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU28_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU28Intensity, AU28IntensityTB, AU28IntensityLevelLbl, AU28IntensityLbl );
			if( !AU28.Checked )
			{
				AU28IntensityLevelLbl.Text = "0%";
				AU28IntensityTB.Value = 0;
                AU28Intensity.Text = "Intensity";
                hapFACS.AU28("0", lastAU28_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU28_Intensity = "0";
            }
        }

		private void AU28Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU28Intensity, AU28IntensityTB, AU28IntensityLevelLbl );
			hapFACS.AU28( AU28Intensity.SelectedItem.ToString( ), lastAU28_Intensity );
			lastAU28_Intensity = AU28Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU38_CheckedChanged(object sender, EventArgs e)
        {
			if( AU38.Checked )
			{
				AU38LeftIntensity.Text = "Intensity";
				AU38Side.Enabled = true; AU38Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU38LeftIntensity, AU38LeftIntensityTB, AU38LeftIntensityLevelLbl, AU38LeftIntensityLbl );
				disableIntensityControls( AU38RightIntensity, AU38RightIntensityTB, AU38RightIntensityLevelLbl, AU38RightIntensityLbl );
				locateControls( AU38LeftIntensityTB, AU38LeftIntensityLevelLbl, AU38LeftIntensityLbl, 368 );
				visibleIntensityControls( AU38RightIntensity, AU38RightIntensityTB, AU38RightIntensityLevelLbl, AU38RightIntensityLbl );
				AU38LeftIntensityLevelLbl.Text = "0%";
				AU38LeftIntensityTB.Value = 0;
				AU38LeftIntensity.Text = "Intensity";
				AU38LeftIntensityLbl.Text = "Left intensity";
				AU38Side.Enabled = false; AU38Side.Text = "Side";
				AU38RightIntensity.Visible = false; AU38RightIntensity.Text = "Right Intensity";
				hapFACS.AU38( "0", "0", lastAU38_LeftIntensity, lastAU38_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU38_LeftIntensity = "0";
				lastAU38_RightIntensity = "0";
			}
        }

		private void AU38LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU38Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU38RightIntensity.SelectedIndex < 0 )
				{
					AU38RightIntensity.SelectedItem = "0"; AU38RightIntensity.Text = "Right Intensity";
					AU38RightIntensityTB.Value = 0; AU38RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU38RightIntensity, AU38RightIntensityTB, AU38RightIntensityLevelLbl, AU38RightIntensityLbl );
				AU38RightIntensity.Visible = true;
				setIntensityLabel( AU38LeftIntensity, AU38LeftIntensityTB, AU38LeftIntensityLevelLbl );
				hapFACS.AU38( AU38LeftIntensity.SelectedItem.ToString( ), AU38RightIntensity.SelectedItem.ToString( ), lastAU38_LeftIntensity, lastAU38_RightIntensity );
				lastAU38_LeftIntensity = AU38LeftIntensity.SelectedItem.ToString( );
				lastAU38_RightIntensity = AU38RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU38RightIntensity, AU38RightIntensityTB, AU38RightIntensityLevelLbl, AU38RightIntensityLbl );
				AU38RightIntensity.Visible = false;
				setIntensityLabel( AU38LeftIntensity, AU38LeftIntensityTB, AU38LeftIntensityLevelLbl );
				hapFACS.AU38( AU38LeftIntensityTB.Value.ToString( ), AU38LeftIntensityTB.Value.ToString( ), lastAU38_LeftIntensity, lastAU38_RightIntensity );
				lastAU38_LeftIntensity = AU38LeftIntensityTB.Value.ToString( );
				lastAU38_RightIntensity = AU38LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU38RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU38LeftIntensity.SelectedIndex < 0 )
			{
				AU38LeftIntensity.SelectedItem = "0"; AU38LeftIntensity.Text = "Left Intensity";
				AU38LeftIntensityTB.Value = 0; AU38LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU38RightIntensity, AU38RightIntensityTB, AU38RightIntensityLevelLbl );
			hapFACS.AU38( AU38LeftIntensityTB.Value.ToString( ), AU38RightIntensityTB.Value.ToString( ), lastAU38_LeftIntensity, lastAU38_RightIntensity );
			lastAU38_LeftIntensity = AU38LeftIntensityTB.Value.ToString( );
			lastAU38_RightIntensity = AU38RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU38Side_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU38LeftIntensity, AU38LeftIntensityTB, AU38LeftIntensityLevelLbl, AU38LeftIntensityLbl );
			AU38RightIntensityTB.Value = 0; AU38RightIntensityLevelLbl.Text = "0%";
			if( AU38Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU38LeftIntensityTB, AU38LeftIntensityLevelLbl, AU38LeftIntensityLbl, 368 );
				enableIntensityControls( AU38RightIntensity, AU38RightIntensityTB, AU38RightIntensityLevelLbl, AU38RightIntensityLbl );
				visibleIntensityControls( AU38RightIntensity, AU38RightIntensityTB, AU38RightIntensityLevelLbl, AU38RightIntensityLbl );
				AU38LeftIntensity.Text = "Left Intensity";
				AU38LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU38LeftIntensityTB, AU38LeftIntensityLevelLbl, AU38LeftIntensityLbl, 264 );
				disableIntensityControls( AU38RightIntensity, AU38RightIntensityTB, AU38RightIntensityLevelLbl, AU38RightIntensityLbl );
				invisibleIntensityControls( AU38RightIntensity, AU38RightIntensityTB, AU38RightIntensityLevelLbl, AU38RightIntensityLbl );
				AU38LeftIntensity.Text = "Intensity";
				AU38LeftIntensityLbl.Text = "Intensity";
			}
		}

        private void AU39_CheckedChanged(object sender, EventArgs e)
        {
			if( AU39.Checked )
			{
				AU39LeftIntensity.Text = "Intensity";
				AU39Side.Enabled = true; AU39Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU39LeftIntensity, AU39LeftIntensityTB, AU39LeftIntensityLevelLbl, AU39LeftIntensityLbl );
				disableIntensityControls( AU39RightIntensity, AU39RightIntensityTB, AU39RightIntensityLevelLbl, AU39RightIntensityLbl );
				locateControls( AU39LeftIntensityTB, AU39LeftIntensityLevelLbl, AU39LeftIntensityLbl, 368 );
				visibleIntensityControls( AU39RightIntensity, AU39RightIntensityTB, AU39RightIntensityLevelLbl, AU39RightIntensityLbl );
				AU39LeftIntensityLevelLbl.Text = "0%";
				AU39LeftIntensityTB.Value = 0;
				AU39LeftIntensity.Text = "Intensity";
				AU39LeftIntensityLbl.Text = "Left intensity";
				AU39Side.Enabled = false; AU39Side.Text = "Side";
				AU39RightIntensity.Visible = false; AU39RightIntensity.Text = "Right Intensity";
				hapFACS.AU39( "0", "0", lastAU39_LeftIntensity, lastAU39_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU39_LeftIntensity = "0";
				lastAU39_RightIntensity = "0";
			}
        }

		private void AU39LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU39Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU39RightIntensity.SelectedIndex < 0 )
				{
					AU39RightIntensity.SelectedItem = "0"; AU39RightIntensity.Text = "Right Intensity";
					AU39RightIntensityTB.Value = 0; AU39RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU39RightIntensity, AU39RightIntensityTB, AU39RightIntensityLevelLbl, AU39RightIntensityLbl );
				AU39RightIntensity.Visible = true;
				setIntensityLabel( AU39LeftIntensity, AU39LeftIntensityTB, AU39LeftIntensityLevelLbl );
				hapFACS.AU39( AU39LeftIntensity.SelectedItem.ToString( ), AU39RightIntensity.SelectedItem.ToString( ), lastAU39_LeftIntensity, lastAU39_RightIntensity );
				lastAU39_LeftIntensity = AU39LeftIntensity.SelectedItem.ToString( );
				lastAU39_RightIntensity = AU39RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU39RightIntensity, AU39RightIntensityTB, AU39RightIntensityLevelLbl, AU39RightIntensityLbl );
				AU39RightIntensity.Visible = false;
				setIntensityLabel( AU39LeftIntensity, AU39LeftIntensityTB, AU39LeftIntensityLevelLbl );
				hapFACS.AU39( AU39LeftIntensityTB.Value.ToString( ), AU39LeftIntensityTB.Value.ToString( ), lastAU39_LeftIntensity, lastAU39_RightIntensity );
				lastAU39_LeftIntensity = AU39LeftIntensityTB.Value.ToString( );
				lastAU39_RightIntensity = AU39LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU39RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU39LeftIntensity.SelectedIndex < 0 )
			{
				AU39LeftIntensity.SelectedItem = "0"; AU39LeftIntensity.Text = "Left Intensity";
				AU39LeftIntensityTB.Value = 0; AU39LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU39RightIntensity, AU39RightIntensityTB, AU39RightIntensityLevelLbl );
			hapFACS.AU39( AU39LeftIntensityTB.Value.ToString( ), AU39RightIntensityTB.Value.ToString( ), lastAU39_LeftIntensity, lastAU39_RightIntensity );
			lastAU39_LeftIntensity = AU39LeftIntensityTB.Value.ToString( );
			lastAU39_RightIntensity = AU39RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU39Side_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU39LeftIntensity, AU39LeftIntensityTB, AU39LeftIntensityLevelLbl, AU39LeftIntensityLbl );
			AU39RightIntensityTB.Value = 0; AU39RightIntensityLevelLbl.Text = "0%";
			if( AU39Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU39LeftIntensityTB, AU39LeftIntensityLevelLbl, AU39LeftIntensityLbl, 368 );
				enableIntensityControls( AU39RightIntensity, AU39RightIntensityTB, AU39RightIntensityLevelLbl, AU39RightIntensityLbl );
				visibleIntensityControls( AU39RightIntensity, AU39RightIntensityTB, AU39RightIntensityLevelLbl, AU39RightIntensityLbl );
				AU39LeftIntensity.Text = "Left Intensity";
				AU39LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU39LeftIntensityTB, AU39LeftIntensityLevelLbl, AU39LeftIntensityLbl, 264 );
				disableIntensityControls( AU39RightIntensity, AU39RightIntensityTB, AU39RightIntensityLevelLbl, AU39RightIntensityLbl );
				invisibleIntensityControls( AU39RightIntensity, AU39RightIntensityTB, AU39RightIntensityLevelLbl, AU39RightIntensityLbl );
				AU39LeftIntensity.Text = "Intensity";
				AU39LeftIntensityLbl.Text = "Intensity";
			}
		}

        private void AU41_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU41Intensity, AU41IntensityTB, AU41IntensityLevelLbl, AU41IntensityLbl );
			if( !AU41.Checked )
			{
				AU41IntensityLevelLbl.Text = "0%";
				AU41IntensityTB.Value = 0;
				AU41Intensity.Text = "Intensity";
                hapFACS.AU41("0", lastAU41_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU41_Intensity = "0";
            }
        }

		private void AU41Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU41Intensity, AU41IntensityTB, AU41IntensityLevelLbl );
			hapFACS.AU41( AU41Intensity.SelectedItem.ToString( ), lastAU41_Intensity );
			lastAU41_Intensity = AU41Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU42_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU42Intensity, AU42IntensityTB, AU42IntensityLevelLbl, AU42IntensityLbl );
			if( !AU42.Checked )
			{
				AU42IntensityLevelLbl.Text = "0%";
				AU42IntensityTB.Value = 0;
				AU42Intensity.Text = "Intensity";
                hapFACS.AU42("0", lastAU42_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU42_Intensity = "0";
            }
        }

		private void AU42Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU42Intensity, AU42IntensityTB, AU42IntensityLevelLbl );
			hapFACS.AU42( AU42Intensity.SelectedItem.ToString( ), lastAU42_Intensity );
			lastAU42_Intensity = AU42Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU43_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU43Intensity, AU43IntensityTB, AU43IntensityLevelLbl, AU43IntensityLbl );
			if( !AU43.Checked )
			{
				AU43IntensityLevelLbl.Text = "0%";
				AU43IntensityTB.Value = 0;
				AU43Intensity.Text = "Intensity";
                hapFACS.AU43("0", lastAU43_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU43_Intensity = "0";
            }
        }

		private void AU43Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU43Intensity, AU43IntensityTB, AU43IntensityLevelLbl );
			hapFACS.AU43( AU43Intensity.SelectedItem.ToString( ), lastAU43_Intensity );
			lastAU43_Intensity = AU43Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU44_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU44Intensity, AU44IntensityTB, AU44IntensityLevelLbl, AU44IntensityLbl );
			if( !AU44.Checked )
			{
				AU44IntensityLevelLbl.Text = "0%";
				AU44IntensityTB.Value = 0;
				AU44Intensity.Text = "Intensity";
                hapFACS.AU44("0", lastAU44_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU44_Intensity = "0";
            }
        }

		private void AU44Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU44Intensity, AU44IntensityTB, AU44IntensityLevelLbl );
			hapFACS.AU44( AU44Intensity.SelectedItem.ToString( ), lastAU44_Intensity );
			lastAU44_Intensity = AU44Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU45_Click( object sender, EventArgs e )
		{
			axActiveHaptekX1.HyperText = hapFACS.AU45( );
		}

		private void AU46_CheckedChanged( object sender, EventArgs e )
		{
			if( AU46.Checked )
				AU46Side.Enabled = true;
			else
			{
				AU46Side.Enabled = false;
				AU46Side.Text = "Side";
			}
		}

		private void AU46Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			axActiveHaptekX1.HyperText = hapFACS.AU46( AU46Side.SelectedItem.ToString( ) );
		}

        private void AU51_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU51Intensity, AU51IntensityTB, AU51IntensityLevelLbl, AU51IntensityLbl );
			if( !AU51.Checked )
			{
				AU51IntensityLevelLbl.Text = "0%";
				AU51IntensityTB.Value = 0;
                AU51Intensity.Text = "Intensity";
                hapFACS.AU51("0", lastAU51_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU51_Intensity = "0";
            }
        }

		private void AU51Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU51Intensity, AU51IntensityTB, AU51IntensityLevelLbl );
			hapFACS.AU51( AU51Intensity.SelectedItem.ToString( ), lastAU51_Intensity );
			lastAU51_Intensity = AU51Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU52_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU52Intensity, AU52IntensityTB, AU52IntensityLevelLbl, AU52IntensityLbl );
			if( !AU52.Checked )
			{
				AU52IntensityLevelLbl.Text = "0%";
				AU52IntensityTB.Value = 0;
                AU52Intensity.Text = "Intensity";
                hapFACS.AU52("0", lastAU52_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU52_Intensity = "0";
            }
        }

		private void AU52Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU52Intensity, AU52IntensityTB, AU52IntensityLevelLbl );
			hapFACS.AU52( AU52Intensity.SelectedItem.ToString( ), lastAU52_Intensity );
			lastAU52_Intensity = AU52Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU53_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU53Intensity, AU53IntensityTB, AU53IntensityLevelLbl, AU53IntensityLbl );
			if( !AU53.Checked )
			{
				AU53IntensityLevelLbl.Text = "0%";
				AU53IntensityTB.Value = 0;
                AU53Intensity.Text = "Intensity";
                hapFACS.AU53("0", lastAU53_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU53_Intensity = "0";
            }
        }

		private void AU53Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU53Intensity, AU53IntensityTB, AU53IntensityLevelLbl );
			hapFACS.AU53( AU53Intensity.SelectedItem.ToString( ), lastAU53_Intensity );
			lastAU53_Intensity = AU53Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU54_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU54Intensity, AU54IntensityTB, AU54IntensityLevelLbl, AU54IntensityLbl );
			if( !AU54.Checked )
			{
				AU54IntensityLevelLbl.Text = "0%";
				AU54IntensityTB.Value = 0;
                AU54Intensity.Text = "Intensity";
                hapFACS.AU54("0", lastAU54_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU54_Intensity = "0";
            }
        }

		private void AU54Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU54Intensity, AU54IntensityTB, AU54IntensityLevelLbl );
			hapFACS.AU54( AU54Intensity.SelectedItem.ToString( ), lastAU54_Intensity );
			lastAU54_Intensity = AU54Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU55_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU55Intensity, AU55IntensityTB, AU55IntensityLevelLbl, AU55IntensityLbl );
			if( !AU55.Checked )
			{
				AU55IntensityLevelLbl.Text = "0%";
				AU55IntensityTB.Value = 0;
                AU55Intensity.Text = "Intensity";
                hapFACS.AU55("0", lastAU55_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU55_Intensity = "0";
            }
        }

		private void AU55Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU55Intensity, AU55IntensityTB, AU55IntensityLevelLbl );
			hapFACS.AU55( AU55Intensity.SelectedItem.ToString( ), lastAU55_Intensity );
			lastAU55_Intensity = AU55Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU56_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU56Intensity, AU56IntensityTB, AU56IntensityLevelLbl, AU56IntensityLbl );
			if( !AU56.Checked )
			{
				AU56IntensityLevelLbl.Text = "0%";
				AU56IntensityTB.Value = 0;
                AU56Intensity.Text = "Intensity";
                hapFACS.AU56("0", lastAU56_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU56_Intensity = "0";
            }
        }

		private void AU56Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU56Intensity, AU56IntensityTB, AU56IntensityLevelLbl );
			hapFACS.AU56( AU56Intensity.SelectedItem.ToString( ), lastAU56_Intensity );
			lastAU56_Intensity = AU56Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU57_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU57Intensity, AU57IntensityTB, AU57IntensityLevelLbl, AU57IntensityLbl );
			if( !AU57.Checked )
			{
				AU57IntensityLevelLbl.Text = "0%";
				AU57IntensityTB.Value = 0;
                AU57Intensity.Text = "Intensity";
                hapFACS.AU57("0", lastAU57_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU57_Intensity = "0";
            }
        }

		private void AU57Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU57Intensity, AU57IntensityTB, AU57IntensityLevelLbl );
			hapFACS.AU57( AU57Intensity.SelectedItem.ToString( ), lastAU57_Intensity );
			lastAU57_Intensity = AU57Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

        private void AU58_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AU58Intensity, AU58IntensityTB, AU58IntensityLevelLbl, AU58IntensityLbl );
			if( !AU58.Checked )
			{
				AU58IntensityLevelLbl.Text = "0%";
				AU58IntensityTB.Value = 0;
                AU58Intensity.Text = "Intensity";
                hapFACS.AU58("0", lastAU58_Intensity);
                axActiveHaptekX1.HyperText = hapFACS.generateFace();
                lastAU58_Intensity = "0";
            }
        }

		private void AU58Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AU58Intensity, AU58IntensityTB, AU58IntensityLevelLbl );
			hapFACS.AU58( AU58Intensity.SelectedItem.ToString( ), lastAU58_Intensity );
			lastAU58_Intensity = AU58Intensity.SelectedItem.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AUM59_Click( object sender, EventArgs e )
		{
			axActiveHaptekX1.HyperText = hapFACS.AUM59( );
			System.Threading.Thread.Sleep( 2000 ); // turn back the stop swith to stop after the expression is done.
			axActiveHaptekX1.HyperText = @"\SetSwitch[switch= stop state=stop]";
		}

		private void AUM60_Click( object sender, EventArgs e )
		{
			axActiveHaptekX1.HyperText = hapFACS.AUM60( );
			System.Threading.Thread.Sleep( 2000 ); // turn back the stop swith to stop after the expression is done.
			axActiveHaptekX1.HyperText = @"\SetSwitch[switch= stop state=stop]";
		}

        private void AU61_CheckedChanged(object sender, EventArgs e)
        {
			if( AU61.Checked )
			{
				AU61LeftIntensity.Text = "Intensity";
				AU61Side.Enabled = true; AU61Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU61LeftIntensity, AU61LeftIntensityTB, AU61LeftIntensityLevelLbl, AU61LeftIntensityLbl );
				disableIntensityControls( AU61RightIntensity, AU61RightIntensityTB, AU61RightIntensityLevelLbl, AU61RightIntensityLbl );
				locateControls( AU61LeftIntensityTB, AU61LeftIntensityLevelLbl, AU61LeftIntensityLbl, 368 );
				visibleIntensityControls( AU61RightIntensity, AU61RightIntensityTB, AU61RightIntensityLevelLbl, AU61RightIntensityLbl );
				AU61LeftIntensityLevelLbl.Text = "0%";
				AU61LeftIntensityTB.Value = 0;
				AU61LeftIntensity.Text = "Intensity";
				AU61LeftIntensityLbl.Text = "Left intensity";
				AU61Side.Enabled = false; AU61Side.Text = "Side";
				AU61RightIntensity.Visible = false; AU61RightIntensity.Text = "Right Intensity";
				hapFACS.AU61( "0", "0", lastAU61_LeftIntensity, lastAU61_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU61_LeftIntensity = "0";
				lastAU61_RightIntensity = "0";
			}
        }

		private void AU61LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU61Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU61RightIntensity.SelectedIndex < 0 )
				{
					AU61RightIntensity.SelectedItem = "0"; AU61RightIntensity.Text = "Right Intensity";
					AU61RightIntensityTB.Value = 0; AU61RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU61RightIntensity, AU61RightIntensityTB, AU61RightIntensityLevelLbl, AU61RightIntensityLbl );
				AU61RightIntensity.Visible = true;
				setIntensityLabel( AU61LeftIntensity, AU61LeftIntensityTB, AU61LeftIntensityLevelLbl );
				hapFACS.AU61( AU61LeftIntensity.SelectedItem.ToString( ), AU61RightIntensity.SelectedItem.ToString( ), lastAU61_LeftIntensity, lastAU61_RightIntensity );
				lastAU61_LeftIntensity = AU61LeftIntensity.SelectedItem.ToString( );
				lastAU61_RightIntensity = AU61RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU61RightIntensity, AU61RightIntensityTB, AU61RightIntensityLevelLbl, AU61RightIntensityLbl );
				AU61RightIntensity.Visible = false;
				setIntensityLabel( AU61LeftIntensity, AU61LeftIntensityTB, AU61LeftIntensityLevelLbl );
				hapFACS.AU61( AU61LeftIntensityTB.Value.ToString( ), AU61LeftIntensityTB.Value.ToString( ), lastAU61_LeftIntensity, lastAU61_RightIntensity );
				lastAU61_LeftIntensity = AU61LeftIntensityTB.Value.ToString( );
				lastAU61_RightIntensity = AU61LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU61RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU61LeftIntensity.SelectedIndex < 0 )
			{
				AU61LeftIntensity.SelectedItem = "0"; AU61LeftIntensity.Text = "Left Intensity";
				AU61LeftIntensityTB.Value = 0; AU61LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU61RightIntensity, AU61RightIntensityTB, AU61RightIntensityLevelLbl );
			hapFACS.AU61( AU61LeftIntensityTB.Value.ToString( ), AU61RightIntensityTB.Value.ToString( ), lastAU61_LeftIntensity, lastAU61_RightIntensity );
			lastAU61_LeftIntensity = AU61LeftIntensityTB.Value.ToString( );
			lastAU61_RightIntensity = AU61RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU61Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU61LeftIntensity, AU61LeftIntensityTB, AU61LeftIntensityLevelLbl, AU61LeftIntensityLbl );
			AU61RightIntensityTB.Value = 0; AU61RightIntensityLevelLbl.Text = "0%";
			if( AU61Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU61LeftIntensityTB, AU61LeftIntensityLevelLbl, AU61LeftIntensityLbl, 368 );
				enableIntensityControls( AU61RightIntensity, AU61RightIntensityTB, AU61RightIntensityLevelLbl, AU61RightIntensityLbl );
				visibleIntensityControls( AU61RightIntensity, AU61RightIntensityTB, AU61RightIntensityLevelLbl, AU61RightIntensityLbl );
				AU61LeftIntensity.Text = "Left Intensity";
				AU61LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU61LeftIntensityTB, AU61LeftIntensityLevelLbl, AU61LeftIntensityLbl, 264 );
				disableIntensityControls( AU61RightIntensity, AU61RightIntensityTB, AU61RightIntensityLevelLbl, AU61RightIntensityLbl );
				invisibleIntensityControls( AU61RightIntensity, AU61RightIntensityTB, AU61RightIntensityLevelLbl, AU61RightIntensityLbl );
				AU61LeftIntensity.Text = "Intensity";
				AU61LeftIntensityLbl.Text = "Intensity";
			}
		}

        private void AU62_CheckedChanged(object sender, EventArgs e)
        {
			if( AU62.Checked )
			{
				AU62LeftIntensity.Text = "Intensity";
				AU62Side.Enabled = true; AU62Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU62LeftIntensity, AU62LeftIntensityTB, AU62LeftIntensityLevelLbl, AU62LeftIntensityLbl );
				disableIntensityControls( AU62RightIntensity, AU62RightIntensityTB, AU62RightIntensityLevelLbl, AU62RightIntensityLbl );
				locateControls( AU62LeftIntensityTB, AU62LeftIntensityLevelLbl, AU62LeftIntensityLbl, 368 );
				visibleIntensityControls( AU62RightIntensity, AU62RightIntensityTB, AU62RightIntensityLevelLbl, AU62RightIntensityLbl );
				AU62LeftIntensityLevelLbl.Text = "0%";
				AU62LeftIntensityTB.Value = 0;
				AU62LeftIntensity.Text = "Intensity";
				AU62LeftIntensityLbl.Text = "Left intensity";
				AU62Side.Enabled = false; AU62Side.Text = "Side";
				AU62RightIntensity.Visible = false; AU62RightIntensity.Text = "Right Intensity";
				hapFACS.AU62( "0", "0", lastAU62_LeftIntensity, lastAU62_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU62_LeftIntensity = "0";
				lastAU62_RightIntensity = "0";
			}
        }

		private void AU62LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU62Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU62RightIntensity.SelectedIndex < 0 )
				{
					AU62RightIntensity.SelectedItem = "0"; AU62RightIntensity.Text = "Right Intensity";
					AU62RightIntensityTB.Value = 0; AU62RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU62RightIntensity, AU62RightIntensityTB, AU62RightIntensityLevelLbl, AU62RightIntensityLbl );
				AU62RightIntensity.Visible = true;
				setIntensityLabel( AU62LeftIntensity, AU62LeftIntensityTB, AU62LeftIntensityLevelLbl );
				hapFACS.AU62( AU62LeftIntensity.SelectedItem.ToString( ), AU62RightIntensity.SelectedItem.ToString( ), lastAU62_LeftIntensity, lastAU62_RightIntensity );
				lastAU62_LeftIntensity = AU62LeftIntensity.SelectedItem.ToString( );
				lastAU62_RightIntensity = AU62RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU62RightIntensity, AU62RightIntensityTB, AU62RightIntensityLevelLbl, AU62RightIntensityLbl );
				AU62RightIntensity.Visible = false;
				setIntensityLabel( AU62LeftIntensity, AU62LeftIntensityTB, AU62LeftIntensityLevelLbl );
				hapFACS.AU62( AU62LeftIntensityTB.Value.ToString( ), AU62LeftIntensityTB.Value.ToString( ), lastAU62_LeftIntensity, lastAU62_RightIntensity );
				lastAU62_LeftIntensity = AU62LeftIntensityTB.Value.ToString( );
				lastAU62_RightIntensity = AU62LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU62RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU62LeftIntensity.SelectedIndex < 0 )
			{
				AU62LeftIntensity.SelectedItem = "0"; AU62LeftIntensity.Text = "Left Intensity";
				AU62LeftIntensityTB.Value = 0; AU62LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU62RightIntensity, AU62RightIntensityTB, AU62RightIntensityLevelLbl );
			hapFACS.AU62( AU62LeftIntensityTB.Value.ToString( ), AU62RightIntensityTB.Value.ToString( ), lastAU62_LeftIntensity, lastAU62_RightIntensity );
			lastAU62_LeftIntensity = AU62LeftIntensityTB.Value.ToString( );
			lastAU62_RightIntensity = AU62RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU62Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU62LeftIntensity, AU62LeftIntensityTB, AU62LeftIntensityLevelLbl, AU62LeftIntensityLbl );
			AU62RightIntensityTB.Value = 0; AU62RightIntensityLevelLbl.Text = "0%";
			if( AU62Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU62LeftIntensityTB, AU62LeftIntensityLevelLbl, AU62LeftIntensityLbl, 368 );
				enableIntensityControls( AU62RightIntensity, AU62RightIntensityTB, AU62RightIntensityLevelLbl, AU62RightIntensityLbl );
				visibleIntensityControls( AU62RightIntensity, AU62RightIntensityTB, AU62RightIntensityLevelLbl, AU62RightIntensityLbl );
				AU62LeftIntensity.Text = "Left Intensity";
				AU62LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU62LeftIntensityTB, AU62LeftIntensityLevelLbl, AU62LeftIntensityLbl, 264 );
				disableIntensityControls( AU62RightIntensity, AU62RightIntensityTB, AU62RightIntensityLevelLbl, AU62RightIntensityLbl );
				invisibleIntensityControls( AU62RightIntensity, AU62RightIntensityTB, AU62RightIntensityLevelLbl, AU62RightIntensityLbl );
				AU62LeftIntensity.Text = "Intensity";
				AU62LeftIntensityLbl.Text = "Intensity";
			}
		}

        private void AU63_CheckedChanged(object sender, EventArgs e)
        {
			if( AU63.Checked )
			{
				AU63LeftIntensity.Text = "Intensity";
				AU63Side.Enabled = true; AU63Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU63LeftIntensity, AU63LeftIntensityTB, AU63LeftIntensityLevelLbl, AU63LeftIntensityLbl );
				disableIntensityControls( AU63RightIntensity, AU63RightIntensityTB, AU63RightIntensityLevelLbl, AU63RightIntensityLbl );
				locateControls( AU63LeftIntensityTB, AU63LeftIntensityLevelLbl, AU63LeftIntensityLbl, 368 );
				visibleIntensityControls( AU63RightIntensity, AU63RightIntensityTB, AU63RightIntensityLevelLbl, AU63RightIntensityLbl );
				AU63LeftIntensityLevelLbl.Text = "0%";
				AU63LeftIntensityTB.Value = 0;
				AU63LeftIntensity.Text = "Intensity";
				AU63LeftIntensityLbl.Text = "Left intensity";
				AU63Side.Enabled = false; AU63Side.Text = "Side";
				AU63RightIntensity.Visible = false; AU63RightIntensity.Text = "Right Intensity";
				hapFACS.AU63( "0", "0", lastAU63_LeftIntensity, lastAU63_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU63_LeftIntensity = "0";
				lastAU63_RightIntensity = "0";
			}
        }

		private void AU63LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU63Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU63RightIntensity.SelectedIndex < 0 )
				{
					AU63RightIntensity.SelectedItem = "0"; AU63RightIntensity.Text = "Right Intensity";
					AU63RightIntensityTB.Value = 0; AU63RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU63RightIntensity, AU63RightIntensityTB, AU63RightIntensityLevelLbl, AU63RightIntensityLbl );
				AU63RightIntensity.Visible = true;
				setIntensityLabel( AU63LeftIntensity, AU63LeftIntensityTB, AU63LeftIntensityLevelLbl );
				hapFACS.AU63( AU63LeftIntensity.SelectedItem.ToString( ), AU63RightIntensity.SelectedItem.ToString( ), lastAU63_LeftIntensity, lastAU63_RightIntensity );
				lastAU63_LeftIntensity = AU63LeftIntensity.SelectedItem.ToString( );
				lastAU63_RightIntensity = AU63RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU63RightIntensity, AU63RightIntensityTB, AU63RightIntensityLevelLbl, AU63RightIntensityLbl );
				AU63RightIntensity.Visible = false;
				setIntensityLabel( AU63LeftIntensity, AU63LeftIntensityTB, AU63LeftIntensityLevelLbl );
				hapFACS.AU63( AU63LeftIntensityTB.Value.ToString( ), AU63LeftIntensityTB.Value.ToString( ), lastAU63_LeftIntensity, lastAU63_RightIntensity );
				lastAU63_LeftIntensity = AU63LeftIntensityTB.Value.ToString( );
				lastAU63_RightIntensity = AU63LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU63RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU63LeftIntensity.SelectedIndex < 0 )
			{
				AU63LeftIntensity.SelectedItem = "0"; AU63LeftIntensity.Text = "Left Intensity";
				AU63LeftIntensityTB.Value = 0; AU63LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU63RightIntensity, AU63RightIntensityTB, AU63RightIntensityLevelLbl );
			hapFACS.AU63( AU63LeftIntensityTB.Value.ToString( ), AU63RightIntensityTB.Value.ToString( ), lastAU63_LeftIntensity, lastAU63_RightIntensity );
			lastAU63_LeftIntensity = AU63LeftIntensityTB.Value.ToString( );
			lastAU63_RightIntensity = AU63RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU63Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU63LeftIntensity, AU63LeftIntensityTB, AU63LeftIntensityLevelLbl, AU63LeftIntensityLbl );
			AU63RightIntensityTB.Value = 0; AU63RightIntensityLevelLbl.Text = "0%";
			if( AU63Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU63LeftIntensityTB, AU63LeftIntensityLevelLbl, AU63LeftIntensityLbl, 368 );
				enableIntensityControls( AU63RightIntensity, AU63RightIntensityTB, AU63RightIntensityLevelLbl, AU63RightIntensityLbl );
				visibleIntensityControls( AU63RightIntensity, AU63RightIntensityTB, AU63RightIntensityLevelLbl, AU63RightIntensityLbl );
				AU63LeftIntensity.Text = "Left Intensity";
				AU63LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU63LeftIntensityTB, AU63LeftIntensityLevelLbl, AU63LeftIntensityLbl, 264 );
				disableIntensityControls( AU63RightIntensity, AU63RightIntensityTB, AU63RightIntensityLevelLbl, AU63RightIntensityLbl );
				invisibleIntensityControls( AU63RightIntensity, AU63RightIntensityTB, AU63RightIntensityLevelLbl, AU63RightIntensityLbl );
				AU63LeftIntensity.Text = "Intensity";
				AU63LeftIntensityLbl.Text = "Intensity";
			}
		}

		private void AU64_CheckedChanged(object sender, EventArgs e)
        {
			if( AU64.Checked )
			{
				AU64LeftIntensity.Text = "Intensity";
				AU64Side.Enabled = true; AU64Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU64LeftIntensity, AU64LeftIntensityTB, AU64LeftIntensityLevelLbl, AU64LeftIntensityLbl );
				disableIntensityControls( AU64RightIntensity, AU64RightIntensityTB, AU64RightIntensityLevelLbl, AU64RightIntensityLbl );
				locateControls( AU64LeftIntensityTB, AU64LeftIntensityLevelLbl, AU64LeftIntensityLbl, 368 );
				visibleIntensityControls( AU64RightIntensity, AU64RightIntensityTB, AU64RightIntensityLevelLbl, AU64RightIntensityLbl );
				AU64LeftIntensityLevelLbl.Text = "0%";
				AU64LeftIntensityTB.Value = 0;
				AU64LeftIntensity.Text = "Intensity";
				AU64LeftIntensityLbl.Text = "Left intensity";
				AU64Side.Enabled = false; AU64Side.Text = "Side";
				AU64RightIntensity.Visible = false; AU64RightIntensity.Text = "Right Intensity";
				hapFACS.AU64( "0", "0", lastAU64_LeftIntensity, lastAU64_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU64_LeftIntensity = "0";
				lastAU64_RightIntensity = "0";
			}
        }

		private void AU64LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU64Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU64RightIntensity.SelectedIndex < 0 )
				{
					AU64RightIntensity.SelectedItem = "0"; AU64RightIntensity.Text = "Right Intensity";
					AU64RightIntensityTB.Value = 0; AU64RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU64RightIntensity, AU64RightIntensityTB, AU64RightIntensityLevelLbl, AU64RightIntensityLbl );
				AU64RightIntensity.Visible = true;
				setIntensityLabel( AU64LeftIntensity, AU64LeftIntensityTB, AU64LeftIntensityLevelLbl );
				hapFACS.AU64( AU64LeftIntensity.SelectedItem.ToString( ), AU64RightIntensity.SelectedItem.ToString( ), lastAU64_LeftIntensity, lastAU64_RightIntensity );
				lastAU64_LeftIntensity = AU64LeftIntensity.SelectedItem.ToString( );
				lastAU64_RightIntensity = AU64RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU64RightIntensity, AU64RightIntensityTB, AU64RightIntensityLevelLbl, AU64RightIntensityLbl );
				AU64RightIntensity.Visible = false;
				setIntensityLabel( AU64LeftIntensity, AU64LeftIntensityTB, AU64LeftIntensityLevelLbl );
				hapFACS.AU64( AU64LeftIntensityTB.Value.ToString( ), AU64LeftIntensityTB.Value.ToString( ), lastAU64_LeftIntensity, lastAU64_RightIntensity );
				lastAU64_LeftIntensity = AU64LeftIntensityTB.Value.ToString( );
				lastAU64_RightIntensity = AU64LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU64RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU64LeftIntensity.SelectedIndex < 0 )
			{
				AU64LeftIntensity.SelectedItem = "0"; AU64LeftIntensity.Text = "Left Intensity";
				AU64LeftIntensityTB.Value = 0; AU64LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU64RightIntensity, AU64RightIntensityTB, AU64RightIntensityLevelLbl );
			hapFACS.AU64( AU64LeftIntensityTB.Value.ToString( ), AU64RightIntensityTB.Value.ToString( ), lastAU64_LeftIntensity, lastAU64_RightIntensity );
			lastAU64_LeftIntensity = AU64LeftIntensityTB.Value.ToString( );
			lastAU64_RightIntensity = AU64RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU64Intensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU64LeftIntensity, AU64LeftIntensityTB, AU64LeftIntensityLevelLbl, AU64LeftIntensityLbl );
			AU64RightIntensityTB.Value = 0; AU64RightIntensityLevelLbl.Text = "0%";
			if( AU64Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU64LeftIntensityTB, AU64LeftIntensityLevelLbl, AU64LeftIntensityLbl, 368 );
				enableIntensityControls( AU64RightIntensity, AU64RightIntensityTB, AU64RightIntensityLevelLbl, AU64RightIntensityLbl );
				visibleIntensityControls( AU64RightIntensity, AU64RightIntensityTB, AU64RightIntensityLevelLbl, AU64RightIntensityLbl );
				AU64LeftIntensity.Text = "Left Intensity";
				AU64LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU64LeftIntensityTB, AU64LeftIntensityLevelLbl, AU64LeftIntensityLbl, 264 );
				disableIntensityControls( AU64RightIntensity, AU64RightIntensityTB, AU64RightIntensityLevelLbl, AU64RightIntensityLbl );
				invisibleIntensityControls( AU64RightIntensity, AU64RightIntensityTB, AU64RightIntensityLevelLbl, AU64RightIntensityLbl );
				AU64LeftIntensity.Text = "Intensity";
				AU64LeftIntensityLbl.Text = "Intensity";
			}
		}

        private void AU65_CheckedChanged(object sender, EventArgs e)
        {
			if( AU65.Checked )
			{
				AU65LeftIntensity.Text = "Intensity";
				AU65Side.Enabled = true; AU65Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU65LeftIntensity, AU65LeftIntensityTB, AU65LeftIntensityLevelLbl, AU65LeftIntensityLbl );
				disableIntensityControls( AU65RightIntensity, AU65RightIntensityTB, AU65RightIntensityLevelLbl, AU65RightIntensityLbl );
				locateControls( AU65LeftIntensityTB, AU65LeftIntensityLevelLbl, AU65LeftIntensityLbl, 368 );
				visibleIntensityControls( AU65RightIntensity, AU65RightIntensityTB, AU65RightIntensityLevelLbl, AU65RightIntensityLbl );
				AU65LeftIntensityLevelLbl.Text = "0%";
				AU65LeftIntensityTB.Value = 0;
				AU65LeftIntensity.Text = "Intensity";
				AU65LeftIntensityLbl.Text = "Left intensity";
				AU65Side.Enabled = false; AU65Side.Text = "Side";
				AU65RightIntensity.Visible = false; AU65RightIntensity.Text = "Right Intensity";
				hapFACS.AU65( "0", "0", lastAU65_LeftIntensity, lastAU65_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU65_LeftIntensity = "0";
				lastAU65_RightIntensity = "0";
			}
        }

		private void AU65LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU65Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU65RightIntensity.SelectedIndex < 0 )
				{
					AU65RightIntensity.SelectedItem = "0"; AU65RightIntensity.Text = "Right Intensity";
					AU65RightIntensityTB.Value = 0; AU65RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU65RightIntensity, AU65RightIntensityTB, AU65RightIntensityLevelLbl, AU65RightIntensityLbl );
				AU65RightIntensity.Visible = true;
				setIntensityLabel( AU65LeftIntensity, AU65LeftIntensityTB, AU65LeftIntensityLevelLbl );
				hapFACS.AU65( AU65LeftIntensity.SelectedItem.ToString( ), AU65RightIntensity.SelectedItem.ToString( ), lastAU65_LeftIntensity, lastAU65_RightIntensity );
				lastAU65_LeftIntensity = AU65LeftIntensity.SelectedItem.ToString( );
				lastAU65_RightIntensity = AU65RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU65RightIntensity, AU65RightIntensityTB, AU65RightIntensityLevelLbl, AU65RightIntensityLbl );
				AU65RightIntensity.Visible = false;
				setIntensityLabel( AU65LeftIntensity, AU65LeftIntensityTB, AU65LeftIntensityLevelLbl );
				hapFACS.AU65( AU65LeftIntensityTB.Value.ToString( ), AU65LeftIntensityTB.Value.ToString( ), lastAU65_LeftIntensity, lastAU65_RightIntensity );
				lastAU65_LeftIntensity = AU65LeftIntensityTB.Value.ToString( );
				lastAU65_RightIntensity = AU65LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU65RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU65LeftIntensity.SelectedIndex < 0 )
			{
				AU65LeftIntensity.SelectedItem = "0"; AU65LeftIntensity.Text = "Left Intensity";
				AU65LeftIntensityTB.Value = 0; AU65LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU65RightIntensity, AU65RightIntensityTB, AU65RightIntensityLevelLbl );
			hapFACS.AU65( AU65LeftIntensityTB.Value.ToString( ), AU65RightIntensityTB.Value.ToString( ), lastAU65_LeftIntensity, lastAU65_RightIntensity );
			lastAU65_LeftIntensity = AU65LeftIntensityTB.Value.ToString( );
			lastAU65_RightIntensity = AU65RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}
		
		private void AU65Side_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU65LeftIntensity, AU65LeftIntensityTB, AU65LeftIntensityLevelLbl, AU65LeftIntensityLbl );
			AU65RightIntensityTB.Value = 0; AU65RightIntensityLevelLbl.Text = "0%";
			if( AU65Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU65LeftIntensityTB, AU65LeftIntensityLevelLbl, AU65LeftIntensityLbl, 368 );
				enableIntensityControls( AU65RightIntensity, AU65RightIntensityTB, AU65RightIntensityLevelLbl, AU65RightIntensityLbl );
				visibleIntensityControls( AU65RightIntensity, AU65RightIntensityTB, AU65RightIntensityLevelLbl, AU65RightIntensityLbl );
				AU65LeftIntensity.Text = "Left Intensity";
				AU65LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU65LeftIntensityTB, AU65LeftIntensityLevelLbl, AU65LeftIntensityLbl, 264 );
				disableIntensityControls( AU65RightIntensity, AU65RightIntensityTB, AU65RightIntensityLevelLbl, AU65RightIntensityLbl );
				invisibleIntensityControls( AU65RightIntensity, AU65RightIntensityTB, AU65RightIntensityLevelLbl, AU65RightIntensityLbl );
				AU65LeftIntensity.Text = "Intensity";
				AU65LeftIntensityLbl.Text = "Intensity";
			}
		}

        private void AU66_CheckedChanged(object sender, EventArgs e)
        {
			if( AU66.Checked )
			{
				AU66LeftIntensity.Text = "Intensity";
				AU66Side.Enabled = true; AU66Side.Text = "Side";
			}
			else
			{
				disableIntensityControls( AU66LeftIntensity, AU66LeftIntensityTB, AU66LeftIntensityLevelLbl, AU66LeftIntensityLbl );
				disableIntensityControls( AU66RightIntensity, AU66RightIntensityTB, AU66RightIntensityLevelLbl, AU66RightIntensityLbl );
				locateControls( AU66LeftIntensityTB, AU66LeftIntensityLevelLbl, AU66LeftIntensityLbl, 368 );
				visibleIntensityControls( AU66RightIntensity, AU66RightIntensityTB, AU66RightIntensityLevelLbl, AU66RightIntensityLbl );
				AU66LeftIntensityLevelLbl.Text = "0%";
				AU66LeftIntensityTB.Value = 0;
				AU66LeftIntensity.Text = "Intensity";
				AU66LeftIntensityLbl.Text = "Left intensity";
				AU66Side.Enabled = false; AU66Side.Text = "Side";
				AU66RightIntensity.Visible = false; AU66RightIntensity.Text = "Right Intensity";
				hapFACS.AU66( "0", "0", lastAU66_LeftIntensity, lastAU66_RightIntensity );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
				lastAU66_LeftIntensity = "0";
				lastAU66_RightIntensity = "0";
			}
        }

		private void AU66LeftIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU66Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				if( AU66RightIntensity.SelectedIndex < 0 )
				{
					AU66RightIntensity.SelectedItem = "0"; AU66RightIntensity.Text = "Right Intensity";
					AU66RightIntensityTB.Value = 0; AU66RightIntensityLevelLbl.Text = "0%";
				}
				enableIntensityControls( AU66RightIntensity, AU66RightIntensityTB, AU66RightIntensityLevelLbl, AU66RightIntensityLbl );
				AU66RightIntensity.Visible = true;
				setIntensityLabel( AU66LeftIntensity, AU66LeftIntensityTB, AU66LeftIntensityLevelLbl );
				hapFACS.AU66( AU66LeftIntensity.SelectedItem.ToString( ), AU66RightIntensity.SelectedItem.ToString( ), lastAU66_LeftIntensity, lastAU66_RightIntensity );
				lastAU66_LeftIntensity = AU66LeftIntensity.SelectedItem.ToString( );
				lastAU66_RightIntensity = AU66RightIntensity.SelectedItem.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
			else
			{
				disableIntensityControls( AU66RightIntensity, AU66RightIntensityTB, AU66RightIntensityLevelLbl, AU66RightIntensityLbl );
				AU66RightIntensity.Visible = false;
				setIntensityLabel( AU66LeftIntensity, AU66LeftIntensityTB, AU66LeftIntensityLevelLbl );
				hapFACS.AU66( AU66LeftIntensityTB.Value.ToString( ), AU66LeftIntensityTB.Value.ToString( ), lastAU66_LeftIntensity, lastAU66_RightIntensity );
				lastAU66_LeftIntensity = AU66LeftIntensityTB.Value.ToString( );
				lastAU66_RightIntensity = AU66LeftIntensityTB.Value.ToString( );
				axActiveHaptekX1.HyperText = hapFACS.generateFace( );
			}
		}

		private void AU66RightIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( AU66LeftIntensity.SelectedIndex < 0 )
			{
				AU66LeftIntensity.SelectedItem = "0"; AU66LeftIntensity.Text = "Left Intensity";
				AU66LeftIntensityTB.Value = 0; AU66LeftIntensityLevelLbl.Text = "0%";
			}
			setIntensityLabel( AU66RightIntensity, AU66RightIntensityTB, AU66RightIntensityLevelLbl );
			hapFACS.AU66( AU66LeftIntensityTB.Value.ToString( ), AU66RightIntensityTB.Value.ToString( ), lastAU66_LeftIntensity, lastAU66_RightIntensity );
			lastAU66_LeftIntensity = AU66LeftIntensityTB.Value.ToString( );
			lastAU66_RightIntensity = AU66RightIntensityTB.Value.ToString( );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU66Side_SelectedIndexChanged( object sender, EventArgs e )
		{
			enableIntensityControls( AU66LeftIntensity, AU66LeftIntensityTB, AU66LeftIntensityLevelLbl, AU66LeftIntensityLbl );
			AU66RightIntensityTB.Value = 0; AU66RightIntensityLevelLbl.Text = "0%";
			if( AU66Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				locateControls( AU66LeftIntensityTB, AU66LeftIntensityLevelLbl, AU66LeftIntensityLbl, 368 );
				enableIntensityControls( AU66RightIntensity, AU66RightIntensityTB, AU66RightIntensityLevelLbl, AU66RightIntensityLbl );
				visibleIntensityControls( AU66RightIntensity, AU66RightIntensityTB, AU66RightIntensityLevelLbl, AU66RightIntensityLbl );
				AU66LeftIntensity.Text = "Left Intensity";
				AU66LeftIntensityLbl.Text = "Left intensity";
			}
			else
			{
				locateControls( AU66LeftIntensityTB, AU66LeftIntensityLevelLbl, AU66LeftIntensityLbl, 264 );
				disableIntensityControls( AU66RightIntensity, AU66RightIntensityTB, AU66RightIntensityLevelLbl, AU66RightIntensityLbl );
				invisibleIntensityControls( AU66RightIntensity, AU66RightIntensityTB, AU66RightIntensityLevelLbl, AU66RightIntensityLbl );
				AU66LeftIntensity.Text = "Intensity";
				AU66LeftIntensityLbl.Text = "Intensity";
			}
		}

		#endregion

			#region The following methods are activated when AUs are selected in the video generation, and when their corresponsinf remove buttons are clicked
		private void videoAUcombo1_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo1, videoSideCombo1, videoStartCombo1, videoEndCombo1, videoStartTxt1, videoEndTxt1, removeBtn1, secLbl1, secLbl1_2  );
        }

        private void removeBtn1_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo1, videoSideCombo1, videoStartCombo1, videoEndCombo1, videoStartTxt1, videoEndTxt1, removeBtn1, secLbl1, secLbl1_2 );
        }

        private void videoAUcombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo2, videoSideCombo2, videoStartCombo2, videoEndCombo2, videoStartTxt2, videoEndTxt2, removeBtn2, secLbl2, secLbl2_2 );
        }

        private void removeBtn2_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo2, videoSideCombo2, videoStartCombo2, videoEndCombo2, videoStartTxt2, videoEndTxt2, removeBtn2, secLbl2, secLbl2_2 );
        }

        private void videoAUcombo3_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo3, videoSideCombo3, videoStartCombo3, videoEndCombo3, videoStartTxt3, videoEndTxt3, removeBtn3, secLbl3, secLbl3_2 );
        }

        private void removeBtn3_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo3, videoSideCombo3, videoStartCombo3, videoEndCombo3, videoStartTxt3, videoEndTxt3, removeBtn3, secLbl3, secLbl3_2 );
        }

        private void videoAUcombo4_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo4, videoSideCombo4, videoStartCombo4, videoEndCombo4, videoStartTxt4, videoEndTxt4, removeBtn4, secLbl4, secLbl4_2 );
        }

        private void removeBtn4_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo4, videoSideCombo4, videoStartCombo4, videoEndCombo4, videoStartTxt4, videoEndTxt4, removeBtn4, secLbl4, secLbl4_2 );
        }

        private void videoAUcombo5_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo5, videoSideCombo5, videoStartCombo5, videoEndCombo5, videoStartTxt5, videoEndTxt5, removeBtn5, secLbl5, secLbl5_2 );
        }

        private void removeBtn5_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo5, videoSideCombo5, videoStartCombo5, videoEndCombo5, videoStartTxt5, videoEndTxt5, removeBtn5, secLbl5, secLbl5_2 );
        }

        private void videoAUcombo6_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo6, videoSideCombo6, videoStartCombo6, videoEndCombo6, videoStartTxt6, videoEndTxt6, removeBtn6, secLbl6, secLbl6_2 );
        }

        private void removeBtn6_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo6, videoSideCombo6, videoStartCombo6, videoEndCombo6, videoStartTxt6, videoEndTxt6, removeBtn6, secLbl6, secLbl6_2 );
        }

        private void videoAUcombo7_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo7, videoSideCombo7, videoStartCombo7, videoEndCombo7, videoStartTxt7, videoEndTxt7, removeBtn7, secLbl7, secLbl7_2 );
        }

        private void removeBtn7_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo7, videoSideCombo7, videoStartCombo7, videoEndCombo7, videoStartTxt7, videoEndTxt7, removeBtn7, secLbl7, secLbl7_2 );
        }

        private void videoAUcombo8_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo8, videoSideCombo8, videoStartCombo8, videoEndCombo8, videoStartTxt8, videoEndTxt8, removeBtn8, secLbl8, secLbl8_2 );
        }

        private void removeBtn8_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo8, videoSideCombo8, videoStartCombo8, videoEndCombo8, videoStartTxt8, videoEndTxt8, removeBtn8, secLbl8, secLbl8_2 );
        }

        private void videoAUcombo9_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo9, videoSideCombo9, videoStartCombo9, videoEndCombo9, videoStartTxt9, videoEndTxt9, removeBtn9, secLbl9, secLbl9_2 );
        }

        private void removeBtn9_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo9, videoSideCombo9, videoStartCombo9, videoEndCombo9, videoStartTxt9, videoEndTxt9, removeBtn9, secLbl9, secLbl9_2 );
        }

        private void videoAUcombo10_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo10, videoSideCombo10, videoStartCombo10, videoEndCombo10, videoStartTxt10, videoEndTxt10, removeBtn10, secLbl10, secLbl10_2 );
        }

        private void removeBtn10_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo10, videoSideCombo10, videoStartCombo10, videoEndCombo10, videoStartTxt10, videoEndTxt10, removeBtn10, secLbl10, secLbl10_2 );
        }

        private void videoAUcombo11_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo11, videoSideCombo11, videoStartCombo11, videoEndCombo11, videoStartTxt11, videoEndTxt11, removeBtn11, secLbl11, secLbl11_2 );
        }

        private void removeBtn11_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo11, videoSideCombo11, videoStartCombo11, videoEndCombo11, videoStartTxt11, videoEndTxt11, removeBtn11, secLbl11, secLbl11_2 );
        }

        private void videoAUcombo12_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo12, videoSideCombo12, videoStartCombo12, videoEndCombo12, videoStartTxt12, videoEndTxt12, removeBtn12, secLbl12, secLbl12_2 );
        }

        private void removeBtn12_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo12, videoSideCombo12, videoStartCombo12, videoEndCombo12, videoStartTxt12, videoEndTxt12, removeBtn12, secLbl12, secLbl12_2 );
        }

        private void videoAUcombo13_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo13, videoSideCombo13, videoStartCombo13, videoEndCombo13, videoStartTxt13, videoEndTxt13, removeBtn13, secLbl13, secLbl13_2 );
        }

        private void removeBtn13_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo13, videoSideCombo13, videoStartCombo13, videoEndCombo13, videoStartTxt13, videoEndTxt13, removeBtn13, secLbl13, secLbl13_2 );
        }

        private void videoAUcombo14_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo14, videoSideCombo14, videoStartCombo14, videoEndCombo14, videoStartTxt14, videoEndTxt14, removeBtn14, secLbl14, secLbl14_2 );
        }

        private void removeBtn14_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo14, videoSideCombo14, videoStartCombo14, videoEndCombo14, videoStartTxt14, videoEndTxt14, removeBtn14, secLbl14, secLbl14_2 );
        }

        private void videoAUcombo15_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo15, videoSideCombo15, videoStartCombo15, videoEndCombo15, videoStartTxt15, videoEndTxt15, removeBtn15, secLbl15, secLbl15_2 );
        }

        private void removeBtn15_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo15, videoSideCombo15, videoStartCombo15, videoEndCombo15, videoStartTxt15, videoEndTxt15, removeBtn15, secLbl15, secLbl15_2 );
        }

        private void videoAUcombo16_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo16, videoSideCombo16, videoStartCombo16, videoEndCombo16, videoStartTxt16, videoEndTxt16, removeBtn16, secLbl16, secLbl16_2 );
        }

        private void removeBtn16_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo16, videoSideCombo16, videoStartCombo16, videoEndCombo16, videoStartTxt16, videoEndTxt16, removeBtn16, secLbl16, secLbl26_2 );
        }

        private void videoAUcombo17_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo17, videoSideCombo17, videoStartCombo17, videoEndCombo17, videoStartTxt17, videoEndTxt17, removeBtn17, secLbl17, secLbl17_2 );
        }

        private void removeBtn17_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo17, videoSideCombo17, videoStartCombo17, videoEndCombo17, videoStartTxt17, videoEndTxt17, removeBtn17, secLbl17, secLbl17_2 );
        }

        private void videoAUcombo18_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo18, videoSideCombo18, videoStartCombo18, videoEndCombo18, videoStartTxt18, videoEndTxt18, removeBtn18, secLbl18, secLbl18_2 );
        }

        private void removeBtn18_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo18, videoSideCombo18, videoStartCombo18, videoEndCombo18, videoStartTxt18, videoEndTxt18, removeBtn18, secLbl18, secLbl18_2 );
        }

        private void videoAUcombo19_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo19, videoSideCombo19, videoStartCombo19, videoEndCombo19, videoStartTxt19, videoEndTxt19, removeBtn19, secLbl19, secLbl19_2 );
        }

        private void removeBtn19_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo19, videoSideCombo19, videoStartCombo19, videoEndCombo19, videoStartTxt19, videoEndTxt19, removeBtn19, secLbl19, secLbl19_2 );
        }

        private void videoAUcombo20_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo20, videoSideCombo20, videoStartCombo20, videoEndCombo20, videoStartTxt20, videoEndTxt20, removeBtn20, secLbl20, secLbl20_2 );
        }

        private void removeBtn20_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo20, videoSideCombo20, videoStartCombo20, videoEndCombo20, videoStartTxt20, videoEndTxt20, removeBtn20, secLbl20, secLbl20_2 );
        }

        private void videoAUcombo21_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo21, videoSideCombo21, videoStartCombo21, videoEndCombo21, videoStartTxt21, videoEndTxt21, removeBtn21, secLbl21, secLbl21_2 );
        }

        private void removeBtn21_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo21, videoSideCombo21, videoStartCombo21, videoEndCombo21, videoStartTxt21, videoEndTxt21, removeBtn21, secLbl21, secLbl21_2 );
        }

        private void videoAUcombo22_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo22, videoSideCombo22, videoStartCombo22, videoEndCombo22, videoStartTxt22, videoEndTxt22, removeBtn22, secLbl22, secLbl22_2 );
        }

        private void removeBtn22_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo22, videoSideCombo22, videoStartCombo22, videoEndCombo22, videoStartTxt22, videoEndTxt22, removeBtn22, secLbl22, secLbl22_2 );
        }

        private void videoAUcombo23_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo23, videoSideCombo23, videoStartCombo23, videoEndCombo23, videoStartTxt23, videoEndTxt23, removeBtn23, secLbl23, secLbl23_2 );
        }

        private void removeBtn23_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo23, videoSideCombo23, videoStartCombo23, videoEndCombo23, videoStartTxt23, videoEndTxt23, removeBtn23, secLbl23, secLbl23_2 );
        }

        private void videoAUcombo24_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo24, videoSideCombo24, videoStartCombo24, videoEndCombo24, videoStartTxt24, videoEndTxt24, removeBtn24, secLbl24, secLbl24_2 );
        }

        private void removeBtn24_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo24, videoSideCombo24, videoStartCombo24, videoEndCombo24, videoStartTxt24, videoEndTxt24, removeBtn24, secLbl24, secLbl24_2 );
        }

        private void videoAUcombo25_SelectedIndexChanged(object sender, EventArgs e)
        {
			makeVisible( videoAUcombo25, videoSideCombo25, videoStartCombo25, videoEndCombo25, videoStartTxt25, videoEndTxt25, removeBtn25, secLbl25, secLbl25_2 );
        }

        private void removeBtn25_Click(object sender, EventArgs e)
        {
			removeVideoAU( videoAUcombo25, videoSideCombo25, videoStartCombo25, videoEndCombo25, videoStartTxt25, videoEndTxt25, removeBtn25, secLbl25, secLbl25_2 );
        }

		private void videoAUcombo26_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo26, videoSideCombo26, videoStartCombo26, videoEndCombo26, videoStartTxt26, videoEndTxt26, removeBtn26, secLbl26, secLbl26_2 );
		}

		private void removeBtn26_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo26, videoSideCombo26, videoStartCombo26, videoEndCombo26, videoStartTxt26, videoEndTxt26, removeBtn26, secLbl26, secLbl26_2 );
		}

		private void videoAUcombo27_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo27, videoSideCombo27, videoStartCombo27, videoEndCombo27, videoStartTxt27, videoEndTxt27, removeBtn27, secLbl27, secLbl27_2 );

		}

		private void videoAUcombo28_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo28, videoSideCombo28, videoStartCombo28, videoEndCombo28, videoStartTxt28, videoEndTxt28, removeBtn28, secLbl28, secLbl28_2 );
		}

		private void videoAUcombo29_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo29, videoSideCombo29, videoStartCombo29, videoEndCombo29, videoStartTxt29, videoEndTxt29, removeBtn29, secLbl29, secLbl29_2 );
		}

		private void videoAUcombo30_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo30, videoSideCombo30, videoStartCombo30, videoEndCombo30, videoStartTxt30, videoEndTxt30, removeBtn30, secLbl30, secLbl30_2 );
		}

		private void videoAUcombo31_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo31, videoSideCombo31, videoStartCombo31, videoEndCombo31, videoStartTxt31, videoEndTxt31, removeBtn31, secLbl31, secLbl31_2 );
		}

		private void videoAUcombo32_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo32, videoSideCombo32, videoStartCombo32, videoEndCombo32, videoStartTxt32, videoEndTxt32, removeBtn32, secLbl32, secLbl32_2 );
		}

		private void videoAUcombo33_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo33, videoSideCombo33, videoStartCombo33, videoEndCombo33, videoStartTxt33, videoEndTxt33, removeBtn33, secLbl33, secLbl33_2 );
		}

		private void videoAUcombo34_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo34, videoSideCombo34, videoStartCombo34, videoEndCombo34, videoStartTxt34, videoEndTxt34, removeBtn34, secLbl34, secLbl34_2 );
		}

		private void videoAUcombo35_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo35, videoSideCombo35, videoStartCombo35, videoEndCombo35, videoStartTxt35, videoEndTxt35, removeBtn35, secLbl35, secLbl35_2 );
		}

		private void videoAUcombo36_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo36, videoSideCombo36, videoStartCombo36, videoEndCombo36, videoStartTxt36, videoEndTxt36, removeBtn36, secLbl36, secLbl36_2 );
		}

		private void videoAUcombo37_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo37, videoSideCombo37, videoStartCombo37, videoEndCombo37, videoStartTxt37, videoEndTxt37, removeBtn37, secLbl37, secLbl37_2 );
		}

		private void videoAUcombo38_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo38, videoSideCombo38, videoStartCombo38, videoEndCombo38, videoStartTxt38, videoEndTxt38, removeBtn38, secLbl38, secLbl38_2 );
		}

		private void videoAUcombo39_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo39, videoSideCombo39, videoStartCombo39, videoEndCombo39, videoStartTxt39, videoEndTxt39, removeBtn39, secLbl39, secLbl39_2 );
		}

		private void videoAUcombo40_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo40, videoSideCombo40, videoStartCombo40, videoEndCombo40, videoStartTxt40, videoEndTxt40, removeBtn40, secLbl40, secLbl40_2 );
		}

		private void videoAUcombo41_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo41, videoSideCombo41, videoStartCombo41, videoEndCombo41, videoStartTxt41, videoEndTxt41, removeBtn41, secLbl41, secLbl41_2 );
		}

		private void videoAUcombo42_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo42, videoSideCombo42, videoStartCombo42, videoEndCombo42, videoStartTxt42, videoEndTxt42, removeBtn42, secLbl42, secLbl42_2 );
		}

		private void videoAUcombo43_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo43, videoSideCombo43, videoStartCombo43, videoEndCombo43, videoStartTxt43, videoEndTxt43, removeBtn43, secLbl43, secLbl43_2 );
		}

		private void videoAUcombo44_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo44, videoSideCombo44, videoStartCombo44, videoEndCombo44, videoStartTxt44, videoEndTxt44, removeBtn44, secLbl44, secLbl44_2 );
		}

		private void videoAUcombo45_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo45, videoSideCombo45, videoStartCombo45, videoEndCombo45, videoStartTxt45, videoEndTxt45, removeBtn45, secLbl45, secLbl45_2 );
		}

		private void videoAUcombo46_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo46, videoSideCombo46, videoStartCombo46, videoEndCombo46, videoStartTxt46, videoEndTxt46, removeBtn46, secLbl46, secLbl46_2 );
		}

		private void videoAUcombo47_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo47, videoSideCombo47, videoStartCombo47, videoEndCombo47, videoStartTxt47, videoEndTxt47, removeBtn47, secLbl47, secLbl47_2 );
		}

		private void videoAUcombo48_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo48, videoSideCombo48, videoStartCombo48, videoEndCombo48, videoStartTxt48, videoEndTxt48, removeBtn48, secLbl48, secLbl48_2 );
		}

		private void videoAUcombo49_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo49, videoSideCombo49, videoStartCombo49, videoEndCombo49, videoStartTxt49, videoEndTxt49, removeBtn49, secLbl49, secLbl49_2 );
		}

		private void videoAUcombo50_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo50, videoSideCombo50, videoStartCombo50, videoEndCombo50, videoStartTxt50, videoEndTxt50, removeBtn50, secLbl50, secLbl50_2 );
		}

		private void videoAUcombo51_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo51, videoSideCombo51, videoStartCombo51, videoEndCombo51, videoStartTxt51, videoEndTxt51, removeBtn51, secLbl51, secLbl51_2 );
		}

		private void videoAUcombo52_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo52, videoSideCombo52, videoStartCombo52, videoEndCombo52, videoStartTxt52, videoEndTxt52, removeBtn52, secLbl52, secLbl52_2 );
		}

		private void videoAUcombo53_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo53, videoSideCombo53, videoStartCombo53, videoEndCombo53, videoStartTxt53, videoEndTxt53, removeBtn53, secLbl53, secLbl53_2 );
		}

		private void videoAUcombo54_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo54, videoSideCombo54, videoStartCombo54, videoEndCombo54, videoStartTxt54, videoEndTxt54, removeBtn54, secLbl54, secLbl54_2 );
		}

		private void videoAUcombo55_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo55, videoSideCombo55, videoStartCombo55, videoEndCombo55, videoStartTxt55, videoEndTxt55, removeBtn55, secLbl55, secLbl55_2 );
		}

		private void videoAUcombo56_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo56, videoSideCombo56, videoStartCombo56, videoEndCombo56, videoStartTxt56, videoEndTxt56, removeBtn56, secLbl56, secLbl56_2 );
		}

		private void videoAUcombo57_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo57, videoSideCombo57, videoStartCombo57, videoEndCombo57, videoStartTxt57, videoEndTxt57, removeBtn57, secLbl57, secLbl57_2 );
		}

		private void videoAUcombo58_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo58, videoSideCombo58, videoStartCombo58, videoEndCombo58, videoStartTxt58, videoEndTxt58, removeBtn58, secLbl58, secLbl58_2 );
		}

		private void videoAUcombo59_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo59, videoSideCombo59, videoStartCombo59, videoEndCombo59, videoStartTxt59, videoEndTxt59, removeBtn59, secLbl59, secLbl59_2 );
		}

		private void videoAUcombo60_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo60, videoSideCombo60, videoStartCombo60, videoEndCombo60, videoStartTxt60, videoEndTxt60, removeBtn60, secLbl60, secLbl60_2 );
		}

		private void videoAUcombo61_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo61, videoSideCombo61, videoStartCombo61, videoEndCombo61, videoStartTxt61, videoEndTxt61, removeBtn61, secLbl61, secLbl61_2 );
		}

		private void videoAUcombo62_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo62, videoSideCombo62, videoStartCombo62, videoEndCombo62, videoStartTxt62, videoEndTxt62, removeBtn62, secLbl62, secLbl62_2 );
		}

		private void videoAUcombo63_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo63, videoSideCombo63, videoStartCombo63, videoEndCombo63, videoStartTxt63, videoEndTxt63, removeBtn63, secLbl63, secLbl63_2 );
		}

		private void videoAUcombo64_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo64, videoSideCombo64, videoStartCombo64, videoEndCombo64, videoStartTxt64, videoEndTxt64, removeBtn64, secLbl64, secLbl64_2 );
		}

		private void videoAUcombo65_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo65, videoSideCombo65, videoStartCombo65, videoEndCombo65, videoStartTxt65, videoEndTxt65, removeBtn65, secLbl65, secLbl65_2 );
		}

		private void videoAUcombo66_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo66, videoSideCombo66, videoStartCombo66, videoEndCombo66, videoStartTxt66, videoEndTxt66, removeBtn66, secLbl66, secLbl66_2 );
		}

		private void videoAUcombo67_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo67, videoSideCombo67, videoStartCombo67, videoEndCombo67, videoStartTxt67, videoEndTxt67, removeBtn67, secLbl67, secLbl67_2 );
		}

		private void videoAUcombo68_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo68, videoSideCombo68, videoStartCombo68, videoEndCombo68, videoStartTxt68, videoEndTxt68, removeBtn68, secLbl68, secLbl68_2 );
		}

		private void videoAUcombo69_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo69, videoSideCombo69, videoStartCombo69, videoEndCombo69, videoStartTxt69, videoEndTxt69, removeBtn69, secLbl69, secLbl69_2 );
		}

		private void videoAUcombo70_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo70, videoSideCombo70, videoStartCombo70, videoEndCombo70, videoStartTxt70, videoEndTxt70, removeBtn70, secLbl70, secLbl70_2 );
		}

		private void videoAUcombo71_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo71, videoSideCombo71, videoStartCombo71, videoEndCombo71, videoStartTxt71, videoEndTxt71, removeBtn71, secLbl71, secLbl71_2 );
		}

		private void videoAUcombo72_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo72, videoSideCombo72, videoStartCombo72, videoEndCombo72, videoStartTxt72, videoEndTxt72, removeBtn72, secLbl72, secLbl72_2 );
		}

		private void videoAUcombo73_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo73, videoSideCombo73, videoStartCombo73, videoEndCombo73, videoStartTxt73, videoEndTxt73, removeBtn73, secLbl73, secLbl73_2 );
		}

		private void videoAUcombo74_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo74, videoSideCombo74, videoStartCombo74, videoEndCombo74, videoStartTxt74, videoEndTxt74, removeBtn74, secLbl74, secLbl74_2 );
		}

		private void videoAUcombo75_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo75, videoSideCombo75, videoStartCombo75, videoEndCombo75, videoStartTxt75, videoEndTxt75, removeBtn75, secLbl75, secLbl75_2 );
		}

		private void videoAUcombo76_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo76, videoSideCombo76, videoStartCombo76, videoEndCombo76, videoStartTxt76, videoEndTxt76, removeBtn76, secLbl76, secLbl76_2 );
		}

		private void videoAUcombo77_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo77, videoSideCombo77, videoStartCombo77, videoEndCombo77, videoStartTxt77, videoEndTxt77, removeBtn77, secLbl77, secLbl77_2 );
		}

		private void videoAUcombo78_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo78, videoSideCombo78, videoStartCombo78, videoEndCombo78, videoStartTxt78, videoEndTxt78, removeBtn78, secLbl78, secLbl78_2 );
		}

		private void videoAUcombo79_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo79, videoSideCombo79, videoStartCombo79, videoEndCombo79, videoStartTxt79, videoEndTxt79, removeBtn79, secLbl79, secLbl79_2 );
		}

		private void videoAUcombo80_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo80, videoSideCombo80, videoStartCombo80, videoEndCombo80, videoStartTxt80, videoEndTxt80, removeBtn80, secLbl80, secLbl80_2 );
		}

		private void videoAUcombo81_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo81, videoSideCombo81, videoStartCombo81, videoEndCombo81, videoStartTxt81, videoEndTxt81, removeBtn81, secLbl81, secLbl81_2 );
		}

		private void videoAUcombo82_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo82, videoSideCombo82, videoStartCombo82, videoEndCombo82, videoStartTxt82, videoEndTxt82, removeBtn82, secLbl82, secLbl82_2 );
		}

		private void videoAUcombo83_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo83, videoSideCombo83, videoStartCombo83, videoEndCombo83, videoStartTxt83, videoEndTxt83, removeBtn83, secLbl83, secLbl83_2 );
		}

		private void videoAUcombo84_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo84, videoSideCombo84, videoStartCombo84, videoEndCombo84, videoStartTxt84, videoEndTxt84, removeBtn84, secLbl84, secLbl84_2 );
		}

		private void videoAUcombo85_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo85, videoSideCombo85, videoStartCombo85, videoEndCombo85, videoStartTxt85, videoEndTxt85, removeBtn85, secLbl85, secLbl85_2 );
		}

		private void videoAUcombo86_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo86, videoSideCombo86, videoStartCombo86, videoEndCombo86, videoStartTxt86, videoEndTxt86, removeBtn86, secLbl86, secLbl86_2 );
		}

		private void videoAUcombo87_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo87, videoSideCombo87, videoStartCombo87, videoEndCombo87, videoStartTxt87, videoEndTxt87, removeBtn87, secLbl87, secLbl87_2 );
		}

		private void videoAUcombo88_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo88, videoSideCombo88, videoStartCombo88, videoEndCombo88, videoStartTxt88, videoEndTxt88, removeBtn88, secLbl88, secLbl88_2 );
		}

		private void videoAUcombo89_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo89, videoSideCombo89, videoStartCombo89, videoEndCombo89, videoStartTxt89, videoEndTxt89, removeBtn89, secLbl89, secLbl89_2 );
		}

		private void videoAUcombo90_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo90, videoSideCombo90, videoStartCombo90, videoEndCombo90, videoStartTxt90, videoEndTxt90, removeBtn90, secLbl90, secLbl90_2 );
		}

		private void videoAUcombo91_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo91, videoSideCombo91, videoStartCombo91, videoEndCombo91, videoStartTxt91, videoEndTxt91, removeBtn91, secLbl91, secLbl91_2 );
		}

		private void videoAUcombo92_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo92, videoSideCombo92, videoStartCombo92, videoEndCombo92, videoStartTxt92, videoEndTxt92, removeBtn92, secLbl92, secLbl92_2 );
		}

		private void videoAUcombo93_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo93, videoSideCombo93, videoStartCombo93, videoEndCombo93, videoStartTxt93, videoEndTxt93, removeBtn93, secLbl93, secLbl93_2 );
		}

		private void videoAUcombo94_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo94, videoSideCombo94, videoStartCombo94, videoEndCombo94, videoStartTxt94, videoEndTxt94, removeBtn94, secLbl94, secLbl94_2 );
		}

		private void videoAUcombo95_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo95, videoSideCombo95, videoStartCombo95, videoEndCombo95, videoStartTxt95, videoEndTxt95, removeBtn95, secLbl95, secLbl95_2 );
		}

		private void videoAUcombo96_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo96, videoSideCombo96, videoStartCombo96, videoEndCombo96, videoStartTxt96, videoEndTxt96, removeBtn96, secLbl96, secLbl96_2 );
		}

		private void videoAUcombo97_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo97, videoSideCombo97, videoStartCombo97, videoEndCombo97, videoStartTxt97, videoEndTxt97, removeBtn97, secLbl97, secLbl97_2 );
		}

		private void videoAUcombo98_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo98, videoSideCombo98, videoStartCombo98, videoEndCombo98, videoStartTxt98, videoEndTxt98, removeBtn98, secLbl98, secLbl98_2 );
		}

		private void videoAUcombo99_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo99, videoSideCombo99, videoStartCombo99, videoEndCombo99, videoStartTxt99, videoEndTxt99, removeBtn99, secLbl99, secLbl99_2 );
		}

		private void videoAUcombo100_SelectedIndexChanged( object sender, EventArgs e )
		{
			makeVisible( videoAUcombo100, videoSideCombo100, videoStartCombo100, videoEndCombo100, videoStartTxt100, videoEndTxt100, removeBtn100, secLbl100, secLbl100_2 );
		}

		private void removeBtn27_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo27, videoSideCombo27, videoStartCombo27, videoEndCombo27, videoStartTxt27, videoEndTxt27, removeBtn27, secLbl27, secLbl27_2 );
		}

		private void removeBtn28_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo28, videoSideCombo28, videoStartCombo28, videoEndCombo28, videoStartTxt28, videoEndTxt28, removeBtn28, secLbl28, secLbl28_2 );
		}

		private void removeBtn29_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo29, videoSideCombo29, videoStartCombo29, videoEndCombo29, videoStartTxt29, videoEndTxt29, removeBtn29, secLbl29, secLbl29_2 );
		}

		private void removeBtn30_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo30, videoSideCombo30, videoStartCombo30, videoEndCombo30, videoStartTxt30, videoEndTxt30, removeBtn30, secLbl30, secLbl30_2 );
		}

		private void removeBtn31_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo31, videoSideCombo31, videoStartCombo31, videoEndCombo31, videoStartTxt31, videoEndTxt31, removeBtn31, secLbl31, secLbl31_2 );
		}

		private void removeBtn32_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo32, videoSideCombo32, videoStartCombo32, videoEndCombo32, videoStartTxt32, videoEndTxt32, removeBtn32, secLbl32, secLbl32_2 );
		}

		private void removeBtn33_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo33, videoSideCombo33, videoStartCombo33, videoEndCombo33, videoStartTxt33, videoEndTxt33, removeBtn33, secLbl33, secLbl33_2 );
		}

		private void removeBtn34_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo34, videoSideCombo34, videoStartCombo34, videoEndCombo34, videoStartTxt34, videoEndTxt34, removeBtn34, secLbl34, secLbl34_2 );
		}

		private void removeBtn35_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo35, videoSideCombo35, videoStartCombo35, videoEndCombo35, videoStartTxt35, videoEndTxt35, removeBtn35, secLbl35, secLbl35_2 );
		}

		private void removeBtn36_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo36, videoSideCombo36, videoStartCombo36, videoEndCombo36, videoStartTxt36, videoEndTxt36, removeBtn36, secLbl36, secLbl36_2 );
		}

		private void removeBtn37_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo37, videoSideCombo37, videoStartCombo37, videoEndCombo37, videoStartTxt37, videoEndTxt37, removeBtn37, secLbl37, secLbl37_2 );
		}

		private void removeBtn38_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo38, videoSideCombo38, videoStartCombo38, videoEndCombo38, videoStartTxt38, videoEndTxt38, removeBtn38, secLbl38, secLbl38_2 );
		}

		private void removeBtn39_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo39, videoSideCombo39, videoStartCombo39, videoEndCombo39, videoStartTxt39, videoEndTxt39, removeBtn39, secLbl39, secLbl39_2 );
		}

		private void removeBtn40_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo40, videoSideCombo40, videoStartCombo40, videoEndCombo40, videoStartTxt40, videoEndTxt40, removeBtn40, secLbl40, secLbl40_2 );
		}

		private void removeBtn41_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo41, videoSideCombo41, videoStartCombo41, videoEndCombo41, videoStartTxt41, videoEndTxt41, removeBtn41, secLbl41, secLbl41_2 );
		}

		private void removeBtn42_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo42, videoSideCombo42, videoStartCombo42, videoEndCombo42, videoStartTxt42, videoEndTxt42, removeBtn42, secLbl42, secLbl42_2 );
		}

		private void removeBtn43_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo43, videoSideCombo43, videoStartCombo43, videoEndCombo43, videoStartTxt43, videoEndTxt43, removeBtn43, secLbl43, secLbl43_2 );
		}

		private void removeBtn44_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo44, videoSideCombo44, videoStartCombo44, videoEndCombo44, videoStartTxt44, videoEndTxt44, removeBtn44, secLbl44, secLbl44_2 );
		}

		private void removeBtn45_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo45, videoSideCombo45, videoStartCombo45, videoEndCombo45, videoStartTxt45, videoEndTxt45, removeBtn45, secLbl45, secLbl45_2 );
		}

		private void removeBtn46_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo46, videoSideCombo46, videoStartCombo46, videoEndCombo46, videoStartTxt46, videoEndTxt46, removeBtn46, secLbl46, secLbl46_2 );
		}

		private void removeBtn47_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo47, videoSideCombo47, videoStartCombo47, videoEndCombo47, videoStartTxt47, videoEndTxt47, removeBtn47, secLbl47, secLbl47_2 );
		}

		private void removeBtn48_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo48, videoSideCombo48, videoStartCombo48, videoEndCombo48, videoStartTxt48, videoEndTxt48, removeBtn48, secLbl48, secLbl48_2 );
		}

		private void removeBtn49_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo49, videoSideCombo49, videoStartCombo49, videoEndCombo49, videoStartTxt49, videoEndTxt49, removeBtn49, secLbl49, secLbl49_2 );
		}

		private void removeBtn50_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo50, videoSideCombo50, videoStartCombo50, videoEndCombo50, videoStartTxt50, videoEndTxt50, removeBtn50, secLbl50, secLbl50_2 );
		}

		private void removeBtn51_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo51, videoSideCombo51, videoStartCombo51, videoEndCombo51, videoStartTxt51, videoEndTxt51, removeBtn51, secLbl51, secLbl51_2 );
		}

		private void removeBtn52_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo52, videoSideCombo52, videoStartCombo52, videoEndCombo52, videoStartTxt52, videoEndTxt52, removeBtn52, secLbl52, secLbl52_2 );
		}

		private void removeBtn53_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo53, videoSideCombo53, videoStartCombo53, videoEndCombo53, videoStartTxt53, videoEndTxt53, removeBtn53, secLbl53, secLbl53_2 );
		}

		private void removeBtn54_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo54, videoSideCombo54, videoStartCombo54, videoEndCombo54, videoStartTxt54, videoEndTxt54, removeBtn54, secLbl54, secLbl54_2 );
		}

		private void removeBtn55_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo55, videoSideCombo55, videoStartCombo55, videoEndCombo55, videoStartTxt55, videoEndTxt55, removeBtn55, secLbl55, secLbl55_2 );
		}

		private void removeBtn56_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo56, videoSideCombo56, videoStartCombo56, videoEndCombo56, videoStartTxt56, videoEndTxt56, removeBtn56, secLbl56, secLbl56_2 );
		}

		private void removeBtn57_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo57, videoSideCombo57, videoStartCombo57, videoEndCombo57, videoStartTxt57, videoEndTxt57, removeBtn57, secLbl57, secLbl57_2 );
		}

		private void removeBtn58_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo58, videoSideCombo58, videoStartCombo58, videoEndCombo58, videoStartTxt58, videoEndTxt58, removeBtn58, secLbl58, secLbl58_2 );
		}

		private void removeBtn59_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo59, videoSideCombo59, videoStartCombo59, videoEndCombo59, videoStartTxt59, videoEndTxt59, removeBtn59, secLbl59, secLbl59_2 );
		}

		private void removeBtn60_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo60, videoSideCombo60, videoStartCombo60, videoEndCombo60, videoStartTxt60, videoEndTxt60, removeBtn60, secLbl60, secLbl60_2 );
		}

		private void removeBtn61_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo61, videoSideCombo61, videoStartCombo61, videoEndCombo61, videoStartTxt61, videoEndTxt61, removeBtn61, secLbl61, secLbl61_2 );
		}

		private void removeBtn62_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo62, videoSideCombo62, videoStartCombo62, videoEndCombo62, videoStartTxt62, videoEndTxt62, removeBtn62, secLbl62, secLbl62_2 );
		}

		private void removeBtn63_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo63, videoSideCombo63, videoStartCombo63, videoEndCombo63, videoStartTxt63, videoEndTxt63, removeBtn63, secLbl63, secLbl63_2 );
		}

		private void removeBtn64_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo64, videoSideCombo64, videoStartCombo64, videoEndCombo64, videoStartTxt64, videoEndTxt64, removeBtn64, secLbl64, secLbl64_2 );
		}

		private void removeBtn65_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo65, videoSideCombo65, videoStartCombo65, videoEndCombo65, videoStartTxt65, videoEndTxt65, removeBtn65, secLbl65, secLbl65_2 );
		}

		private void removeBtn66_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo66, videoSideCombo66, videoStartCombo66, videoEndCombo66, videoStartTxt66, videoEndTxt66, removeBtn66, secLbl66, secLbl66_2 );
		}

		private void removeBtn67_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo67, videoSideCombo67, videoStartCombo67, videoEndCombo67, videoStartTxt67, videoEndTxt67, removeBtn67, secLbl67, secLbl67_2 );
		}

		private void removeBtn68_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo68, videoSideCombo68, videoStartCombo68, videoEndCombo68, videoStartTxt68, videoEndTxt68, removeBtn68, secLbl68, secLbl68_2 );
		}

		private void removeBtn69_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo69, videoSideCombo69, videoStartCombo69, videoEndCombo69, videoStartTxt69, videoEndTxt69, removeBtn69, secLbl69, secLbl69_2 );
		}

		private void removeBtn70_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo70, videoSideCombo70, videoStartCombo70, videoEndCombo70, videoStartTxt70, videoEndTxt70, removeBtn70, secLbl70, secLbl70_2 );
		}

		private void removeBtn71_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo71, videoSideCombo71, videoStartCombo71, videoEndCombo71, videoStartTxt71, videoEndTxt71, removeBtn71, secLbl71, secLbl71_2 );
		}

		private void removeBtn72_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo72, videoSideCombo72, videoStartCombo72, videoEndCombo72, videoStartTxt72, videoEndTxt72, removeBtn72, secLbl72, secLbl72_2 );
		}

		private void removeBtn73_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo73, videoSideCombo73, videoStartCombo73, videoEndCombo73, videoStartTxt73, videoEndTxt73, removeBtn73, secLbl73, secLbl73_2 );
		}

		private void removeBtn74_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo74, videoSideCombo74, videoStartCombo74, videoEndCombo74, videoStartTxt74, videoEndTxt74, removeBtn74, secLbl74, secLbl74_2 );
		}

		private void removeBtn75_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo75, videoSideCombo75, videoStartCombo75, videoEndCombo75, videoStartTxt75, videoEndTxt75, removeBtn75, secLbl75, secLbl75_2 );
		}

		private void removeBtn76_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo76, videoSideCombo76, videoStartCombo76, videoEndCombo76, videoStartTxt76, videoEndTxt76, removeBtn76, secLbl76, secLbl76_2 );
		}

		private void removeBtn77_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo77, videoSideCombo77, videoStartCombo77, videoEndCombo77, videoStartTxt77, videoEndTxt77, removeBtn77, secLbl77, secLbl77_2 );
		}

		private void removeBtn78_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo78, videoSideCombo78, videoStartCombo78, videoEndCombo78, videoStartTxt78, videoEndTxt78, removeBtn78, secLbl78, secLbl78_2 );
		}

		private void removeBtn79_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo79, videoSideCombo79, videoStartCombo79, videoEndCombo79, videoStartTxt79, videoEndTxt79, removeBtn79, secLbl79, secLbl79_2 );
		}

		private void removeBtn80_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo80, videoSideCombo80, videoStartCombo80, videoEndCombo80, videoStartTxt80, videoEndTxt80, removeBtn80, secLbl80, secLbl80_2 );
		}

		private void removeBtn81_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo81, videoSideCombo81, videoStartCombo81, videoEndCombo81, videoStartTxt81, videoEndTxt81, removeBtn81, secLbl81, secLbl81_2 );
		}

		private void removeBtn82_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo82, videoSideCombo82, videoStartCombo82, videoEndCombo82, videoStartTxt82, videoEndTxt82, removeBtn82, secLbl82, secLbl82_2 );
		}

		private void removeBtn83_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo83, videoSideCombo83, videoStartCombo83, videoEndCombo83, videoStartTxt83, videoEndTxt83, removeBtn83, secLbl83, secLbl83_2 );
		}

		private void removeBtn84_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo84, videoSideCombo84, videoStartCombo84, videoEndCombo84, videoStartTxt84, videoEndTxt84, removeBtn84, secLbl84, secLbl84_2 );
		}

		private void removeBtn85_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo85, videoSideCombo85, videoStartCombo85, videoEndCombo85, videoStartTxt85, videoEndTxt85, removeBtn85, secLbl85, secLbl85_2 );
		}

		private void removeBtn86_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo86, videoSideCombo86, videoStartCombo86, videoEndCombo86, videoStartTxt86, videoEndTxt86, removeBtn86, secLbl86, secLbl86_2 );
		}

		private void removeBtn87_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo87, videoSideCombo87, videoStartCombo87, videoEndCombo87, videoStartTxt87, videoEndTxt87, removeBtn87, secLbl87, secLbl87_2 );
		}

		private void removeBtn88_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo88, videoSideCombo88, videoStartCombo88, videoEndCombo88, videoStartTxt88, videoEndTxt88, removeBtn88, secLbl88, secLbl88_2 );
		}

		private void removeBtn89_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo89, videoSideCombo89, videoStartCombo89, videoEndCombo89, videoStartTxt89, videoEndTxt89, removeBtn89, secLbl89, secLbl89_2 );
		}

		private void removeBtn90_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo90, videoSideCombo90, videoStartCombo90, videoEndCombo90, videoStartTxt90, videoEndTxt90, removeBtn90, secLbl90, secLbl90_2 );
		}

		private void removeBtn91_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo91, videoSideCombo91, videoStartCombo91, videoEndCombo91, videoStartTxt91, videoEndTxt91, removeBtn91, secLbl91, secLbl91_2 );
		}

		private void removeBtn92_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo92, videoSideCombo92, videoStartCombo92, videoEndCombo92, videoStartTxt92, videoEndTxt92, removeBtn92, secLbl92, secLbl92_2 );
		}

		private void removeBtn93_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo93, videoSideCombo93, videoStartCombo93, videoEndCombo93, videoStartTxt93, videoEndTxt93, removeBtn93, secLbl93, secLbl93_2 );
		}

		private void removeBtn94_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo94, videoSideCombo94, videoStartCombo94, videoEndCombo94, videoStartTxt94, videoEndTxt94, removeBtn94, secLbl94, secLbl94_2 );
		}

		private void removeBtn95_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo95, videoSideCombo95, videoStartCombo95, videoEndCombo95, videoStartTxt95, videoEndTxt95, removeBtn95, secLbl95, secLbl95_2 );
		}

		private void removeBtn96_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo96, videoSideCombo96, videoStartCombo96, videoEndCombo96, videoStartTxt96, videoEndTxt96, removeBtn96, secLbl96, secLbl96_2 );
		}

		private void removeBtn97_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo97, videoSideCombo97, videoStartCombo97, videoEndCombo97, videoStartTxt97, videoEndTxt97, removeBtn97, secLbl97, secLbl97_2 );
		}

		private void removeBtn98_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo98, videoSideCombo98, videoStartCombo98, videoEndCombo98, videoStartTxt98, videoEndTxt98, removeBtn98, secLbl98, secLbl98_2 );
		}

		private void removeBtn99_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo99, videoSideCombo99, videoStartCombo99, videoEndCombo99, videoStartTxt99, videoEndTxt99, removeBtn99, secLbl99, secLbl99_2 );
		}

		private void removeBtn100_Click( object sender, EventArgs e )
		{
			removeVideoAU( videoAUcombo100, videoSideCombo100, videoStartCombo100, videoEndCombo100, videoStartTxt100, videoEndTxt100, removeBtn100, secLbl100, secLbl100_2 );
		}

		#endregion
		
			#region The following methods run when an emotion's intensity is changed

		private void Neutral_CheckedChanged(object sender, EventArgs e)
        {
            axActiveHaptekX1.HyperText = emFACS.Neutral();
        }
        
        private void Happiness_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( HappinessIntensity, HappinessIntensityTB, HappinessIntensityLevelLbl, HappinessIntensityLbl );
			if( !Happiness.Checked )
			{
				HappinessIntensityLevelLbl.Text = "0%";
				HappinessIntensityTB.Value = 0;
				HappinessIntensity.Text = "Intensity";
				axActiveHaptekX1.HyperText = emFACS.Happiness("0", lastHappiness_Intensity);
                lastHappiness_Intensity = "0";
            }
        }

		private void HappinessIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( HappinessIntensity, HappinessIntensityTB, HappinessIntensityLevelLbl );
			axActiveHaptekX1.HyperText = emFACS.Happiness( HappinessIntensity.SelectedItem.ToString( ), lastHappiness_Intensity );
			lastHappiness_Intensity = HappinessIntensity.SelectedItem.ToString( );
		}

        private void Sadness_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( SadnessIntensity, SadnessIntensityTB, SadnessIntensityLevelLbl, SadnessIntensityLbl );
			if( !Sadness.Checked )
			{
				SadnessIntensityLevelLbl.Text = "0%";
				SadnessIntensityTB.Value = 0; 
				SadnessIntensity.Text = "Intensity";
				axActiveHaptekX1.HyperText = emFACS.Sadness("0", lastSadness_Intensity);
                lastSadness_Intensity = "0";
            }
        }

		private void SadnessIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( SadnessIntensity, SadnessIntensityTB, SadnessIntensityLevelLbl );
			axActiveHaptekX1.HyperText = emFACS.Sadness( SadnessIntensity.SelectedItem.ToString( ), lastSadness_Intensity );
			lastSadness_Intensity = SadnessIntensity.SelectedItem.ToString( );
		}

        private void Surprise_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( SurpriseIntensity, SurpriseIntensityTB, SurpriseIntensityLevelLbl, SurpriseIntensityLbl );
			if( !Surprise.Checked )
			{
				SurpriseIntensityLevelLbl.Text = "0%";
				SurpriseIntensityTB.Value = 0; 
				SurpriseIntensity.Text = "Intensity";
				axActiveHaptekX1.HyperText = emFACS.Surprise("0", lastSurprise_Intensity);
                lastSurprise_Intensity = "0";
            }
        }

		private void SurpriseIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( SurpriseIntensity, SurpriseIntensityTB, SurpriseIntensityLevelLbl );
			axActiveHaptekX1.HyperText = emFACS.Surprise( SurpriseIntensityTB.Value.ToString( ), lastSurprise_Intensity );
			lastSurprise_Intensity = SurpriseIntensityTB.Value.ToString( );
		}

        private void Anger_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( AngerIntensity, AngerIntensityTB, AngerIntensityLevelLbl, AngerIntensityLbl );
			if( !Anger.Checked )
			{
				AngerIntensityLevelLbl.Text = "0%";
				AngerIntensityTB.Value = 0; 
				AngerIntensity.Text = "Intensity";
				axActiveHaptekX1.HyperText = emFACS.Anger("0", lastAnger_Intensity);
                lastAnger_Intensity = "0";
            }
        }

		private void AngerIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( AngerIntensity, AngerIntensityTB, AngerIntensityLevelLbl );
			axActiveHaptekX1.HyperText = emFACS.Anger( AngerIntensity.SelectedItem.ToString( ), lastAnger_Intensity );
			lastAnger_Intensity = AngerIntensity.SelectedItem.ToString( );
		}

        private void Fear_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( FearIntensity, FearIntensityTB, FearIntensityLevelLbl, FearIntensityLbl );
			if( !Fear.Checked )
			{
				FearIntensityLevelLbl.Text = "0%";
				FearIntensityTB.Value = 0; 
				FearIntensity.Text = "Intensity";
				axActiveHaptekX1.HyperText = emFACS.Fear("0", lastFear_Intensity);
                lastFear_Intensity = "0";
            }
        }

		private void FearIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( FearIntensity, FearIntensityTB, FearIntensityLevelLbl );
			axActiveHaptekX1.HyperText = emFACS.Fear( FearIntensity.SelectedItem.ToString( ), lastFear_Intensity );
			lastFear_Intensity = FearIntensity.SelectedItem.ToString( );
		}

        private void Disgust_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( DisgustIntensity, DisgustIntensityTB, DisgustIntensityLevelLbl, DisgustIntensityLbl );
			if( !Disgust.Checked )
			{
				DisgustIntensityLevelLbl.Text = "0%";
				DisgustIntensityTB.Value = 0; 
				DisgustIntensity.Text = "Intensity";
				axActiveHaptekX1.HyperText = emFACS.Disgust("0", lastDisgust_Intensity);
                lastDisgust_Intensity = "0";
            }
        }

		private void DisgustIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( DisgustIntensity, DisgustIntensityTB, DisgustIntensityLevelLbl );
			axActiveHaptekX1.HyperText = emFACS.Disgust( DisgustIntensity.SelectedItem.ToString( ), lastDisgust_Intensity );
			lastDisgust_Intensity = DisgustIntensity.SelectedItem.ToString( );
		}

        private void Contempt_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( ContemptIntensity, ContemptIntensityTB, ContemptIntensityLevelLbl, ContemptIntensityLbl );
			if( !Contempt.Checked )
			{
				ContemptIntensityLevelLbl.Text = "0%";
				ContemptIntensityTB.Value = 0;
				ContemptIntensity.Text = "Intensity";
				axActiveHaptekX1.HyperText = emFACS.Contempt("0", lastContempt_Intensity);
                lastContempt_Intensity = "0";
            }
        }

		private void ContemptIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( ContemptIntensity, ContemptIntensityTB, ContemptIntensityLevelLbl );
			axActiveHaptekX1.HyperText = emFACS.Contempt( ContemptIntensity.SelectedItem.ToString( ), lastContempt_Intensity );
			lastContempt_Intensity = ContemptIntensity.SelectedItem.ToString( );
		}

        private void Embarrassment_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( EmbarrassmentIntensity, EmbarrassmentIntensityTB, EmbarrassmentIntensityLevelLbl, EmbarrassmentIntensityLbl );
			if( !Embarrassment.Checked )
			{
				EmbarrassmentIntensityLevelLbl.Text = "0%";
				EmbarrassmentIntensityTB.Value = 0; 
				EmbarrassmentIntensity.Text = "Intensity";
				axActiveHaptekX1.HyperText = emFACS.Embarrassment("0", lastEmbarrassment_Intensity);
                lastEmbarrassment_Intensity = "0";
            }
        }

		private void EmbarrassmentIntensity_SelectedIndexChanged( object sender, EventArgs e )
		{
			setIntensityLabel( EmbarrassmentIntensity, EmbarrassmentIntensityTB, EmbarrassmentIntensityLevelLbl );
			axActiveHaptekX1.HyperText = emFACS.Embarrassment( EmbarrassmentIntensity.SelectedItem.ToString( ), lastEmbarrassment_Intensity );
			lastEmbarrassment_Intensity = EmbarrassmentIntensity.SelectedItem.ToString( );
		}

        private void Pride_CheckedChanged(object sender, EventArgs e)
        {
			switchIntensityControlsVisibility( PrideIntensity, PrideIntensityTB, PrideIntensityLevelLbl, PrideIntensityLbl );
			if( !Pride.Checked )
			{
				PrideIntensityLevelLbl.Text = "0%";
				PrideIntensityTB.Value = 0; 
				PrideIntensity.Text = "Intensity";
				axActiveHaptekX1.HyperText = emFACS.Pride("0", lastPride_Intensity);
                lastPride_Intensity = "0";
            }
        }

        private void PrideIntensity_SelectedIndexChanged(object sender, EventArgs e)
        {
			setIntensityLabel( PrideIntensity, PrideIntensityTB, PrideIntensityLevelLbl );
            axActiveHaptekX1.HyperText = emFACS.Pride(PrideIntensity.SelectedItem.ToString(), lastPride_Intensity);
            lastPride_Intensity = PrideIntensity.SelectedItem.ToString();
        }

		#endregion

			#region The following methods run when track bars are moved

		private void AU1IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU1IntensityLevelLbl.Text = AU1IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU1( AU1IntensityTB.Value.ToString( ), lastAU1_Intensity );
			lastAU1_Intensity = AU1IntensityTB.Value.ToString( );
			setComboBoxText( AU1IntensityTB, AU1Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU2LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU2LeftIntensityLevelLbl.Text = AU2LeftIntensityTB.Value.ToString( ) + "%";
			if( AU2Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU2( AU2LeftIntensityTB.Value.ToString( ), AU2RightIntensityTB.Value.ToString( ), lastAU2_LeftIntensity, lastAU2_RightIntensity );
				lastAU2_RightIntensity = AU2RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU2( AU2LeftIntensityTB.Value.ToString( ), AU2LeftIntensityTB.Value.ToString( ), lastAU2_LeftIntensity, lastAU2_RightIntensity );
				lastAU2_RightIntensity = AU2LeftIntensityTB.Value.ToString( );
			}
			lastAU2_LeftIntensity = AU2LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU2LeftIntensityTB, AU2LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU2RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU2RightIntensityLevelLbl.Text = AU2RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU2( AU2LeftIntensityTB.Value.ToString( ), AU2RightIntensityTB.Value.ToString( ), lastAU2_LeftIntensity, lastAU2_RightIntensity );
			lastAU2_LeftIntensity = AU2LeftIntensityTB.Value.ToString( );
			lastAU2_RightIntensity = AU2RightIntensityTB.Value.ToString( );
			setComboBoxText( AU2RightIntensityTB, AU2RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU4IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU4IntensityLevelLbl.Text = AU4IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU4( AU4IntensityTB.Value.ToString( ), lastAU4_Intensity );
			lastAU4_Intensity = AU4IntensityTB.Value.ToString( );
			setComboBoxText( AU4IntensityTB, AU4Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU5IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU5IntensityLevelLbl.Text = AU5IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU5( AU5IntensityTB.Value.ToString( ), lastAU5_Intensity );
			lastAU5_Intensity = AU5IntensityTB.Value.ToString( );
			setComboBoxText( AU5IntensityTB, AU5Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU6IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU6IntensityLevelLbl.Text = AU6IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU6( AU6IntensityTB.Value.ToString( ), lastAU6_Intensity );
			lastAU6_Intensity = AU6IntensityTB.Value.ToString( );
			setComboBoxText( AU6IntensityTB, AU6Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU7IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU7IntensityLevelLbl.Text = AU7IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU7( AU7IntensityTB.Value.ToString( ), lastAU7_Intensity );
			lastAU7_Intensity = AU7IntensityTB.Value.ToString( );
			setComboBoxText( AU7IntensityTB, AU7Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU8IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU8IntensityLevelLbl.Text = AU8IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU8( AU8IntensityTB.Value.ToString( ), lastAU8_Intensity );
			lastAU8_Intensity = AU8IntensityTB.Value.ToString( );
			setComboBoxText( AU8IntensityTB, AU8Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU9IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU9IntensityLevelLbl.Text = AU9IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU9( AU9IntensityTB.Value.ToString( ), lastAU9_Intensity );
			lastAU9_Intensity = AU9IntensityTB.Value.ToString( );
			setComboBoxText( AU9IntensityTB, AU9Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU10IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU10IntensityLevelLbl.Text = AU10IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU10( AU10IntensityTB.Value.ToString( ), lastAU10_Intensity );
			lastAU10_Intensity = AU10IntensityTB.Value.ToString( );
			setComboBoxText( AU10IntensityTB, AU10Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU11IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU11IntensityLevelLbl.Text = AU11IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU11( AU11IntensityTB.Value.ToString( ), lastAU11_Intensity );
			lastAU11_Intensity = AU11IntensityTB.Value.ToString( );
			setComboBoxText( AU11IntensityTB, AU11Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU12IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU12IntensityLevelLbl.Text = AU12IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU12( AU12IntensityTB.Value.ToString( ), lastAU12_Intensity );
			lastAU12_Intensity = AU12IntensityTB.Value.ToString( );
			setComboBoxText( AU12IntensityTB, AU12Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU13LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU13LeftIntensityLevelLbl.Text = AU13LeftIntensityTB.Value.ToString( ) + "%";
			if( AU13Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU13( AU13LeftIntensityTB.Value.ToString( ), AU13RightIntensityTB.Value.ToString( ), lastAU13_LeftIntensity, lastAU13_RightIntensity );
				lastAU13_RightIntensity = AU13RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU13( AU13LeftIntensityTB.Value.ToString( ), AU13LeftIntensityTB.Value.ToString( ), lastAU13_LeftIntensity, lastAU13_RightIntensity );
				lastAU13_RightIntensity = AU13LeftIntensityTB.Value.ToString( );
			}
			lastAU13_LeftIntensity = AU13LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU13LeftIntensityTB, AU13LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU13RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU13RightIntensityLevelLbl.Text = AU13RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU13( AU13LeftIntensityTB.Value.ToString( ), AU13RightIntensityTB.Value.ToString( ), lastAU13_LeftIntensity, lastAU13_RightIntensity );
			lastAU13_LeftIntensity = AU13LeftIntensityTB.Value.ToString( );
			lastAU13_RightIntensity = AU13RightIntensityTB.Value.ToString( );
			setComboBoxText( AU13RightIntensityTB, AU13RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU14LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU14LeftIntensityLevelLbl.Text = AU14LeftIntensityTB.Value.ToString( ) + "%";
			if( AU14Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU14( AU14LeftIntensityTB.Value.ToString( ), AU14RightIntensityTB.Value.ToString( ), lastAU14_LeftIntensity, lastAU14_RightIntensity );
				lastAU14_RightIntensity = AU14RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU14( AU14LeftIntensityTB.Value.ToString( ), AU14LeftIntensityTB.Value.ToString( ), lastAU14_LeftIntensity, lastAU14_RightIntensity );
				lastAU14_RightIntensity = AU14LeftIntensityTB.Value.ToString( );
			}
			lastAU14_LeftIntensity = AU14LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU14LeftIntensityTB, AU14LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU14RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU14RightIntensityLevelLbl.Text = AU14RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU14( AU14LeftIntensityTB.Value.ToString( ), AU14RightIntensityTB.Value.ToString( ), lastAU14_LeftIntensity, lastAU14_RightIntensity );
			lastAU14_LeftIntensity = AU14LeftIntensityTB.Value.ToString( );
			lastAU14_RightIntensity = AU14RightIntensityTB.Value.ToString( );
			setComboBoxText( AU14RightIntensityTB, AU14RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU15LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU15LeftIntensityLevelLbl.Text = AU15LeftIntensityTB.Value.ToString( ) + "%";
			if( AU15Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU15( AU15LeftIntensityTB.Value.ToString( ), AU15RightIntensityTB.Value.ToString( ), lastAU15_LeftIntensity, lastAU15_RightIntensity );
				lastAU15_RightIntensity = AU15RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU15( AU15LeftIntensityTB.Value.ToString( ), AU15LeftIntensityTB.Value.ToString( ), lastAU15_LeftIntensity, lastAU15_RightIntensity );
				lastAU15_RightIntensity = AU15LeftIntensityTB.Value.ToString( );
			}
			lastAU15_LeftIntensity = AU15LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU15LeftIntensityTB, AU15LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU15RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU15RightIntensityLevelLbl.Text = AU15RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU15( AU15LeftIntensityTB.Value.ToString( ), AU15RightIntensityTB.Value.ToString( ), lastAU15_LeftIntensity, lastAU15_RightIntensity );
			lastAU15_LeftIntensity = AU15LeftIntensityTB.Value.ToString( );
			lastAU15_RightIntensity = AU15RightIntensityTB.Value.ToString( );
			setComboBoxText( AU15RightIntensityTB, AU15RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU16IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU16IntensityLevelLbl.Text = AU16IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU16( AU16IntensityTB.Value.ToString( ), lastAU16_Intensity );
			lastAU16_Intensity = AU16IntensityTB.Value.ToString( );
			setComboBoxText( AU16IntensityTB, AU16Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU17IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU17IntensityLevelLbl.Text = AU17IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU17( AU17IntensityTB.Value.ToString( ), lastAU17_Intensity );
			lastAU17_Intensity = AU17IntensityTB.Value.ToString( );
			setComboBoxText( AU17IntensityTB, AU17Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU18IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU18IntensityLevelLbl.Text = AU18IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU18( AU18IntensityTB.Value.ToString( ), lastAU18_Intensity );
			lastAU18_Intensity = AU18IntensityTB.Value.ToString( );
			setComboBoxText( AU18IntensityTB, AU18Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU20IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU20IntensityLevelLbl.Text = AU20IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU20( AU20IntensityTB.Value.ToString( ), lastAU20_Intensity );
			lastAU20_Intensity = AU20IntensityTB.Value.ToString( );
			setComboBoxText( AU20IntensityTB, AU20Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU22IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU22IntensityLevelLbl.Text = AU22IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU22( AU22IntensityTB.Value.ToString( ), lastAU22_Intensity );
			lastAU22_Intensity = AU22IntensityTB.Value.ToString( );
			setComboBoxText( AU22IntensityTB, AU22Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU23IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU23IntensityLevelLbl.Text = AU23IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU23( AU23IntensityTB.Value.ToString( ), lastAU23_Intensity );
			lastAU23_Intensity = AU23IntensityTB.Value.ToString( );
			setComboBoxText( AU23IntensityTB, AU23Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU24IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU24IntensityLevelLbl.Text = AU24IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU24( AU24IntensityTB.Value.ToString( ), lastAU24_Intensity );
			lastAU24_Intensity = AU24IntensityTB.Value.ToString( );
			setComboBoxText( AU24IntensityTB, AU24Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU25IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU25IntensityLevelLbl.Text = AU25IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU25( AU25IntensityTB.Value.ToString( ), lastAU25_Intensity );
			lastAU25_Intensity = AU25IntensityTB.Value.ToString( );
			setComboBoxText( AU25IntensityTB, AU25Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU26IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU26IntensityLevelLbl.Text = AU26IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU26( AU26IntensityTB.Value.ToString( ), lastAU26_Intensity );
			lastAU26_Intensity = AU26IntensityTB.Value.ToString( );
			setComboBoxText( AU26IntensityTB, AU26Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU27IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU27IntensityLevelLbl.Text = AU27IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU27( AU27IntensityTB.Value.ToString( ), lastAU27_Intensity );
			lastAU27_Intensity = AU27IntensityTB.Value.ToString( );
			setComboBoxText( AU27IntensityTB, AU27Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU28IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU28IntensityLevelLbl.Text = AU28IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU28( AU28IntensityTB.Value.ToString( ), lastAU28_Intensity );
			lastAU28_Intensity = AU28IntensityTB.Value.ToString( );
			setComboBoxText( AU28IntensityTB, AU28Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU38LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU38LeftIntensityLevelLbl.Text = AU38LeftIntensityTB.Value.ToString( ) + "%";
			if( AU38Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU38( AU38LeftIntensityTB.Value.ToString( ), AU38RightIntensityTB.Value.ToString( ), lastAU38_LeftIntensity, lastAU38_RightIntensity );
				lastAU38_RightIntensity = AU38RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU38( AU38LeftIntensityTB.Value.ToString( ), AU38LeftIntensityTB.Value.ToString( ), lastAU38_LeftIntensity, lastAU38_RightIntensity );
				lastAU38_RightIntensity = AU38LeftIntensityTB.Value.ToString( );
			}
			lastAU38_LeftIntensity = AU38LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU38LeftIntensityTB, AU38LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU38RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU38RightIntensityLevelLbl.Text = AU38RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU38( AU38LeftIntensityTB.Value.ToString( ), AU38RightIntensityTB.Value.ToString( ), lastAU38_LeftIntensity, lastAU38_RightIntensity );
			lastAU38_LeftIntensity = AU38LeftIntensityTB.Value.ToString( );
			lastAU38_RightIntensity = AU38RightIntensityTB.Value.ToString( );
			setComboBoxText( AU38RightIntensityTB, AU38RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU39LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU39LeftIntensityLevelLbl.Text = AU39LeftIntensityTB.Value.ToString( ) + "%";
			if( AU39Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU39( AU39LeftIntensityTB.Value.ToString( ), AU39RightIntensityTB.Value.ToString( ), lastAU39_LeftIntensity, lastAU39_RightIntensity );
				lastAU39_RightIntensity = AU39RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU39( AU39LeftIntensityTB.Value.ToString( ), AU39LeftIntensityTB.Value.ToString( ), lastAU39_LeftIntensity, lastAU39_RightIntensity );
				lastAU39_RightIntensity = AU39LeftIntensityTB.Value.ToString( );
			}
			lastAU39_LeftIntensity = AU39LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU39LeftIntensityTB, AU39LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU39RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU39RightIntensityLevelLbl.Text = AU39RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU39( AU39LeftIntensityTB.Value.ToString( ), AU39RightIntensityTB.Value.ToString( ), lastAU39_LeftIntensity, lastAU39_RightIntensity );
			lastAU39_LeftIntensity = AU39LeftIntensityTB.Value.ToString( );
			lastAU39_RightIntensity = AU39RightIntensityTB.Value.ToString( );
			setComboBoxText( AU39RightIntensityTB, AU39RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU41IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU41IntensityLevelLbl.Text = AU41IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU41( AU41IntensityTB.Value.ToString( ), lastAU41_Intensity );
			lastAU41_Intensity = AU41IntensityTB.Value.ToString( );
			setComboBoxText( AU41IntensityTB, AU41Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU42IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU42IntensityLevelLbl.Text = AU42IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU42( AU42IntensityTB.Value.ToString( ), lastAU42_Intensity );
			lastAU42_Intensity = AU42IntensityTB.Value.ToString( );
			setComboBoxText( AU42IntensityTB, AU42Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU43IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU43IntensityLevelLbl.Text = AU43IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU43( AU43IntensityTB.Value.ToString( ), lastAU43_Intensity );
			lastAU43_Intensity = AU43IntensityTB.Value.ToString( );
			setComboBoxText( AU43IntensityTB, AU43Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU44IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU44IntensityLevelLbl.Text = AU44IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU44( AU44IntensityTB.Value.ToString( ), lastAU44_Intensity );
			lastAU44_Intensity = AU44IntensityTB.Value.ToString( );
			setComboBoxText( AU44IntensityTB, AU44Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU51IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU51IntensityLevelLbl.Text = AU51IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU51( AU51IntensityTB.Value.ToString( ), lastAU51_Intensity );
			lastAU51_Intensity = AU51IntensityTB.Value.ToString( );
			setComboBoxText( AU51IntensityTB, AU51Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU52IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU52IntensityLevelLbl.Text = AU52IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU52( AU52IntensityTB.Value.ToString( ), lastAU52_Intensity );
			lastAU52_Intensity = AU52IntensityTB.Value.ToString( );
			setComboBoxText( AU52IntensityTB, AU52Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU53IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU53IntensityLevelLbl.Text = AU53IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU53( AU53IntensityTB.Value.ToString( ), lastAU53_Intensity );
			lastAU53_Intensity = AU53IntensityTB.Value.ToString( );
			setComboBoxText( AU53IntensityTB, AU53Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU54IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU54IntensityLevelLbl.Text = AU54IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU54( AU54IntensityTB.Value.ToString( ), lastAU54_Intensity );
			lastAU54_Intensity = AU54IntensityTB.Value.ToString( );
			setComboBoxText( AU54IntensityTB, AU54Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU55IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU55IntensityLevelLbl.Text = AU55IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU55( AU55IntensityTB.Value.ToString( ), lastAU55_Intensity );
			lastAU55_Intensity = AU55IntensityTB.Value.ToString( );
			setComboBoxText( AU55IntensityTB, AU55Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU56IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU56IntensityLevelLbl.Text = AU56IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU56( AU56IntensityTB.Value.ToString( ), lastAU56_Intensity );
			lastAU56_Intensity = AU56IntensityTB.Value.ToString( );
			setComboBoxText( AU56IntensityTB, AU56Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU57IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU57IntensityLevelLbl.Text = AU57IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU57( AU57IntensityTB.Value.ToString( ), lastAU57_Intensity );
			lastAU57_Intensity = AU57IntensityTB.Value.ToString( );
			setComboBoxText( AU57IntensityTB, AU57Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU58IntensityTB_Scroll( object sender, EventArgs e )
		{
			AU58IntensityLevelLbl.Text = AU58IntensityTB.Value.ToString( ) + "%";
			hapFACS.AU58( AU58IntensityTB.Value.ToString( ), lastAU58_Intensity );
			lastAU58_Intensity = AU58IntensityTB.Value.ToString( );
			setComboBoxText( AU58IntensityTB, AU58Intensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU61LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU61LeftIntensityLevelLbl.Text = AU61LeftIntensityTB.Value.ToString( ) + "%";
			if( AU61Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU61( AU61LeftIntensityTB.Value.ToString( ), AU61RightIntensityTB.Value.ToString( ), lastAU61_LeftIntensity, lastAU61_RightIntensity );
				lastAU61_RightIntensity = AU61RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU61( AU61LeftIntensityTB.Value.ToString( ), AU61LeftIntensityTB.Value.ToString( ), lastAU61_LeftIntensity, lastAU61_RightIntensity );
				lastAU61_RightIntensity = AU61LeftIntensityTB.Value.ToString( );
			}
			lastAU61_LeftIntensity = AU61LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU61LeftIntensityTB, AU61LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU61RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU61RightIntensityLevelLbl.Text = AU61RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU61( AU61LeftIntensityTB.Value.ToString( ), AU61RightIntensityTB.Value.ToString( ), lastAU61_LeftIntensity, lastAU61_RightIntensity );
			lastAU61_LeftIntensity = AU61LeftIntensityTB.Value.ToString( );
			lastAU61_RightIntensity = AU61RightIntensityTB.Value.ToString( );
			setComboBoxText( AU61RightIntensityTB, AU61RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU62LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU62LeftIntensityLevelLbl.Text = AU62LeftIntensityTB.Value.ToString( ) + "%";
			if( AU62Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU62( AU62LeftIntensityTB.Value.ToString( ), AU62RightIntensityTB.Value.ToString( ), lastAU62_LeftIntensity, lastAU62_RightIntensity );
				lastAU62_RightIntensity = AU62RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU62( AU62LeftIntensityTB.Value.ToString( ), AU62LeftIntensityTB.Value.ToString( ), lastAU62_LeftIntensity, lastAU62_RightIntensity );
				lastAU62_RightIntensity = AU62LeftIntensityTB.Value.ToString( );
			}
			lastAU62_LeftIntensity = AU62LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU62LeftIntensityTB, AU62LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU62RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU62RightIntensityLevelLbl.Text = AU62RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU62( AU62LeftIntensityTB.Value.ToString( ), AU62RightIntensityTB.Value.ToString( ), lastAU62_LeftIntensity, lastAU62_RightIntensity );
			lastAU62_LeftIntensity = AU62LeftIntensityTB.Value.ToString( );
			lastAU62_RightIntensity = AU62RightIntensityTB.Value.ToString( );
			setComboBoxText( AU62RightIntensityTB, AU62RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU63LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU63LeftIntensityLevelLbl.Text = AU63LeftIntensityTB.Value.ToString( ) + "%";
			if( AU63Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU63( AU63LeftIntensityTB.Value.ToString( ), AU63RightIntensityTB.Value.ToString( ), lastAU63_LeftIntensity, lastAU63_RightIntensity );
				lastAU63_RightIntensity = AU63RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU63( AU63LeftIntensityTB.Value.ToString( ), AU63LeftIntensityTB.Value.ToString( ), lastAU63_LeftIntensity, lastAU63_RightIntensity );
				lastAU63_RightIntensity = AU63LeftIntensityTB.Value.ToString( );
			}
			lastAU63_LeftIntensity = AU63LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU63LeftIntensityTB, AU63LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU63RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU63RightIntensityLevelLbl.Text = AU63RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU63( AU63LeftIntensityTB.Value.ToString( ), AU63RightIntensityTB.Value.ToString( ), lastAU63_LeftIntensity, lastAU63_RightIntensity );
			lastAU63_LeftIntensity = AU63LeftIntensityTB.Value.ToString( );
			lastAU63_RightIntensity = AU63RightIntensityTB.Value.ToString( );
			setComboBoxText( AU63RightIntensityTB, AU63RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU64LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU64LeftIntensityLevelLbl.Text = AU64LeftIntensityTB.Value.ToString( ) + "%";
			if( AU64Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU64( AU64LeftIntensityTB.Value.ToString( ), AU64RightIntensityTB.Value.ToString( ), lastAU64_LeftIntensity, lastAU64_RightIntensity );
				lastAU64_RightIntensity = AU64RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU64( AU64LeftIntensityTB.Value.ToString( ), AU64LeftIntensityTB.Value.ToString( ), lastAU64_LeftIntensity, lastAU64_RightIntensity );
				lastAU64_RightIntensity = AU64LeftIntensityTB.Value.ToString( );
			}
			lastAU64_LeftIntensity = AU64LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU64LeftIntensityTB, AU64LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU64RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU64RightIntensityLevelLbl.Text = AU64RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU64( AU64LeftIntensityTB.Value.ToString( ), AU64RightIntensityTB.Value.ToString( ), lastAU64_LeftIntensity, lastAU64_RightIntensity );
			lastAU64_LeftIntensity = AU64LeftIntensityTB.Value.ToString( );
			lastAU64_RightIntensity = AU64RightIntensityTB.Value.ToString( );
			setComboBoxText( AU64RightIntensityTB, AU64RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU65LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU65LeftIntensityLevelLbl.Text = AU65LeftIntensityTB.Value.ToString( ) + "%";
			if( AU65Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU65( AU65LeftIntensityTB.Value.ToString( ), AU65RightIntensityTB.Value.ToString( ), lastAU65_LeftIntensity, lastAU65_RightIntensity );
				lastAU65_RightIntensity = AU65RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU65( AU65LeftIntensityTB.Value.ToString( ), AU65LeftIntensityTB.Value.ToString( ), lastAU65_LeftIntensity, lastAU65_RightIntensity );
				lastAU65_RightIntensity = AU65LeftIntensityTB.Value.ToString( );
			}
			lastAU65_LeftIntensity = AU65LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU65LeftIntensityTB, AU65LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU65RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			HeadEyeTab.Text = AU65RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU65( AU65LeftIntensityTB.Value.ToString( ), AU65RightIntensityTB.Value.ToString( ), lastAU65_LeftIntensity, lastAU65_RightIntensity );
			lastAU65_LeftIntensity = AU65LeftIntensityTB.Value.ToString( );
			lastAU65_RightIntensity = AU65RightIntensityTB.Value.ToString( );
			setComboBoxText( AU65RightIntensityTB, AU65RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU66LeftIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU66LeftIntensityLevelLbl.Text = AU66LeftIntensityTB.Value.ToString( ) + "%";
			if( AU66Side.SelectedItem.ToString( ) == "Unilateral" )
			{
				hapFACS.AU66( AU66LeftIntensityTB.Value.ToString( ), AU66RightIntensityTB.Value.ToString( ), lastAU66_LeftIntensity, lastAU66_RightIntensity );
				lastAU66_RightIntensity = AU66RightIntensityTB.Value.ToString( );
			}
			else
			{
				hapFACS.AU66( AU66LeftIntensityTB.Value.ToString( ), AU66LeftIntensityTB.Value.ToString( ), lastAU66_LeftIntensity, lastAU66_RightIntensity );
				lastAU66_RightIntensity = AU66LeftIntensityTB.Value.ToString( );
			}
			lastAU66_LeftIntensity = AU66LeftIntensityTB.Value.ToString( );
			setComboBoxText( AU66LeftIntensityTB, AU66LeftIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void AU66RightIntensityTB_Scroll( object sender, EventArgs e )
		{
			AU66RightIntensityLevelLbl.Text = AU66RightIntensityTB.Value.ToString( ) + "%";
			hapFACS.AU66( AU66LeftIntensityTB.Value.ToString( ), AU66RightIntensityTB.Value.ToString( ), lastAU66_LeftIntensity, lastAU66_RightIntensity );
			lastAU66_LeftIntensity = AU66LeftIntensityTB.Value.ToString( );
			lastAU66_RightIntensity = AU66RightIntensityTB.Value.ToString( );
			setComboBoxText( AU66RightIntensityTB, AU66RightIntensity );
			axActiveHaptekX1.HyperText = hapFACS.generateFace( );
		}

		private void HappinessIntensityTB_Scroll( object sender, EventArgs e )
		{
			HappinessIntensityLevelLbl.Text = HappinessIntensityTB.Value.ToString( ) + "%";
			setComboBoxText( HappinessIntensityTB, HappinessIntensity );
			axActiveHaptekX1.HyperText = emFACS.Happiness( HappinessIntensityTB.Value.ToString( ), lastHappiness_Intensity );
			lastHappiness_Intensity = HappinessIntensityTB.Value.ToString( );
		}

		private void SadnessIntensityTB_Scroll( object sender, EventArgs e )
		{
			SadnessIntensityLevelLbl.Text = SadnessIntensityTB.Value.ToString( ) + "%";
			setComboBoxText( SadnessIntensityTB, SadnessIntensity );
			axActiveHaptekX1.HyperText = emFACS.Sadness( SadnessIntensityTB.Value.ToString( ), lastSadness_Intensity );
			lastSadness_Intensity = SadnessIntensityTB.Value.ToString( );
		}

		private void SurpriseIntensityTB_Scroll( object sender, EventArgs e )
		{
			SurpriseIntensityLevelLbl.Text = SurpriseIntensityTB.Value.ToString( ) + "%";
			setComboBoxText( SurpriseIntensityTB, SurpriseIntensity );
			axActiveHaptekX1.HyperText = emFACS.Surprise( SurpriseIntensityTB.Value.ToString( ), lastSurprise_Intensity );
			lastSurprise_Intensity = SurpriseIntensityTB.Value.ToString( );
		}

		private void AngerIntensityTB_Scroll( object sender, EventArgs e )
		{
			AngerIntensityLevelLbl.Text = AngerIntensityTB.Value.ToString( ) + "%";
			setComboBoxText( AngerIntensityTB, AngerIntensity );
			axActiveHaptekX1.HyperText = emFACS.Anger( AngerIntensityTB.Value.ToString( ), lastAnger_Intensity );
			lastAnger_Intensity = AngerIntensityTB.Value.ToString( );
		}

		private void FearIntensityTB_Scroll( object sender, EventArgs e )
		{
			FearIntensityLevelLbl.Text = FearIntensityTB.Value.ToString( ) + "%";
			setComboBoxText( FearIntensityTB, FearIntensity );
			axActiveHaptekX1.HyperText = emFACS.Fear( FearIntensityTB.Value.ToString( ), lastFear_Intensity );
			lastFear_Intensity = FearIntensityTB.Value.ToString( );
		}

		private void DisgustIntensityTB_Scroll( object sender, EventArgs e )
		{
			DisgustIntensityLevelLbl.Text = DisgustIntensityTB.Value.ToString( ) + "%";
			setComboBoxText( DisgustIntensityTB, DisgustIntensity );
			axActiveHaptekX1.HyperText = emFACS.Disgust( DisgustIntensityTB.Value.ToString( ), lastDisgust_Intensity );
			lastDisgust_Intensity = DisgustIntensityTB.Value.ToString( );
		}

		private void ContemptIntensityTB_Scroll( object sender, EventArgs e )
		{
			ContemptIntensityLevelLbl.Text = ContemptIntensityTB.Value.ToString( ) + "%";
			setComboBoxText( ContemptIntensityTB, ContemptIntensity );
			axActiveHaptekX1.HyperText = emFACS.Contempt( ContemptIntensityTB.Value.ToString( ), lastContempt_Intensity );
			lastContempt_Intensity = ContemptIntensityTB.Value.ToString( );
		}

		private void EmbarrassmentIntensityTB_Scroll( object sender, EventArgs e )
		{
			EmbarrassmentIntensityLevelLbl.Text = EmbarrassmentIntensityTB.Value.ToString( ) + "%";
			setComboBoxText( EmbarrassmentIntensityTB, EmbarrassmentIntensity );
			axActiveHaptekX1.HyperText = emFACS.Embarrassment( EmbarrassmentIntensityTB.Value.ToString( ), lastEmbarrassment_Intensity );
			lastEmbarrassment_Intensity = EmbarrassmentIntensityTB.Value.ToString( );
		}

		private void PrideIntensityTB_Scroll( object sender, EventArgs e )
		{
			PrideIntensityLevelLbl.Text = PrideIntensityTB.Value.ToString( ) + "%";
			setComboBoxText( PrideIntensityTB, PrideIntensity );
			axActiveHaptekX1.HyperText = emFACS.Pride( PrideIntensityTB.Value.ToString( ), lastPride_Intensity );
			lastPride_Intensity = PrideIntensityTB.Value.ToString( );
		}

		#endregion
		
			#region Other methods
		// The character reads out the text in the input textBox
		private void Speak_Click( object sender, EventArgs e )
		{
			SpeakTheText( TextToSpeek.Text );
		}

		// Starts recording the video while the character is speaking
		private void RecordWithVoice_Click( object sender, EventArgs e )
		{
			StartRecording( );
			SpeakTheText( TextToSpeek.Text );
		}

		// Stops the video recording while the character is speaking
		private void StopRec_Click( object sender, EventArgs e )
		{
			StopRecordingImmediately( );
		}

		// Runs when the About menu is clicked
		private void AboutMenu_Click( object sender, EventArgs e )
		{
			AboutBox about = new AboutBox( );
			about.Show( );
		}

		// When the video time text boxes are clicked, their text is selected (to ease typing in the text box)
		private void videoTimes_Clicked( object sender, EventArgs e )
		{
			( ( TextBox )sender ).SelectAll( );
		}

		// Sets the text in the combo boxes when a track bar is changed
		private void setComboBoxText( TrackBar trackBar, ComboBox comboBox )
		{
			// We use 0.5 as an epsilon, because the double numbers may not be exatcly the same
			if( trackBar.Value < 0.5 )
				comboBox.Text = "0";
			else if( Math.Abs( trackBar.Value - 100 / Constants.AIntensity ) < 0.5 )
				comboBox.Text = "A";
			else if( Math.Abs( trackBar.Value - 100 / Constants.BIntensity ) < 0.5 )
				comboBox.Text = "B";
			else if( Math.Abs( trackBar.Value - 100 / Constants.CIntensity ) < 0.5 )
				comboBox.Text = "C";
			else if( Math.Abs( trackBar.Value - 100 / Constants.DIntensity ) < 0.5 )
				comboBox.Text = "D";
			else if( Math.Abs( trackBar.Value - 100 ) < 0.5 )
				comboBox.Text = "E";
			else
				comboBox.Text = "Intensity";
		}
		
		// Runs when background image button is clicked
		private void BackGroundImageBtn_Click( object sender, EventArgs e )
		{
			// Create an instance of the open file dialog box.
			OpenFileDialog openFileDialog = new OpenFileDialog( );

			// Set filter options and filter index.
			openFileDialog.Filter = "JPG Image (.jpg)|*.jpg";
			openFileDialog.InitialDirectory = path + @"\Images"; 
			openFileDialog.Title = "Select an image file"; 
			openFileDialog.FilterIndex = 1;

			openFileDialog.Multiselect = false;

			if( openFileDialog.ShowDialog( ) == DialogResult.OK )
			{
				BackGroundImageTB.Text = openFileDialog.FileName;
				axActiveHaptekX1.HyperText = @"\LoadBackGrnd[ file= [" + BackGroundImageTB.Text + "]]"; 
			}
		}

		// Runs when the background image radio button is clicked
		private void BackGroundImageRB_CheckedChanged( object sender, EventArgs e )
		{
			if( BackGroundImageRB.Checked )
			{
				BackGroundImageTB.Enabled = true;
				BackGroundImageBtn.Enabled = true;
			}
			else
			{
				BackGroundImageTB.Enabled = false;
				BackGroundImageBtn.Enabled = false;
			}
		}

		// Runs when custom color button is clicked
		private void CustomColorBtn_Click( object sender, EventArgs e )
		{
			ColorDialog MyDialog = new ColorDialog( );
			// Lets the user from selecting a custom color.
			MyDialog.AllowFullOpen = true;
			// Sets the initial color select to the current text color.
			MyDialog.Color = Color.White; // Load the default white background

			// Update the text box color if the user clicks OK  
			if( MyDialog.ShowDialog( ) == DialogResult.OK )
			{
				axActiveHaptekX1.HyperText = @"\LoadBackGrnd[ r= " + MyDialog.Color.R/255 + " g= " + MyDialog.Color.G/255 + " b= " + MyDialog.Color.B/255 + "]";
				RedTB.Text = (MyDialog.Color.R / 255.0).ToString( );
				GreenTB.Text = (MyDialog.Color.G / 255.0).ToString( );
				BlueTB.Text = (MyDialog.Color.B / 255.0).ToString( );
			}
		}

		// Runs when the red text box value is changed
		private void RedTB_TextChanged( object sender, EventArgs e )
		{
			updateBackgroundColor( );
		}

		// Runs when the green text box value is changed
		private void GreenTB_TextChanged( object sender, EventArgs e )
		{
			updateBackgroundColor( );
		}

		// Runs when the blue text box value is changed
		private void BlueTB_TextChanged( object sender, EventArgs e )
		{
			updateBackgroundColor( );
		}

		// Runs when the background color radion button is changed
		private void BackGroundColorRB_CheckedChanged( object sender, EventArgs e )
		{
			if( BackGroundColorRB.Checked )
			{
				RedLbl.Enabled = true;
				GreenLbl.Enabled = true;
				BlueLbl.Enabled = true;

				RedTB.Enabled = true;
				GreenTB.Enabled = true;
				BlueTB.Enabled = true;

				CustomColorBtn.Enabled = true;
			}
			else
			{
				RedLbl.Enabled = false;
				GreenLbl.Enabled = false;
				BlueLbl.Enabled = false;

				RedTB.Enabled = false;
				GreenTB.Enabled = false;
				BlueTB.Enabled = false;

				CustomColorBtn.Enabled = false;
			}
		}

		// Runs when the light type is selected to be Point Light
		private void PointLightRB_CheckedChanged( object sender, EventArgs e )
		{
			updateLightType( );
		}

		// Runs when the light type is selected to be Directional Light
		private void DirectionalLightRB_CheckedChanged( object sender, EventArgs e )
		{
			updateLightType( );
		}

		// Runs when the light type is selected to be Ambient Light
		private void AmbientLightRB_CheckedChanged( object sender, EventArgs e )
		{
			updateLightType( );
		}

		// Runs when the light color button is clicked
		private void LightColorBtn_Click( object sender, EventArgs e )
		{
			ColorDialog MyDialog = new ColorDialog( );
			// Lets the user from selecting a custom color.
			MyDialog.AllowFullOpen = true;
			// Sets the initial color select to the current text color.
			MyDialog.Color = Color.White; // Load the default white background

			// Update the text box color if the user clicks OK  
			if( MyDialog.ShowDialog( ) == DialogResult.OK )
			{
				RedLightTB.Text = ( MyDialog.Color.R / 255.0 ).ToString( );
				GreenLightTB.Text = ( MyDialog.Color.G / 255.0 ).ToString( );
				BlueLightTB.Text = ( MyDialog.Color.B / 255.0 ).ToString( );
			}
		}

		// Runs when the value in the red light text box is changed
		private void RedLightTB_TextChanged( object sender, EventArgs e )
		{
			updateLightColor( );
		}

		// Runs when the value in the green light text box is changed
		private void GreenLightTB_TextChanged( object sender, EventArgs e )
		{
			updateLightColor( );
		}

		// Runs when the value in the blue light text box is changed
		private void BlueLightTB_TextChanged( object sender, EventArgs e )
		{
			updateLightColor( );
		}

		// Runs when the value in the lights' X position text box is changed
		private void XTB_TextChanged( object sender, EventArgs e )
		{
			updateLightColor( );
		}

		// Runs when the value in the lights' Y position text box is changed
		private void YTB_TextChanged( object sender, EventArgs e )
		{
			updateLightColor( );
		}

		// Runs when the value in the lights' Z position text box is changed
		private void ZTB_TextChanged( object sender, EventArgs e )
		{
			updateLightColor( );
		}

		// Runs when the light intensity track bar for red color is changed
		private void RedLightIntensityTrB_Scroll( object sender, EventArgs e )
		{
			RedIntensityTB.Text = RedIntensityTrB.Value.ToString();
			updateLightColor( );
		}

		// Runs when the light intensity track bar for green color is changed
		private void GreenIntensityTrB_Scroll( object sender, EventArgs e )
		{
			GreenIntensityTB.Text = GreenIntensityTrB.Value.ToString( );
			updateLightColor( );
		}

		// Runs when the light intensity track bar for blue color is changed
		private void BlueIntensityTrB_Scroll( object sender, EventArgs e )
		{
			BlueIntensityTB.Text = BlueIntensityTrB.Value.ToString( );
			updateLightColor( );
		}

		// Runs when the value in the red light intensity text box is changed
		private void RedIntensityTB_TextChanged( object sender, EventArgs e )
		{
			updateLightColor( );
		}

		// Runs when the value in the green light intensity text box is changed
		private void GreenIntensityTB_TextChanged( object sender, EventArgs e )
		{
			updateLightColor( );
		}

		// Runs when the value in the blue light intensity text box is changed
		private void BlueIntensityTB_TextChanged( object sender, EventArgs e )
		{
			updateLightColor( );
		}

		// Runs when the import button is clicked to import a text file forgenerating the video
        private void importBtn_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog.Filter = "text Image (.txt)|*.txt";
            openFileDialog.InitialDirectory = path + @"\TextFiles";
            openFileDialog.Title = "Select a text file to import";
            openFileDialog.FilterIndex = 1;

            openFileDialog.Multiselect = false;

            String filename = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                stop.Visible = true;

                string txt;

                SaveFileDialog browseFile = new SaveFileDialog();
                browseFile.Filter = "HAP Files (*.hap)|*.hap";
                browseFile.Title = "Browse HAP files";
                if (browseFile.ShowDialog() == DialogResult.Cancel)
                    return;
                try
                {
                    txt = browseFile.FileName;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error opening HAP file", "File Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                HapFACSVideo hfv = new HapFACSVideo(browseFile.FileName);

                filename = openFileDialog.FileName;
                double animationDuration = 0;
                double videoStartTime = 0;
                Animation animation = new Animation();

                try
                {
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        while (!sr.EndOfStream)
                        {
                            String line = sr.ReadLine();
                            string[] words = line.Split('|');

                            animation.addToAnimation(new AnimationSector(words[0].Trim(), words[1].Trim(),
                                                                          words[2].Trim(), words[3].Trim(),
                                                                          words[4].Trim(), words[5].Trim()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The imported file could not be read!" + ex);
                }

                animationDuration = animation.animationLengthIncludingStops();
                hfv.setAnimationDuration(animationDuration);
                videoStartTime = animation.animationStartTime();
                int numberOfSectors = animation.getAnimation().Count;

                for (double i = videoStartTime; i < videoStartTime + animationDuration; i = i + (animationDuration / 100))
                {
                    for (int j = 0; j < numberOfSectors; j++)
                    {
                        AnimationSector sector = (AnimationSector)(animation.getAnimation()[j]);
                        if (i < sector.getEndTime() && i >= sector.getStartTime())
							activateVideoAU( ref hfv, sector.getAU( ), sector.getSide(), sector.getStartIntensity(), sector.getEndIntensity(), sector.getStartTime(), sector.getEndTime(), i );
                    }
                }

                // Add the speaking times and hypertexts to the end of the file
                for (int s = 0; s < numberOfSectors; s++)
                {
                    AnimationSector sector = (AnimationSector)(animation.getAnimation()[s]);
					if( sector.getAU( ) == "Speak" )
                    {
						activateVideoAU( ref hfv, sector.getAU( ), sector.getStartTime(), sector.getTextToSpeak() );
                    }
                }

                String[] output = (String[])(hfv.generateVideo().ToArray(typeof(String)));

                System.IO.File.WriteAllLines(browseFile.FileName, output);
                StartRecording();

                axActiveHaptekX1.HyperText = @"\load [file=[" + browseFile.FileName + "]]";

                // For generating experiment videos
                // It stops the video recording 100 ms after all the last AU is animated.
                System.Threading.Thread.Sleep((int)(animationDuration * 1000) + 100);
                StopRecording();
                stop.Visible = false;
            } 
        }

		// Runs when the License menu is clicked
		private void LicenseMenu_Click( object sender, EventArgs e )
		{
			LicenseMenu license = new LicenseMenu( );
			license.Show( );
		}

		#endregion

		private void HapFACS_UserInterface_Load( object sender, EventArgs e )
		{
		}

		private void HapFACSHelpMenu_Click( object sender, EventArgs e )
		{
			Help.ShowHelp( this, "file:..\\..\\Documentation\\HapFACS_Documentation.chm" );
		}
	}
}