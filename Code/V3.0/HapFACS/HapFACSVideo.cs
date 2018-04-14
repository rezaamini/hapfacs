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
using System.Collections;
using System.Linq;
using System.Text;

namespace HapFACS
{
	/// <author>
	///     Reza Amini (ramin001@fiu.edu) 
	///     Affective Social Computing lab at Florida International University
	/// </author>
	/// <summary>
	///     HapFACSVideo object simulates a video generator which gives us the ability to animate AUs
	/// </summary>
	/// <remarks></remarks>
	class HapFACSVideo
	{
		#region Local variables
		private double minTime = 0.0015;
		// Upper-face registers and switched
		private double MidBrowUD = 0;
		private double LBrowUD = 0;
		private double RBrowUD = 0;
		private double eyes_sad = 0;
		private double trust = 0;
		private double distrust = 0;
		private double blink = 0;
		// Lower-face registers and switched
		private double lipcornerL3ty = 0;
		private double lipcornerR3ty = 0;
		private double smile3 = 0;
		private double kiss = 0;
		private double nostrilL3ty = 0;
		private double nostrilR3ty = 0;
		private double nostrilL3tx = 0;
		private double nostrilR3tx = 0;
		private double uh = 0;
		private double ow = 0;
		private double d = 0;
		private double f = 0;
		private double iy = 0;
		private double uw = 0;
		private double smirk = 0;
		private double smirkL = 0;
		private double mouth2ty = 0;
		private double th = 0;
		private double aa = 0;
		private double b = 0;
		private double ch = 0;
		private double ey = 0;
		// Eye movement registers and switched
		private double LEyeBallLR = 0;
		private double REyeBallLR = 0;
		private double LEyeBallUD = 0;
		private double REyeBallUD = 0;
		// Head movement registers and switched
		private double HeadTwist = 0;
		private double HeadForward = 0;
		private double HeadSideBend = 0;
		private double NeckForward = 0;

		private double time = 0; // can be removed
		private ArrayList scriptText; // the output script with timing which will be written in the Haptek animation file.

		private double animationDuration = 0; // total duration of the animation including the stops (if there is any)

		#endregion

		#region Constructor

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Creates an object of the Haptek animation given a file address (including the file name).
		/// </summary>
		/// <param name="fileName"> 
		///     Address (including the file name) of the output Haptek animation file.
		/// </param>
		/// <remarks></remarks>
		public HapFACSVideo( String fileName )
		{
			scriptText = new ArrayList( );
			fileName = fileName.Substring( fileName.LastIndexOf( "\\" ) + 1 );

			scriptText.Add( "\"#Haptek  Version= 2.00 Name= " + fileName + "   HapType= script FileType= text" );
			scriptText.Add( "    " );
		}

		#endregion

		#region Helper methods

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Sets the animation duration. It cannot set inside the constructor, because at the time of object creation, the animaiton duration is not known yet.
		/// </summary>
		/// <param name="animationDuration"> 
		///     Animation duration.
		/// </param>
		/// <remarks></remarks>
		public void setAnimationDuration( double animationDuration )
		{
			this.animationDuration = animationDuration;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Converts the intensity label (i.e., 0, A, B, C, D, E) to the numerical value of it based on the maximum value of each Haptek register for a specific AU.
		/// </summary>
		/// <param name="maxValue"> 
		///     Maximum value of the registed for a specific AU.
		/// </param>
		/// <param name="intensity"> 
		///     The intensity label (i.e., 0, A, B, C, D, E) to be converted.
		/// </param>
		/// <returns>
		///     The numerical value of the Haptek register for the input intensity.
		/// </returns>
		/// <remarks></remarks>
		public double convertIntensityToNumber( double maxValue, String intensity )
		{
			if( intensity == "A" )
			{
				return maxValue / Constants.AIntensity;
			}
			else if( intensity == "B" )
			{
				return maxValue / Constants.BIntensity;
			}
			else if( intensity == "C" )
			{
				return maxValue / Constants.CIntensity;
			}
			else if( intensity == "D" )
			{
				return maxValue / Constants.DIntensity;
			}
			else if( intensity == "E" )
			{
				return maxValue;
			}
			else
			{
				return 0;
			}
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets the variables that activate AU20 to intensity 0
		/// </summary>
		/// <remarks></remarks>
		public void resetAU20( )
		{
			lipcornerL3ty = 0;
			lipcornerR3ty = 0;
			b = 0;
			ow = 0;
			mouth2ty = 0;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Finishes the generated hypertext for video generation and returns the complete text to be printed to a file
		/// </summary>
		/// <returns>
		///     ArrayList containing the complete text to be printed to a file.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList generateVideo( )
		{
			scriptText.Add( "\"" );
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets registers of all the upper-face Action Units.
		/// </summary>
		/// <remarks></remarks>
		public void resetUpperAUs( )
		{
			MidBrowUD = 0;
			LBrowUD = 0;
			RBrowUD = 0;
			eyes_sad = 0;
			blink = 0;
			trust = 0;
			distrust = 0;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets registers of all the lower-face Action Units.
		/// </summary>
		/// <remarks></remarks>
		public void resetLowerAUs( )
		{
			lipcornerL3ty = 0;
			lipcornerR3ty = 0;
			smile3 = 0;
			kiss = 0;
			nostrilL3ty = 0;
			nostrilR3ty = 0;
			nostrilL3tx = 0;
			nostrilR3tx = 0;
			uh = 0;
			ow = 0;
			d = 0;
			iy = 0;
			uw = 0;
			smirk = 0;
			smirkL = 0;
			mouth2ty = 0;
			th = 0;
			aa = 0;
			b = 0;
			ch = 0;
			ey = 0;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets registers of all the head and eye Action Units.
		/// </summary>
		/// <remarks></remarks>
		public void resetHeadAndEyeAUs( )
		{
			LEyeBallLR = 0;
			REyeBallLR = 0;
			LEyeBallUD = 0;
			REyeBallUD = 0;
			HeadTwist = 0;
			HeadForward = 0;
			HeadSideBend = 0;
			NeckForward = 0;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets registers of all the Action Units.
		/// </summary>
		/// <remarks></remarks>
		public void resetAllAUs( )
		{
			resetHeadAndEyeAUs( );
			resetLowerAUs( );
			resetUpperAUs( );
		}
		#endregion

		#region Methods that generate the animation of different AUs

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU1 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU1 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU1 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU1.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU1.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU1 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU1Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double MidBrowUDStart = convertIntensityToNumber( Constants.MidBrowUD_MAX_AU1, startIntensity );
			double MidBrowUDEnd = convertIntensityToNumber( Constants.MidBrowUD_MAX_AU1, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( MidBrowUDEnd - MidBrowUDStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU1 registes with the starting values.
			if( startTime == currentTime )
			{
				MidBrowUD = MidBrowUDStart;
			}

			if( MidBrowUDEnd > MidBrowUDStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( MidBrowUD + intensityStep <= 0 ) // register value is above its minimum for AU1 (MidBrowUD is negative in AU1) 
				//{
					MidBrowUD += intensityStep;
				//}
				//else //Prevent passing the minimum intensity (i.e., 0)
				//	MidBrowUD = 0;
			}
			else // Activating the AU from a lower intensity to a higher intensity
			{
				//if( MidBrowUD - intensityStep >= Constants.MidBrowUD_MAX_AU1 ) // register value is below its maximum for AU1 (MidBrowUD is negative in AU1) 
					MidBrowUD -= intensityStep;
				//else // Prevent passing the maximum intensity
				//	MidBrowUD = Constants.MidBrowUD_MAX_AU1;
			}
			
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= MidBrowUD   f0= " + MidBrowUD + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU2 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="side"> 
		///     Side of AU2 in the animation. 
		/// </param>
		/// <param name="startIntensity"> 
		///     Start intensity of AU2 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU2 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU2.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU2.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU2 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU2Video( String side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double LBrowUDStart = convertIntensityToNumber( Constants.LBrowUD_MAX_AU2, startIntensity );
			double LBrowUDEnd = convertIntensityToNumber( Constants.LBrowUD_MAX_AU2, endIntensity );
			double RBrowUDStart = convertIntensityToNumber( Constants.RBrowUD_MAX_AU2, startIntensity );
			double RBrowUDEnd = convertIntensityToNumber( Constants.RBrowUD_MAX_AU2, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepL = Math.Abs( LBrowUDEnd - LBrowUDStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( RBrowUDEnd - RBrowUDStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU2 registes with the starting values.
			if( startTime == currentTime )
			{
				LBrowUD = LBrowUDStart; RBrowUD = RBrowUDStart;
			}

			if( side == "Bilateral" && LBrowUDEnd > LBrowUDStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( RBrowUD + intensityStepR <= 0) // register value is above its minimum for AU2 (RBrowUD and LBrowUD are negative in AU2) 
					RBrowUD += intensityStepR;
				//else
				//	RBrowUD = 0; // Prevent passing the minimum intensity
					
				//if( LBrowUD + intensityStepL <= 0 ) // register value is above its minimum for AU2 (RBrowUD and LBrowUD are negative in AU2) 
					LBrowUD += intensityStepL;
				//else
				//	LBrowUD = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && LBrowUDEnd > LBrowUDStart )
			{
				//if( LBrowUD + intensityStepL <= 0 ) // register value is above its minimum for AU2 (RBrowUD and LBrowUD are negative in AU2) 
					LBrowUD += intensityStepL;
				//else
				//	LBrowUD = 0; // Prevent passing the minimum intensity
			}
			else if( RBrowUDEnd > RBrowUDStart )// Right
			{
				//if( RBrowUD + intensityStepR <= 0 ) // register value is above its minimum for AU2 (RBrowUD and LBrowUD are negative in AU2) 
					RBrowUD += intensityStepR;
				//else
				//	RBrowUD = 0; // Prevent passing the minimum intensity
			}
			
			else if( side == "Bilateral" && LBrowUDEnd < LBrowUDStart) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( RBrowUD - intensityStepR >= Constants.RBrowUD_MAX_AU2 ) // register value is below its maximum for AU2 (RBrowUD and LBrowUD are negative in AU2) 
					RBrowUD -= intensityStepR;
				//else
				//	RBrowUD = Constants.RBrowUD_MAX_AU2; // Prevent passing the maximum intensity

				//if( LBrowUD - intensityStepL >= Constants.LBrowUD_MAX_AU2 ) // register value is below its maximum for AU2 (RBrowUD and LBrowUD are negative in AU2) 
					LBrowUD -= intensityStepL;
				//else
				//	LBrowUD = Constants.LBrowUD_MAX_AU2; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && LBrowUDEnd < LBrowUDStart) 
			{
				//if( LBrowUD - intensityStepL >= Constants.LBrowUD_MAX_AU2 ) // register value is below its maximum for AU2 (RBrowUD and LBrowUD are negative in AU2) 
					LBrowUD -= intensityStepL;
				//else
				//	LBrowUD = Constants.LBrowUD_MAX_AU2; // Prevent passing the maximum intensity
			}
			else if ( RBrowUDEnd < RBrowUDStart)// Right
			{
				//if( RBrowUD - intensityStepR >= Constants.RBrowUD_MAX_AU2 ) // register value is below its maximum for AU2 (RBrowUD and LBrowUD are negative in AU2) 
					RBrowUD -= intensityStepR;
				//else
				//	RBrowUD = Constants.RBrowUD_MAX_AU2; // Prevent passing the maximum intensity
			}
			
			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LBrowUD   f0= " + LBrowUD + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= RBrowUD   f0= " + RBrowUD + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LBrowUD   f0= " + LBrowUD + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= RBrowUD   f0= " + RBrowUD + "]" );
			}

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU4 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU4 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU4 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU4.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU4.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU4 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU4Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double MidBrowUDStart = convertIntensityToNumber( Constants.MidBrowUD_MAX_AU4, startIntensity );
			double MidBrowUDEnd = convertIntensityToNumber( Constants.MidBrowUD_MAX_AU4, endIntensity );
			double LBrowUDStart = convertIntensityToNumber( Constants.LBrowUD_MAX_AU4, startIntensity );
			double LBrowUDEnd = convertIntensityToNumber( Constants.LBrowUD_MAX_AU4, endIntensity );
			double RBrowUDStart = convertIntensityToNumber( Constants.RBrowUD_MAX_AU4, startIntensity );
			double RBrowUDEnd = convertIntensityToNumber( Constants.RBrowUD_MAX_AU4, endIntensity );
			double eyes_sadStart = convertIntensityToNumber( Constants.eyes_sad_MAX_AU4, startIntensity );
			double eyes_sadEnd = convertIntensityToNumber( Constants.eyes_sad_MAX_AU4, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepM = Math.Abs( MidBrowUDEnd - MidBrowUDStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepL = Math.Abs( LBrowUDEnd - LBrowUDStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( RBrowUDEnd - RBrowUDStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepE = Math.Abs( eyes_sadEnd - eyes_sadStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU4 registes with the starting values.
			if( startTime == currentTime )
			{
				MidBrowUD = MidBrowUDStart; LBrowUD = LBrowUDStart; RBrowUD = RBrowUDStart; eyes_sad = eyes_sadStart;
			}

			if( MidBrowUDEnd > MidBrowUDStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( MidBrowUD + intensityStepM <= Constants.MidBrowUD_MAX_AU4 ) // register value is below the maximum
					MidBrowUD += intensityStepM;
				//else
				//	MidBrowUD = Constants.MidBrowUD_MAX_AU4; // Prevent passing the maximum intensity

				//if( LBrowUD + intensityStepL <= Constants.LBrowUD_MAX_AU4 ) // register value is below the maximum
					LBrowUD += intensityStepL;
				//else
				//	LBrowUD = Constants.LBrowUD_MAX_AU4; // Prevent passing the maximum intensity

				//if( RBrowUD + intensityStepR <= Constants.RBrowUD_MAX_AU4 ) // register value is below the maximum
					RBrowUD += intensityStepR;
				//else
				//	RBrowUD = Constants.RBrowUD_MAX_AU4; // Prevent passing the maximum intensity

				//if( eyes_sad + intensityStepE <= Constants.eyes_sad_MAX_AU4 ) // register value is below the maximum
					eyes_sad += intensityStepE;
				//else
				//	eyes_sad = Constants.eyes_sad_MAX_AU4; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( MidBrowUD - intensityStepM >= 0 ) // register value is above the minimum
					MidBrowUD -= intensityStepM;
				//else
				//	MidBrowUD = 0; // Prevent passing the minimum intensity

				//if( LBrowUD - intensityStepL >= 0 ) // register value is above the minimum
					LBrowUD-= intensityStepL;
				//else
				//	LBrowUD = 0; // Prevent passing the minimum intensity

				//if( RBrowUD - intensityStepR >= 0 ) // register value is above the minimum
					RBrowUD -= intensityStepR;
				//else
				//	RBrowUD = 0; // Prevent passing the minimum intensity

				//if( eyes_sad - intensityStepE >= 0 ) // register value is above the minimum
					eyes_sad -= intensityStepE;
				//else
				//	eyes_sad = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= MidBrowUD   f0= " + MidBrowUD + "]" );
			// 0.0015 is the smallest effective time intervale in Haptek animation files. This value is calculated experimentally.
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= LBrowUD   f0= " + LBrowUD + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= RBrowUD   f0= " + RBrowUD + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= eyes_sad   f0= " + eyes_sad + "]" );
			
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU5 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU5 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU5 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU5.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU5.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU5 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU5Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double trustStart = convertIntensityToNumber( Constants.trust_MAX_AU5, startIntensity );
			double trustEnd = convertIntensityToNumber( Constants.trust_MAX_AU5, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( trustEnd - trustStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU5 registes with the starting values.
			if( startTime == currentTime )
			{
				trust = trustStart;
			}

			if( trustEnd > trustStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( trust + intensityStep <= 0 ) // register value is above the minimum for AU5 (trust is negative in AU5) 
					trust += intensityStep;
				//else
				//	trust = 0; // Prevent passing the minimum intensity
			}
			else
			{
				//if( trust - intensityStep >= Constants.trust_MAX_AU5 ) // register value is below the maximum for AU5 (trust is negative in AU5) 
					trust -= intensityStep;
				//else 
				//	trust = Constants.trust_MAX_AU5; // Prevent passing the maximum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= trust   f0= " + trust + "]" );
			
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU6 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU6 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU6 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU6.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU6.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU6 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU6Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double lipcornerL3tyStart = convertIntensityToNumber( Constants.lipcornerL3ty_MAX_AU6, startIntensity );
			double lipcornerL3tyEnd = convertIntensityToNumber( Constants.lipcornerL3ty_MAX_AU6, endIntensity );
			double lipcornerR3tyStart = convertIntensityToNumber( Constants.lipcornerR3ty_MAX_AU6, startIntensity );
			double lipcornerR3tyEnd = convertIntensityToNumber( Constants.lipcornerR3ty_MAX_AU6, endIntensity );
			double smile3Start = convertIntensityToNumber( Constants.smile3_MAX_AU6, startIntensity );
			double smile3End = convertIntensityToNumber( Constants.smile3_MAX_AU6, endIntensity );
			double kissStart = convertIntensityToNumber( Constants.kiss_MAX_AU6, startIntensity );
			double kissEnd = convertIntensityToNumber( Constants.kiss_MAX_AU6, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepL = Math.Abs( lipcornerL3tyEnd - lipcornerL3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( lipcornerR3tyEnd - lipcornerR3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepS = Math.Abs( smile3End - smile3Start ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepK = Math.Abs( kissEnd - kissStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU6 registes with the starting values.
			if( startTime == currentTime )
			{
				lipcornerL3ty = lipcornerL3tyStart; lipcornerR3ty = lipcornerR3tyStart; smile3 = smile3Start; kiss = smile3Start;
			}

			if( smile3End > smile3Start ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( lipcornerL3ty - intensityStepL >= Constants.lipcornerL3ty_MAX_AU6 ) // register value is below the maximum for AU6 (lipcornerR3ty and lipcornerL3ty are negative in AU6) 
					lipcornerL3ty -= intensityStepL;
				//else
				//	lipcornerL3ty = Constants.lipcornerL3ty_MAX_AU6; // Prevent passing the maximum intensity

				//if( lipcornerR3ty - intensityStepR >= Constants.lipcornerR3ty_MAX_AU6 ) // register value is below the maximum for AU6 (lipcornerR3ty and lipcornerL3ty are negative in AU6) 
					lipcornerR3ty -= intensityStepR;
				//else
				//	lipcornerR3ty = Constants.lipcornerR3ty_MAX_AU6; // Prevent passing the maximum intensity

				//if( smile3 + intensityStepS <= Constants.smile3_MAX_AU6 ) // register value is below the maximum for AU6 
					smile3 += intensityStepS;
				//else
				//	smile3 = Constants.smile3_MAX_AU6; // Prevent passing the maximum intensity

				//if( kiss + intensityStepK <= Constants.kiss_MAX_AU6 ) // register value is below the maximum for AU6 
					kiss += intensityStepK;
				//else
				//	kiss = Constants.kiss_MAX_AU6; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( lipcornerL3ty + intensityStepL <= 0 ) // register value is above the minimum for AU6 (lipcornerR3ty and lipcornerL3ty are negative in AU6) 
					lipcornerL3ty += intensityStepL;
				//else
				//	lipcornerL3ty = 0; // Prevent passing the maximum intensity

				//if( lipcornerR3ty + intensityStepR <= 0 ) // register value is above the minimum for AU6 (lipcornerR3ty and lipcornerL3ty are negative in AU6) 
					lipcornerR3ty += intensityStepR;
				//else
				//	lipcornerR3ty = 0; // Prevent passing the minimum intensity

				//if( smile3 - intensityStepS >= 0 ) // register value is above the minimum for AU6 
					smile3 -= intensityStepS;
				//else
				//	smile3 = 0; // Prevent passing the minimum intensity

				//if( kiss - intensityStepK >= 0 ) // register value is above the minimum for AU6 
					kiss -= intensityStepK;
				//else
				//	kiss = 0; // Prevent passing the maximum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= lipcornerL3ty   f0= " + lipcornerL3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= lipcornerR3ty   f0= " + lipcornerR3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= smile3   f0= " + smile3 + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= kiss   f0= " + kiss + "]" );
			
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU7 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU7 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU7 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU7.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU7.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU7 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU7Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double distrustStart = convertIntensityToNumber( Constants.distrust_MAX_AU7, startIntensity );
			double distrustEnd = convertIntensityToNumber( Constants.distrust_MAX_AU7, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( distrustEnd - distrustStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU7 registes with the starting values.
			if( startTime == currentTime )
			{
				distrust = distrustStart;
			}

			if( distrustEnd > distrustStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( distrust + intensityStep <= Constants.distrust_MAX_AU7 ) // register value is below its maximum for AU7
					distrust += intensityStep;
				//else
				//	distrust = Constants.distrust_MAX_AU7; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( distrust - intensityStep >= 0 ) // register value is above its minimum for AU7
					distrust -= intensityStep;
				//else
				//	distrust = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= distrust   f0= " + distrust + "]" );
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU8 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU8 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU8 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU8.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU8.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU8 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU8Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double bStart = convertIntensityToNumber( Constants.b_MAX_AU8, startIntensity );
			double bEnd = convertIntensityToNumber( Constants.b_MAX_AU8, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( bEnd - bStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU8 registes with the starting values.
			if( startTime == currentTime )
			{
				b = bStart;
			}

			if( bEnd > bStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( b + intensityStep <= Constants.b_MAX_AU8 ) // register value is below its maximum for AU8
					b += intensityStep;
				//else 
				//	b = Constants.b_MAX_AU8; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( b - intensityStep >= 0 ) // register value is above its minimum for AU8
					b -= intensityStep;
				//else
				//	b = 0; // Prevent passing the minimum intensity
			}
			
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= b   f0= " + b + "]" );
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU9 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU9 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU9 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU9.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU9.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU9 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU9Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double nostrilL3tyStart = convertIntensityToNumber( Constants.nostrilL3ty_MAX_AU9, startIntensity );
			double nostrilL3tyEnd = convertIntensityToNumber( Constants.nostrilL3ty_MAX_AU9, endIntensity );
			double nostrilR3tyStart = convertIntensityToNumber( Constants.nostrilR3ty_MAX_AU9, startIntensity );
			double nostrilR3tyEnd = convertIntensityToNumber( Constants.nostrilR3ty_MAX_AU9, endIntensity );
			double nostrilL3txStart = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU9, startIntensity );
			double nostrilL3txEnd = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU9, endIntensity );
			double nostrilR3txStart = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU9, startIntensity );
			double nostrilR3txEnd = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU9, endIntensity );
			double MidBrowUDStart = convertIntensityToNumber( Constants.MidBrowUD_MAX_AU9, startIntensity );
			double MidBrowUDEnd = convertIntensityToNumber( Constants.MidBrowUD_MAX_AU9, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepYL = Math.Abs( nostrilL3tyEnd - nostrilL3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepYR = Math.Abs( nostrilR3tyEnd - nostrilR3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepXL = Math.Abs( nostrilL3txEnd - nostrilL3txStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepXR = Math.Abs( nostrilR3txEnd - nostrilR3txStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepMB = Math.Abs( MidBrowUDEnd - MidBrowUDStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU9 registes with the starting values.
			if( startTime == currentTime )
			{
				nostrilL3ty = nostrilL3tyStart; nostrilR3ty = nostrilR3tyStart; nostrilL3tx = nostrilL3txStart; nostrilR3tx = nostrilR3txStart; MidBrowUD = MidBrowUDStart;
			}

			if( nostrilL3tyEnd > nostrilL3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( nostrilL3ty + intensityStepYL <= Constants.nostrilL3ty_MAX_AU9 ) // register value is below its maximum for AU9
					nostrilL3ty += intensityStepYL;
				//else
				//	nostrilL3ty = Constants.nostrilL3ty_MAX_AU9; // Prevent passing the maximum intensity

				//if( nostrilR3ty + intensityStepYR <= Constants.nostrilR3ty_MAX_AU9 ) // register value is below its maximum for AU9
					nostrilR3ty += intensityStepYR;
				//else
				//	nostrilR3ty = Constants.nostrilR3ty_MAX_AU9; // Prevent passing the maximum intensity

				//if( nostrilL3tx + intensityStepXL <= Constants.nostrilL3tx_MAX_AU9 ) // register value is below its maximum for AU9 
					nostrilL3tx += intensityStepXL;
				//else
				//	nostrilL3tx = Constants.nostrilL3tx_MAX_AU9; // Prevent passing the maximum intensity

				//if( nostrilR3tx - intensityStepXR >= Constants.nostrilR3tx_MAX_AU9 ) // register value is below its maximum for AU9 (nostrilR3tx is negative in AU9)
					nostrilR3tx -= intensityStepXR;
				//else
				//	nostrilR3tx = Constants.nostrilR3tx_MAX_AU9; // Prevent passing the maximum intensity

				//if( MidBrowUD + intensityStepMB <= Constants.MidBrowUD_MAX_AU9 ) // register value is below its maximum for AU9
					MidBrowUD += intensityStepMB;
				//else
				//	MidBrowUD = Constants.MidBrowUD_MAX_AU9; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( nostrilL3ty - intensityStepYL >= 0 ) // register value is above its minimum for AU9
					nostrilL3ty -= intensityStepYL;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilR3ty - intensityStepYR >= 0 ) // register value is above its minimum for AU9
					nostrilR3ty -= intensityStepYR;
				//else
				//	nostrilR3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilL3tx - intensityStepXL >= 0 ) // register value is above its minimum for AU9
					nostrilL3tx -= intensityStepXL;
				//else
				//	nostrilL3tx = 0; // Prevent passing the minimum intensity

				//if( nostrilR3tx + intensityStepXR <= 0 ) // register value is above its minimum for AU9 (nostrilR3tx is negative in AU9)
					nostrilR3tx += intensityStepXR;
				//else
				//	nostrilR3tx = 0; // Prevent passing the minimum intensity

				//if( MidBrowUD - intensityStepMB >= 0 ) // register value is above its minimum for AU9
					MidBrowUD -= intensityStepMB;
				//else
				//	MidBrowUD = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= nostrilL3ty   f0= " + nostrilL3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= nostrilR3ty   f0= " + nostrilR3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= nostrilL3tx   f0= " + nostrilL3tx + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= nostrilR3tx   f0= " + nostrilR3tx + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0060 ) + "]   \\setreg[name= MidBrowUD   f0= " + MidBrowUD + "]" );

			return scriptText;
		}

		/// <author> 
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU10 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU10 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU10 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU10.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU10.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU10 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU10Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double uhStart = convertIntensityToNumber( Constants.uh_MAX_AU10, startIntensity );
			double uhEnd = convertIntensityToNumber( Constants.uh_MAX_AU10, endIntensity );
			double owStart = convertIntensityToNumber( Constants.ow_MAX_AU10, startIntensity );
			double owEnd = convertIntensityToNumber( Constants.ow_MAX_AU10, endIntensity );
			double dStart = convertIntensityToNumber( Constants.d_MAX_AU10, startIntensity );
			double dEnd = convertIntensityToNumber( Constants.d_MAX_AU10, endIntensity );
			double iyStart = convertIntensityToNumber( Constants.iy_MAX_AU10, startIntensity );
			double iyEnd = convertIntensityToNumber( Constants.iy_MAX_AU10, endIntensity );
			double nostrilL3tyStart = convertIntensityToNumber( Constants.nostrilL3ty_MAX_AU10, startIntensity );
			double nostrilL3tyEnd = convertIntensityToNumber( Constants.nostrilL3ty_MAX_AU10, endIntensity );
			double nostrilR3tyStart = convertIntensityToNumber( Constants.nostrilR3ty_MAX_AU10, startIntensity );
			double nostrilR3tyEnd = convertIntensityToNumber( Constants.nostrilR3ty_MAX_AU10, endIntensity );
			double nostrilL3txStart = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU10, startIntensity );
			double nostrilL3txEnd = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU10, endIntensity );
			double nostrilR3txStart = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU10, startIntensity );
			double nostrilR3txEnd = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU10, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepU = Math.Abs( uhEnd - uhStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepO = Math.Abs( owEnd - owStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepD = Math.Abs( dEnd - dStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepI = Math.Abs( iyEnd - iyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepYL = Math.Abs( nostrilL3tyEnd - nostrilL3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepYR = Math.Abs( nostrilR3tyEnd - nostrilR3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepXL = Math.Abs( nostrilL3txEnd - nostrilL3txStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepXR = Math.Abs( nostrilR3txEnd - nostrilR3txStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU10 registes with the starting values.
			if( startTime == currentTime )
			{
				uh = uhStart; ow = owStart; d = dStart; iy = iyStart; nostrilL3ty = nostrilL3tyStart; nostrilR3ty = nostrilR3tyStart; nostrilL3tx = nostrilL3txStart; nostrilR3tx = nostrilR3txStart;
			}

			if( dEnd > dStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( uh - intensityStepU >= Constants.uh_MAX_AU10 ) // register value is below its maximum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					uh -= intensityStepU;
				//else
				//	uh = Constants.uh_MAX_AU10; // Prevent passing the maximum intensity

				//if( ow + intensityStepO <= Constants.ow_MAX_AU10 ) // register value is below its maximum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					ow += intensityStepO;
				//else
				//	ow = Constants.ow_MAX_AU10; // Prevent passing the maximum intensity

				//if( d + intensityStepD <= Constants.d_MAX_AU10 ) // register value is below its maximum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					d += intensityStepD;
				//else
				//	d = Constants.d_MAX_AU10; // Prevent passing the maximum intensity

				//if( iy - intensityStepI >= Constants.iy_MAX_AU10 ) // register value is below its maximum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					iy -= intensityStepI;
				//else
				//	iy = Constants.iy_MAX_AU10; // Prevent passing the maximum intensity

				//if( nostrilL3ty + intensityStepYL <= Constants.nostrilL3ty_MAX_AU10 ) // register value is below its maximum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					nostrilL3ty += intensityStepYL;
				//else
				//	nostrilL3ty = Constants.nostrilL3ty_MAX_AU10; // Prevent passing the maximum intensity

				//if( nostrilR3ty + intensityStepYR <= Constants.nostrilR3ty_MAX_AU10 ) // register value is below its maximum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					nostrilR3ty += intensityStepYR;
				//else
				//	nostrilR3ty = Constants.nostrilR3ty_MAX_AU10; // Prevent passing the maximum intensity

				//if( nostrilL3tx + intensityStepXL <= Constants.nostrilL3tx_MAX_AU10 ) // register value is below its maximum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					nostrilL3tx += intensityStepXL;
				//else
				//	nostrilL3tx = Constants.nostrilL3tx_MAX_AU10; // Prevent passing the maximum intensity

				//if( nostrilR3tx - intensityStepXR >= Constants.nostrilR3tx_MAX_AU10 ) // register value is below its maximum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					nostrilR3tx -= intensityStepXR;
				//else
				//	nostrilR3tx = Constants.nostrilR3tx_MAX_AU10; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( uh + intensityStepU <= 0 ) // register value is above its minimum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					uh += intensityStepU;
				//else
				//	uh = 0; // Prevent passing the minimum intensity

				//if( ow - intensityStepO >= 0 ) // register value is above its minimum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					ow -= intensityStepO;
				//else
				//	ow = 0; // Prevent passing the minimum intensity

				//if( d - intensityStepD >= 0 ) // register value is above its minimum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					d -= intensityStepD;
				//else
				//	d = 0; // Prevent passing the minimum intensity

				//if( iy + intensityStepI <= 0 ) // register value is above its minimum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					iy += intensityStepI;
				//else
				//	iy = 0; // Prevent passing the minimum intensity

				//if( nostrilL3ty - intensityStepYL >= 0 ) // register value is above its minimum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					nostrilL3ty -= intensityStepYL;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilR3ty - intensityStepYR >= 0 ) // register value is above its minimum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					nostrilR3ty -= intensityStepYR;
				//else
				//	nostrilR3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilL3tx - intensityStepXL >= 0 ) // register value is above its minimum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					nostrilL3tx -= intensityStepXL;
				//else
				//	nostrilL3tx = 0; // Prevent passing the minimum intensity

				//if( nostrilR3tx + intensityStepXR <= 0 ) // register value is above its minimum for AU10 (uh, iy, and nostrilR3tx are negative in AU10)
					nostrilR3tx += intensityStepXR;
				//else
				//	nostrilR3tx = 0; // Prevent passing the minimum intensity	
			}
			
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= uh   f0= " + uh + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= ow   f0= " + ow + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= d   f0= " + d + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= iy   f0= " + iy + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0060 ) + "]   \\setreg[name= nostrilL3ty   f0= " + nostrilL3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0075 ) + "]   \\setreg[name= nostrilR3ty   f0= " + nostrilR3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0090 ) + "]   \\setreg[name= nostrilL3tx   f0= " + nostrilL3tx + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0105 ) + "]   \\setreg[name= nostrilR3tx   f0= " + nostrilR3tx + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU11 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU11 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU11 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU11.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU11.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU11 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU11Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double nostrilL3txStart = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU11, startIntensity );
			double nostrilL3txEnd = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU11, endIntensity );
			double nostrilR3txStart = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU11, startIntensity );
			double nostrilR3txEnd = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU11, endIntensity );
			double uhStart = convertIntensityToNumber( Constants.uh_MAX_AU11, startIntensity );
			double uhEnd = convertIntensityToNumber( Constants.uh_MAX_AU11, endIntensity );
			double dStart = convertIntensityToNumber( Constants.d_MAX_AU11, startIntensity );
			double dEnd = convertIntensityToNumber( Constants.d_MAX_AU11, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepXL = Math.Abs( nostrilL3txEnd - nostrilL3txStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepXR = Math.Abs( nostrilR3txEnd - nostrilR3txStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepU = Math.Abs( uhEnd - uhStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepD = Math.Abs( dEnd - dStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU11 registes with the starting values.
			if( startTime == currentTime )
			{
				nostrilL3tx = nostrilL3txStart; nostrilR3tx = nostrilR3txStart; uh = uhStart; d = dStart;
			}

			if( nostrilL3txEnd > nostrilL3txStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( nostrilL3tx + intensityStepXL <= Constants.nostrilL3tx_MAX_AU11 ) // register value is below its maximum for AU11 (uh and nostrilL3tx are negative in AU11)
					nostrilL3tx += intensityStepXL;
				//else 
				//	nostrilL3tx = Constants.nostrilL3tx_MAX_AU11; // Prevent passing the maximum intensity

				//if( nostrilR3tx - intensityStepXR >= Constants.nostrilR3tx_MAX_AU11 ) // register value is below its maximum for AU11 (uh and nostrilL3tx are negative in AU11)
					nostrilR3tx -= intensityStepXR;
				//else
				//	nostrilR3tx = Constants.nostrilR3tx_MAX_AU11; // Prevent passing the maximum intensity

				//if( d + intensityStepD <= Constants.d_MAX_AU11 ) // register value is below its maximum for AU11 (uh and nostrilL3tx are negative in AU11)
					d += intensityStepD;
				//else
				//	d = Constants.d_MAX_AU11; // Prevent passing the maximum intensity

				//if (uh >= Constants.uh_MAX_AU11 ) // register value is below its maximum for AU11 (uh and nostrilL3tx are negative in AU11)
					uh -= intensityStepU;
				//else
				//	uh = Constants.uh_MAX_AU11; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( nostrilL3tx - intensityStepXL >= 0 ) // register value is above its minimum for AU11 (uh and nostrilL3tx are negative in AU11)
					nostrilL3tx -= intensityStepXL;
				//else
				//	nostrilL3tx = 0; // Prevent passing the minimum intensity

				//if( nostrilR3tx + intensityStepXR <= 0 ) // register value is above its minimum for AU11 (uh and nostrilL3tx are negative in AU11)
					nostrilR3tx += intensityStepXR;
				//else
				//	nostrilR3tx = 0; // Prevent passing the minimum intensity

				//if( d - intensityStepD >= 0 ) // register value is above its minimum for AU11 (uh and nostrilL3tx are negative in AU11)
					d -= intensityStepD;
				//else
				//	d = 0; // Prevent passing the minimum intensity

				//if( uh + intensityStepU <= 0 ) // register value is above its minimum for AU11 (uh and nostrilL3tx are negative in AU11)
					uh += intensityStepU;
				//else
				//	uh = 0; // Prevent passing the minimum intensity
			}
			
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= nostrilL3tx   f0= " + nostrilL3tx + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= nostrilR3tx   f0= " + nostrilR3tx + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= uh   f0= " + uh + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= d   f0= " + d + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU12 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU12 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU12 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU12.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU12.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU12 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU12Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double smile3Start = convertIntensityToNumber( Constants.smile3_MAX_AU12, startIntensity );
			double smile3End = convertIntensityToNumber( Constants.smile3_MAX_AU12, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepS = Math.Abs( smile3End - smile3Start ) * animationDuration / ( sectionDuration * 100 );
			
			// If it is the begining of activating the AU, over-write the AU12 registes with the starting values.
			if( startTime == currentTime )
			{
				smile3 = smile3Start;
			}

			if( smile3End > smile3Start )
			{
				//if( smile3 + intensityStepS <= Constants.smile3_MAX_AU12 ) // register value is below its maximum for AU12
					smile3 += intensityStepS;
				//else
				//	smile3 = Constants.smile3_MAX_AU12; // Prevent passing the maximum intensity
			}
			else  // Activating the AU from a higher intensity to a lower intensity
			{
				//if( smile3 - intensityStepS >= 0 )  // register value is above its minimum for AU12
					smile3 -= intensityStepS;
				//else
				//	smile3 = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= smile3   f0= " + smile3 + "]" );
			
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU13 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU13 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU13 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU13.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU13.
		/// </param>
		/// <param name="side"> 
		///     Side of AU13 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU13 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU13Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double lipcornerL3tyStart = convertIntensityToNumber( Constants.lipcornerL3ty_MAX_AU13, startIntensity );
			double lipcornerL3tyEnd = convertIntensityToNumber( Constants.lipcornerL3ty_MAX_AU13, endIntensity );
			double lipcornerR3tyStart = convertIntensityToNumber( Constants.lipcornerR3ty_MAX_AU13, startIntensity );
			double lipcornerR3tyEnd = convertIntensityToNumber( Constants.lipcornerR3ty_MAX_AU13, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepYL = Math.Abs( lipcornerL3tyEnd - lipcornerL3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepYR = Math.Abs( lipcornerR3tyEnd - lipcornerR3tyStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU13 registes with the starting values.
			if( startTime == currentTime )
			{
				lipcornerL3ty = lipcornerL3tyStart; lipcornerR3ty = lipcornerR3tyStart;
			}

			if( side == "Bilateral" && lipcornerL3tyEnd > lipcornerL3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( lipcornerL3ty + intensityStepYL <= Constants.lipcornerL3ty_MAX_AU13 ) // register value is below its maximum for AU13
					lipcornerL3ty += intensityStepYL;
				//else
				//	lipcornerL3ty = Constants.lipcornerL3ty_MAX_AU13; // Prevent passing the maximum intensity

				//if ( lipcornerR3ty + intensityStepYR <= Constants.lipcornerR3ty_MAX_AU13 ) // register value is below its maximum for AU13
					lipcornerR3ty += intensityStepYR;
				//else
				//	lipcornerR3ty = Constants.lipcornerR3ty_MAX_AU13; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && lipcornerL3tyEnd > lipcornerL3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( lipcornerL3ty + intensityStepYL <= Constants.lipcornerL3ty_MAX_AU13 ) // register value is below its maximum for AU13
					lipcornerL3ty += intensityStepYL;
				//else
				//	lipcornerL3ty = Constants.lipcornerL3ty_MAX_AU13; // Prevent passing the maximum intensity
			}
			else if( lipcornerR3tyEnd > lipcornerR3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( lipcornerR3ty + intensityStepYR <= Constants.lipcornerR3ty_MAX_AU13 ) // register value is below its maximum for AU13
					lipcornerR3ty += intensityStepYR;
				//else
				//	lipcornerR3ty = Constants.lipcornerR3ty_MAX_AU13; // Prevent passing the maximum intensity
			}
			else if( side == "Bilateral" && lipcornerL3tyEnd < lipcornerL3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( lipcornerL3ty - intensityStepYL >= 0 ) // register value is above its minimum for AU13
					lipcornerL3ty -= intensityStepYL;
				//else
				//	lipcornerL3ty = 0; // Prevent passing the minimum intensity

				//if( lipcornerR3ty - intensityStepYR >= 0 ) // register value is above its minimum for AU13
					lipcornerR3ty -= intensityStepYR;
				//else
				//	lipcornerR3ty = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && lipcornerL3tyEnd < lipcornerL3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( lipcornerL3ty - intensityStepYL >= 0 ) // register value is above its minimum for AU13
					lipcornerL3ty -= intensityStepYL;
				//else
				//	lipcornerL3ty = 0; // Prevent passing the minimum intensity
			}
			else if( lipcornerR3tyEnd < lipcornerR3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( lipcornerR3ty - intensityStepYR >= 0 )// register value is above its minimum for AU13
					lipcornerR3ty -= intensityStepYR;
				//else
				//	lipcornerR3ty = 0; // Prevent passing the minimum intensity
			}

			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= lipcornerL3ty   f0= " + lipcornerL3ty +  "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= lipcornerR3ty   f0= " + lipcornerR3ty + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= lipcornerL3ty   f0= " + lipcornerL3ty + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= lipcornerR3ty   f0= " + lipcornerR3ty + "]" );
			}
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU14 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU14 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU14 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU14.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU14.
		/// </param>
		/// <param name="side"> 
		///     Side of AU14 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU14 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU14Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double smirkStart = convertIntensityToNumber( Constants.smirk_MAX_AU14, startIntensity );
			double smirkEnd = convertIntensityToNumber( Constants.smirk_MAX_AU14, endIntensity );
			double smirkLStart = convertIntensityToNumber( Constants.smirkL_MAX_AU14, startIntensity );
			double smirkLEnd = convertIntensityToNumber( Constants.smirkL_MAX_AU14, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepR = Math.Abs( smirkEnd - smirkStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepL = Math.Abs( smirkLEnd - smirkLStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU13 registes with the starting values.
			if( startTime == currentTime )
			{
				smirk = smirkStart; smirkL = smirkLStart;
			}

			if( side == "Bilateral" && smirkEnd > smirkStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( smirk + intensityStepR <= Constants.smirk_MAX_AU14 ) // register value is below its maximum for AU14
					smirk += intensityStepR;
				//else
				//	smirk = Constants.smirk_MAX_AU14; // Prevent passing the maximum intensity

				//if( smirkL + intensityStepR <= Constants.smirkL_MAX_AU14 ) // register value is below its maximum for AU14
					smirkL += intensityStepL;
				//else
				//	smirkL = Constants.smirkL_MAX_AU14; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && smirkLEnd > smirkLStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( smirkL + intensityStepR <= Constants.smirkL_MAX_AU14 ) // register value is below its maximum for AU14
					smirkL += intensityStepL;
				//else
				//	smirkL = Constants.smirkL_MAX_AU14; // Prevent passing the maximum intensity
			}
			else if( smirkEnd > smirkStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( smirk + intensityStepR <= Constants.smirk_MAX_AU14 ) // register value is below its maximum for AU14
					smirk += intensityStepR;
				//else
				//	smirk = Constants.smirk_MAX_AU14; // Prevent passing the maximum intensity
			}
			else if( side == "Bilateral" && smirkEnd < smirkStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( smirk - intensityStepR >= 0 ) // register value is above its minimum for AU14
					smirk -= intensityStepR;
				//else
				//	smirk = 0; // Prevent passing the minimum intensity

				//if( smirkL - intensityStepR >= 0 ) // register value is above its minimum for AU14
					smirkL -= intensityStepL;
				//else
				//	smirkL = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && smirkLEnd < smirkLStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( smirkL - intensityStepR >= 0 ) // register value is above its minimum for AU14
					smirkL -= intensityStepL;
				//else
				//	smirkL = 0; // Prevent passing the minimum intensity
			}
			else if( smirkEnd < smirkStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( smirk - intensityStepR >= 0 ) // register value is above its minimum for AU14
					smirk -= intensityStepR;
				//else
				//	smirk = 0; // Prevent passing the minimum intensity
			}
			
			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= smirk   f0= " + smirk + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= smirkL   f0= " + smirkL + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= smirkL   f0= " + smirkL + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= smirk   f0= " + smirk + "]" );
			}

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU15 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU15 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU15 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU15.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU15.
		/// </param>
		/// <param name="side"> 
		///     Side of AU15 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU15 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU15Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double lipcornerL3tyStart = convertIntensityToNumber( Constants.lipcornerL3ty_MAX_AU15, startIntensity );
			double lipcornerL3tyEnd = convertIntensityToNumber( Constants.lipcornerL3ty_MAX_AU15, endIntensity );
			double lipcornerR3tyStart = convertIntensityToNumber( Constants.lipcornerR3ty_MAX_AU15, startIntensity );
			double lipcornerR3tyEnd = convertIntensityToNumber( Constants.lipcornerR3ty_MAX_AU15, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepYL = Math.Abs( lipcornerL3tyEnd - lipcornerL3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepYR = Math.Abs( lipcornerR3tyEnd - lipcornerR3tyStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU13 registes with the starting values.
			if( startTime == currentTime )
			{
				lipcornerL3ty = lipcornerL3tyStart; lipcornerR3ty = lipcornerR3tyStart;
			}

			if( side == "Bilateral" && lipcornerL3tyEnd > lipcornerL3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( lipcornerL3ty + intensityStepYL <= 0 ) // register value is above its minimum for AU15
					lipcornerL3ty += intensityStepYL;
				//else
				//	lipcornerL3ty = 0; // Prevent passing the minimum intensity

				//if( lipcornerR3ty + intensityStepYR <= 0 ) // register value is above its minimum for AU15
					lipcornerR3ty += intensityStepYR;
				//else
				//	lipcornerR3ty = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && lipcornerL3tyEnd > lipcornerL3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( lipcornerL3ty + intensityStepYL <= 0 ) // register value is above its minimum for AU15
					lipcornerL3ty += intensityStepYL;
				//else
				//	lipcornerL3ty = 0; // Prevent passing the minimum intensity
			}
			else if( lipcornerR3tyEnd > lipcornerR3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( lipcornerR3ty + intensityStepYR <= 0 ) // register value is above its minimum for AU15
					lipcornerR3ty += intensityStepYR;
				//else
				//	lipcornerR3ty = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Bilateral" && lipcornerL3tyEnd < lipcornerL3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( lipcornerL3ty - intensityStepYL >= Constants.lipcornerL3ty_MAX_AU15 ) // register value is below its maximum for AU15
					lipcornerL3ty -= intensityStepYL;
				//else
				//	lipcornerL3ty = Constants.lipcornerL3ty_MAX_AU15; // Prevent passing the maximum intensity

				//if( lipcornerR3ty - intensityStepYR >= Constants.lipcornerR3ty_MAX_AU15 ) // register value is below its maximum for AU15
					lipcornerR3ty -= intensityStepYR;
				//else
				//	lipcornerR3ty = Constants.lipcornerR3ty_MAX_AU15; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && lipcornerL3tyEnd < lipcornerL3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( lipcornerL3ty - intensityStepYL >= Constants.lipcornerL3ty_MAX_AU15 ) // register value is below its maximum for AU15
					lipcornerL3ty -= intensityStepYL;
				//else
				//	lipcornerL3ty = Constants.lipcornerL3ty_MAX_AU15; // Prevent passing the maximum intensity
			}
			else if( lipcornerR3tyEnd < lipcornerR3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( lipcornerR3ty - intensityStepYR >= Constants.lipcornerR3ty_MAX_AU15 ) // register value is below its maximum for AU15
					lipcornerR3ty -= intensityStepYR;
				//else
				//	lipcornerR3ty = Constants.lipcornerR3ty_MAX_AU15; // Prevent passing the maximum intensity
			}

			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= lipcornerL3ty   f0= " + lipcornerL3ty + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= lipcornerR3ty   f0= " + lipcornerR3ty + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= lipcornerL3ty   f0= " + lipcornerL3ty + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= lipcornerR3ty   f0= " + lipcornerR3ty + "]" );
			}
			
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU16 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU16 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU16 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU16.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU16.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU16 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU16Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double thStart = convertIntensityToNumber( Constants.th_MAX_AU16, startIntensity );
			double thEnd = convertIntensityToNumber( Constants.th_MAX_AU16, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( thEnd - thStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU16 registes with the starting values.
			if( startTime == currentTime )
			{
				th = thStart;
			}

			if( thEnd > thStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( th + intensityStep <= Constants.th_MAX_AU16 ) // register value is below its maximum for AU16
					th += intensityStep;
				//else 
				//	th = Constants.th_MAX_AU16; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( th - intensityStep >= 0 ) // register value is below its maximum for AU16
					th -= intensityStep;
				//else
				//	th = 0;  // Prevent passing the minimum intensity
			}
			
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= th   f0= " + th + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU17 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU17 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU17 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU17.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU17.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU17 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU17Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double lipcornerL3tyStart = convertIntensityToNumber( Constants.lipcornerL3ty_MAX_AU17, startIntensity );
			double lipcornerL3tyEnd = convertIntensityToNumber( Constants.lipcornerL3ty_MAX_AU17, endIntensity );
			double lipcornerR3tyStart = convertIntensityToNumber( Constants.lipcornerR3ty_MAX_AU17, startIntensity );
			double lipcornerR3tyEnd = convertIntensityToNumber( Constants.lipcornerR3ty_MAX_AU17, endIntensity );
			double aaStart = convertIntensityToNumber( Constants.aa_MAX_AU17, startIntensity );
			double aaEnd = convertIntensityToNumber( Constants.aa_MAX_AU17, endIntensity );
			double owStart = convertIntensityToNumber( Constants.ow_MAX_AU17, startIntensity );
			double owEnd = convertIntensityToNumber( Constants.ow_MAX_AU17, endIntensity );
			double mouth2tyStart = convertIntensityToNumber( Constants.mouth2ty_MAX_AU17, startIntensity );
			double mouth2tyEnd = convertIntensityToNumber( Constants.mouth2ty_MAX_AU17, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepL = Math.Abs( lipcornerL3tyEnd - lipcornerL3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( lipcornerR3tyEnd - lipcornerR3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepA = Math.Abs( aaEnd - aaStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepO = Math.Abs( owEnd - owStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepM = Math.Abs( mouth2tyEnd - mouth2tyStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU17 registes with the starting values.
			if( startTime == currentTime )
			{
				lipcornerL3ty = lipcornerL3tyStart; lipcornerR3ty = lipcornerR3tyStart; aa = aaStart; ow = owStart; mouth2ty = mouth2tyStart;
			}

			if( mouth2tyEnd < mouth2tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( lipcornerL3ty + intensityStepL >= Constants.lipcornerL3ty_MAX_AU17 ) // register value is below its maximum for AU17 (lipcornerL3ty, lipcornerR3ty, aa, and ow are negative in AU17)
					lipcornerL3ty += intensityStepL;
				//else
				//	lipcornerL3ty = Constants.lipcornerL3ty_MAX_AU17; // Prevent passing the maximum intensity		

				//if ( lipcornerR3ty + intensityStepR >= Constants.lipcornerR3ty_MAX_AU17 ) // register value is below its maximum for AU17 (lipcornerL3ty, lipcornerR3ty, aa, and ow are negative in AU17)
					lipcornerR3ty += intensityStepR;
				//else 
				//	lipcornerR3ty = Constants.lipcornerR3ty_MAX_AU17; // Prevent passing the maximum intensity

				//if ( aa + intensityStepA >= Constants.aa_MAX_AU17 ) // register value is below its maximum for AU17 (lipcornerL3ty, lipcornerR3ty, aa, and ow are negative in AU17)
					aa += intensityStepA;
				//else 
				//	aa = Constants.aa_MAX_AU17; // Prevent passing the maximum intensity

				//if ( ow + intensityStepO >= Constants.ow_MAX_AU17 ) // register value is below its maximum for AU17 (lipcornerL3ty, lipcornerR3ty, aa, and ow are negative in AU17)
					ow += intensityStepO;
				//else 
				//	ow = Constants.ow_MAX_AU17; // Prevent passing the maximum intensity

				//if ( mouth2ty - intensityStepM <= Constants.mouth2ty_MAX_AU17 ) // register value is below its maximum for AU17 
					mouth2ty -= intensityStepM;
				//else 
				//	mouth2ty = Constants.mouth2ty_MAX_AU17; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a lower intensity to a higher intensity
			{
				//if( lipcornerL3ty - intensityStepL <= 0 ) // register value is above its minimum for AU17 (lipcornerL3ty, lipcornerR3ty, aa, and ow are negative in AU17)
					lipcornerL3ty -= intensityStepL;
				//else
				//	lipcornerL3ty = 0; // Prevent passing the minimum intensity

				//if( lipcornerR3ty - intensityStepR <= 0 ) // register value is above its minimum for AU17 (lipcornerL3ty, lipcornerR3ty, aa, and ow are negative in AU17)
					lipcornerR3ty -= intensityStepR;
				//else
				//	lipcornerR3ty = 0; // Prevent passing the minimum intensity

				//if( aa - intensityStepA <= 0 ) // register value is above its minimum for AU17 (lipcornerL3ty, lipcornerR3ty, aa, and ow are negative in AU17)
					aa -= intensityStepA;
				//else
				//	aa = 0; // Prevent passing the minimum intensity

				//if( ow - intensityStepO <= 0 ) // register value is above its minimum for AU17 (lipcornerL3ty, lipcornerR3ty, aa, and ow are negative in AU17)
					ow -= intensityStepO;
				//else
				//	ow = 0; // Prevent passing the minimum intensity

				//if( mouth2ty + intensityStepM >= 0 ) // register value is above its minimum for AU17 
					mouth2ty += intensityStepM;
				//else
				//	mouth2ty = 0; // Prevent passing the minimum intensity		
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= lipcornerL3ty   f0= " + lipcornerL3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= lipcornerR3ty   f0= " + lipcornerR3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= aa   f0= " + aa + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= ow   f0= " + ow + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0060 ) + "]   \\setreg[name= mouth2ty   f0= " + mouth2ty + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU18 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU18 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU18 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU18.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU18.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU18 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU18Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double kissStart = convertIntensityToNumber( Constants.kiss_MAX_AU18, startIntensity );
			double kissEnd = convertIntensityToNumber( Constants.kiss_MAX_AU18, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( kissEnd - kissStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU18 registes with the starting values.
			if( startTime == currentTime )
			{
				kiss = kissStart;
			}

			if( kissEnd > kissStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( kiss + intensityStep <= Constants.kiss_MAX_AU18 ) // register value is below its maximum for AU18
					kiss += intensityStep;
				//else
				//	kiss = Constants.kiss_MAX_AU18; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( kiss - intensityStep >= 0 ) // register value is above its minimum for AU18 
					kiss -= intensityStep;
				//else
				//	kiss = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= kiss   f0= " + kiss + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU20 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU20 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU20 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU20.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU20.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU20 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU20Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double lipcornerL3tyStart = convertIntensityToNumber( Constants.lipcornerL3ty_MAX_AU20, startIntensity );
			double lipcornerL3tyEnd = convertIntensityToNumber( Constants.lipcornerL3ty_MAX_AU20, endIntensity );
			double lipcornerR3tyStart = convertIntensityToNumber( Constants.lipcornerR3ty_MAX_AU20, startIntensity );
			double lipcornerR3tyEnd = convertIntensityToNumber( Constants.lipcornerR3ty_MAX_AU20, endIntensity );
			double bStart = convertIntensityToNumber( Constants.b_MAX_AU20, startIntensity );
			double bEnd = convertIntensityToNumber( Constants.b_MAX_AU20, endIntensity );
			double owStart = convertIntensityToNumber( Constants.ow_MAX_AU20, startIntensity );
			double owEnd = convertIntensityToNumber( Constants.ow_MAX_AU20, endIntensity );
			double mouth2tyStart = convertIntensityToNumber( Constants.mouth2ty_MAX_AU20, startIntensity );
			double mouth2tyEnd = convertIntensityToNumber( Constants.mouth2ty_MAX_AU20, endIntensity );
			double nostrilL3txStart = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU20, startIntensity );
			double nostrilL3txEnd = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU20, endIntensity );
			double nostrilR3txStart = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU20, startIntensity );
			double nostrilR3txEnd = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU20, endIntensity );
			double thStart = convertIntensityToNumber( Constants.th_MAX_AU20, startIntensity );
			double thEnd = convertIntensityToNumber( Constants.th_MAX_AU20, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepL = Math.Abs( lipcornerL3tyEnd - lipcornerL3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( lipcornerR3tyEnd - lipcornerR3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepB = Math.Abs( bEnd - bStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepO = Math.Abs( owEnd - owStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepM = Math.Abs( mouth2tyEnd - mouth2tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepLx = Math.Abs( nostrilL3txEnd - nostrilL3txStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepRx = Math.Abs( nostrilR3txEnd - nostrilR3txStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepT = Math.Abs( thEnd - thStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU20 registes with the starting values.
			if( startTime == currentTime )
			{
				lipcornerL3ty = lipcornerL3tyStart; lipcornerR3ty = lipcornerR3tyStart; b = bStart; ow = owStart; mouth2ty = mouth2tyStart;
				nostrilL3tx = nostrilL3txStart; nostrilR3tx = nostrilR3txStart; th = thStart;
			}

			if( bEnd > bStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( lipcornerL3ty - intensityStepL >= Constants.lipcornerL3ty_MAX_AU20 ) // register value is below its maximum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					lipcornerL3ty -= intensityStepL;
				//else 
				//	lipcornerL3ty = Constants.lipcornerL3ty_MAX_AU20; // Prevent passing the maximum intensity

				//if ( lipcornerR3ty - intensityStepR >= Constants.lipcornerR3ty_MAX_AU20 ) // register value is below its maximum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					lipcornerR3ty -= intensityStepR;
				//else 
				//	lipcornerR3ty = Constants.lipcornerR3ty_MAX_AU20; // Prevent passing the maximum intensity

				//if ( b + intensityStepB <= Constants.b_MAX_AU20 ) // register value is below its maximum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					b += intensityStepB;
				//else 
				//	b = Constants.b_MAX_AU20; // Prevent passing the maximum intensity

				//if ( ow - intensityStepO >= Constants.ow_MAX_AU20 ) // register value is below its maximum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					ow -= intensityStepO;
				//else 
				//	ow = Constants.ow_MAX_AU20; // Prevent passing the maximum intensity

				//if ( mouth2ty - intensityStepM >= Constants.mouth2ty_MAX_AU20 ) // register value is below its maximum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					mouth2ty -= intensityStepM;
				//else 
				//	mouth2ty = Constants.mouth2ty_MAX_AU20; // Prevent passing the maximum intensity

				//if ( nostrilL3tx - intensityStepLx >= Constants.nostrilL3tx_MAX_AU20 ) // register value is below its maximum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					nostrilL3tx -= intensityStepLx;
				//else 
				//	nostrilL3tx = Constants.nostrilL3tx_MAX_AU20; // Prevent passing the maximum intensity

				//if ( nostrilR3tx + intensityStepRx <= Constants.nostrilR3tx_MAX_AU20 ) // register value is below its maximum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					nostrilR3tx += intensityStepRx;
				//else 
				//	nostrilR3tx = Constants.nostrilR3tx_MAX_AU20; // Prevent passing the maximum intensity

				//if ( th - intensityStepT >= Constants.th_MAX_AU20 ) // register value is below its maximum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					th -= intensityStepT;
				//else 
				//	th = Constants.th_MAX_AU20; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( lipcornerL3ty + intensityStepL <= 0 ) // register value is above its minimum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					lipcornerL3ty += intensityStepL;
				//else
				//	lipcornerL3ty = 0; // Prevent passing the minimum intensity

				//if( lipcornerR3ty + intensityStepR <= 0 ) // register value is above its minimum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					lipcornerR3ty += intensityStepR;
				//else
				//	lipcornerR3ty = 0; // Prevent passing the minimum intensity

				//if( b - intensityStepB >= 0 ) // register value is above its minimum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					b -= intensityStepB;
				//else
				//	b = 0; // Prevent passing the minimum intensity

				//if( ow + intensityStepO <= 0 ) // register value is above its minimum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					ow += intensityStepO;
				//else
				//	ow = 0; // Prevent passing the minimum intensity

				//if( mouth2ty + intensityStepM <= 0 ) // register value is above its minimum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					mouth2ty += intensityStepM;
				//else
				//	mouth2ty = 0; // Prevent passing the minimum intensity

				//if( nostrilL3tx + intensityStepLx <= 0 ) // register value is above its minimum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					nostrilL3tx += intensityStepLx;
				//else
				//	nostrilL3tx = 0; // Prevent passing the minimum intensity

				//if( nostrilR3tx - intensityStepRx >= 0 ) // register value is above its minimum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					nostrilR3tx -= intensityStepRx;
				//else
				//	nostrilR3tx = 0; // Prevent passing the minimum intensity

				//if( th + intensityStepT <= 0 ) // register value is above its minimum for AU20 (lipcornerL3ty, lipcornerR3ty, mouth2ty, nostrilL3tx, th, and ow are negative in AU20)
					th += intensityStepT;
				//else
				//	th = 0; // Prevent passing the minimum intensity	
			}
			
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= lipcornerL3ty   f0= " + lipcornerL3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= lipcornerR3ty   f0= " + lipcornerR3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= b   f0= " + b + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= ow   f0= " + ow + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0060 ) + "]   \\setreg[name= mouth2ty   f0= " + mouth2ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0075 ) + "]   \\setreg[name= nostrilL3tx   f0= " + nostrilL3tx + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0090 ) + "]   \\setreg[name= nostrilR3tx   f0= " + nostrilR3tx + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0105 ) + "]   \\setreg[name= th   f0= " + th + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU22 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU22 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU22 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU22.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU22.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU22 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU22Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double chStart = convertIntensityToNumber( Constants.ch_MAX_AU22, startIntensity );
			double chEnd = convertIntensityToNumber( Constants.ch_MAX_AU22, endIntensity );
			double uwStart = convertIntensityToNumber( Constants.uw_MAX_AU22, startIntensity );
			double uwEnd = convertIntensityToNumber( Constants.uw_MAX_AU22, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepC = Math.Abs( chEnd - chStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepU = Math.Abs( uwEnd - uwStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU22 registes with the starting values.
			if( startTime == currentTime )
			{
				ch = chStart; uw = uwStart;
			}

			if( chEnd > chStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( ch + intensityStepC <= Constants.ch_MAX_AU22 ) // register value is below its maximum for AU22
					ch += intensityStepC;
				//else 
				//	ch = Constants.ch_MAX_AU22; // Prevent passing the maximum intensity

				//if (  uw + intensityStepU <= Constants.uw_MAX_AU22 ) // register value is below its maximum for AU22
					uw += intensityStepU;
				//else
				//	uw = Constants.uw_MAX_AU22; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( ch - intensityStepC >= 0 ) // register value is above its minimum for AU22
					ch -= intensityStepC;
				//else
				//	ch = 0; // Prevent passing the minimum intensity

				//if( uw - intensityStepU >= 0 ) // register value is above its minimum for AU22
					uw -= intensityStepU;
				//else
				//	uw = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= ch   f0= " + ch + "]" );
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= uw   f0= " + uw + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU23 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU23 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU23 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU23.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU23.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU23 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU23Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double bStart = convertIntensityToNumber( Constants.b_MAX_AU23, startIntensity );
			double bEnd = convertIntensityToNumber( Constants.b_MAX_AU23, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepB = Math.Abs( bEnd - bStart ) * animationDuration / ( sectionDuration * 100 );
			//double intensityStepK = Math.Abs(kissEnd - kissStart) * animationDuration / (sectionDuration * 100);

			// If it is the begining of activating the AU, over-write the AU23 registes with the starting values.
			if( startTime == currentTime )
			{
				b = bStart; //kiss = kissStart;
			}

			if( bEnd > bStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( b + intensityStepB <= Constants.b_MAX_AU23 ) // register value is below its maximum for AU23
					b += intensityStepB;
				//else 
				//	b = Constants.b_MAX_AU23; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( b - intensityStepB >= 0 ) // register value is above its minimum for AU23
					b -= intensityStepB;
				//else
				//	b = 0; // Prevent passing the minimum intensity
			}
			
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= b   f0= " + b + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU24 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU24 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU24 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU24.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU24.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU24 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU24Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double bStart = convertIntensityToNumber( Constants.b_MAX_AU24, startIntensity );
			double bEnd = convertIntensityToNumber( Constants.b_MAX_AU24, endIntensity );
			double kissStart = convertIntensityToNumber( Constants.kiss_MAX_AU24, startIntensity );
			double kissEnd = convertIntensityToNumber( Constants.kiss_MAX_AU24, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepB = Math.Abs( bEnd - bStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepK = Math.Abs( kissEnd - kissStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU24 registes with the starting values.
			if( startTime == currentTime )
			{
				b = bStart; kiss = kissStart;
			}

			if( bEnd > bStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( b + intensityStepB <= Constants.b_MAX_AU24 ) // register value is below its maximum for AU24
					b += intensityStepB;
				//else
				//	b = Constants.b_MAX_AU24; // Prevent passing the maximum intensity

				//if ( kiss + intensityStepK <= Constants.kiss_MAX_AU24 ) // register value is below its maximum for AU24
					kiss += intensityStepK;
				//else
				//	kiss = Constants.kiss_MAX_AU24; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( b - intensityStepB >= 0 ) // register value is below its maximum for AU24
					b -= intensityStepB;
				//else
				//	b = 0; // Prevent passing the maximum intensity

				//if( kiss - intensityStepK >= 0 ) // register value is below its maximum for AU24
					kiss -= intensityStepK;
				//else
				//	kiss = 0; // Prevent passing the maximum intensity	
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= b   f0= " + b + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= kiss   f0= " + kiss + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU25 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU25 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU25 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU25.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU25.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU25 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU25Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double chStart = convertIntensityToNumber( Constants.ch_MAX_AU25, startIntensity );
			double chEnd = convertIntensityToNumber( Constants.ch_MAX_AU25, endIntensity );
			double nostrilL3tyStart = convertIntensityToNumber( Constants.nostrilL3ty_MAX_AU25, startIntensity );
			double nostrilL3tyEnd = convertIntensityToNumber( Constants.nostrilL3ty_MAX_AU25, endIntensity );
			double nostrilR3tyStart = convertIntensityToNumber( Constants.nostrilR3ty_MAX_AU25, startIntensity );
			double nostrilR3tyEnd = convertIntensityToNumber( Constants.nostrilR3ty_MAX_AU25, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepC = Math.Abs( chEnd - chStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepL = Math.Abs( nostrilL3tyEnd - nostrilL3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( nostrilR3tyEnd - nostrilR3tyStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU25 registes with the starting values.
			if( startTime == currentTime )
			{
				ch = chStart; nostrilL3ty = nostrilL3tyStart; nostrilR3ty = nostrilR3tyStart;
			}

			if( chEnd > chStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( ch + intensityStepC <= Constants.ch_MAX_AU25 ) // register value is below its maximum for AU25
					ch += intensityStepC;
				//else 
				//	ch = Constants.ch_MAX_AU25; // Prevent passing the maximum intensity

				//if( nostrilL3ty + intensityStepL <= Constants.nostrilL3ty_MAX_AU25 ) // register value is below its maximum for AU25
					nostrilL3ty += intensityStepL;
				//else
				//	nostrilL3ty = Constants.nostrilL3ty_MAX_AU25; // Prevent passing the maximum intensity

				//if ( nostrilR3ty + intensityStepR <= Constants.nostrilR3ty_MAX_AU25 ) // register value is below its maximum for AU25
					nostrilR3ty += intensityStepR;
				//else
				//	nostrilR3ty = Constants.nostrilR3ty_MAX_AU25; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( ch - intensityStepC >= 0 ) // register value is above its minimum for AU25
					ch -= intensityStepC;
				//else
				//	ch = 0; // Prevent passing the minimum intensity

				//if( nostrilL3ty - intensityStepL >= 0 ) // register value is above its minimum for AU25
					nostrilL3ty -= intensityStepL;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilR3ty - intensityStepR >= 0 ) // register value is above its minimum for AU25
					nostrilR3ty -= intensityStepR;
				//else
				//	nostrilR3ty = 0; // Prevent passing the minimum intensity
			}
			
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= ch   f0= " + ch + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= nostrilL3ty   f0= " + nostrilL3ty + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= nostrilR3ty   f0= " + nostrilR3ty + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU26 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU26 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU26 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU26.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU26.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU26 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU26Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double aaStart = convertIntensityToNumber( Constants.aa_MAX_AU26, startIntensity );
			double aaEnd = convertIntensityToNumber( Constants.aa_MAX_AU26, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( aaEnd - aaStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU26 registes with the starting values.
			if( startTime == currentTime )
			{
				aa = aaStart;
			}

			if( aaEnd > aaStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( aa + intensityStep <= Constants.aa_MAX_AU26 ) // register value is below its maximum for AU26
					aa += intensityStep;
				//else 
				//	aa = Constants.aa_MAX_AU26; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( aa - intensityStep >= 0 ) // register value is above its minimum for AU26
					aa -= intensityStep;
				//else
				//	aa = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= aa   f0= " + aa + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU27 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU27 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU27 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU27.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU27.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU27 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU27Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double aaStart = convertIntensityToNumber( Constants.aa_MAX_AU27, startIntensity );
			double aaEnd = convertIntensityToNumber( Constants.aa_MAX_AU27, endIntensity );
			double eyStart = convertIntensityToNumber( Constants.ey_MAX_AU27, startIntensity );
			double eyEnd = convertIntensityToNumber( Constants.ey_MAX_AU27, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepA = Math.Abs( aaEnd - aaStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepE = Math.Abs( eyEnd - eyStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU27 registes with the starting values.
			if( startTime == currentTime )
			{
				aa = aaStart; ey = eyStart;
			}

			if( aaEnd > aaStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( aa + intensityStepA <= Constants.aa_MAX_AU27 ) // register value is below its maximum for AU27
					aa += intensityStepA;
				//else 
				//	aa = Constants.aa_MAX_AU27; // Prevent passing the maximum intensity

				//if (ey + intensityStepE <= Constants.ey_MAX_AU27 ) // register value is below its maximum for AU27
					ey += intensityStepE;
				//else
				//	ey = Constants.ey_MAX_AU27; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( aa - intensityStepA >= 0 ) // register value is above its minimum for AU27
					aa -= intensityStepA;
				//else
				//	aa = 0; // Prevent passing the minimum intensity

				//if( ey - intensityStepE >= 0 ) // register value is above its minimum for AU27
					ey -= intensityStepE;
				//else
				//	ey = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= aa   f0= " + aa + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= ey   f0= " + ey + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU28 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU28 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU28 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU28.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU28.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU28 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU28Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double bStart = convertIntensityToNumber( Constants.b_MAX_AU28, startIntensity );
			double bEnd = convertIntensityToNumber( Constants.b_MAX_AU28, endIntensity );
			double dStart = convertIntensityToNumber( Constants.d_MAX_AU28, startIntensity );
			double dEnd = convertIntensityToNumber( Constants.d_MAX_AU28, endIntensity );
			double fStart = convertIntensityToNumber( Constants.f_MAX_AU28, startIntensity );
			double fEnd = convertIntensityToNumber( Constants.f_MAX_AU28, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepB = Math.Abs( bEnd - bStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepD = Math.Abs( dEnd - dStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepF = Math.Abs( fEnd - fStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU28 registes with the starting values.
			if( startTime == currentTime )
			{
				b = bStart; d = dStart; f = fStart;
			}

			if( bEnd > bStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( b + intensityStepB <= Constants.b_MAX_AU28 ) // register value is below its maximum for AU28
					b += intensityStepB;
				//else 
				//	b = Constants.b_MAX_AU28; // Prevent passing the maximum intensity

				//if( f + intensityStepF <= Constants.f_MAX_AU28 ) // register value is below its maximum for AU28
					f += intensityStepF;
				//else
				//	f = Constants.f_MAX_AU28; // Prevent passing the maximum intensity

				//if ( d - intensityStepD >= Constants.d_MAX_AU28 ) // register value is below its maximum for AU28
					d -= intensityStepD;
				//else
				//	d = Constants.d_MAX_AU28; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( b - intensityStepB >= 0 ) // register value is above its minimum for AU28
					b -= intensityStepB;
				//else
				//	b = 0; // Prevent passing the minimum intensity

				//if( f - intensityStepF >= 0 ) // register value is above its minimum for AU28
					f -= intensityStepF;
				//else
				//	f = 0; // Prevent passing the minimum intensity

				//if( d + intensityStepD <= 0 ) // register value is above its minimum for AU28
					d += intensityStepD;
				//else
				//	d = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= b   f0= " + b + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= f   f0= " + f + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= d   f0= " + d + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU38 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU38 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU38 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU38.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU38.
		/// </param>
		/// <param name="side"> 
		///     Side of AU38 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU38 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU38Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double nostrilL3tyStart = convertIntensityToNumber( Constants.nostrilL3ty_MAX_AU38, startIntensity );
			double nostrilL3tyEnd = convertIntensityToNumber( Constants.nostrilL3ty_MAX_AU38, endIntensity );
			double nostrilR3tyStart = convertIntensityToNumber( Constants.nostrilR3ty_MAX_AU38, startIntensity );
			double nostrilR3tyEnd = convertIntensityToNumber( Constants.nostrilR3ty_MAX_AU38, endIntensity );
			double nostrilL3txStart = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU38, startIntensity );
			double nostrilL3txEnd = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU38, endIntensity );
			double nostrilR3txStart = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU38, startIntensity );
			double nostrilR3txEnd = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU38, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepYL = Math.Abs( nostrilL3tyEnd - nostrilL3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepYR = Math.Abs( nostrilR3tyEnd - nostrilR3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepXL = Math.Abs( nostrilL3txEnd - nostrilL3txStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepXR = Math.Abs( nostrilR3txEnd - nostrilR3txStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU38 registes with the starting values.
			if( startTime == currentTime )
			{
				nostrilL3ty = nostrilL3tyStart; nostrilR3ty = nostrilR3tyStart; nostrilL3tx = nostrilL3txStart; nostrilR3tx = nostrilR3txStart;
			}

			if( side == "Bilateral" && nostrilL3tyEnd > nostrilL3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( nostrilL3ty + intensityStepYL <= 0 ) // register value is above its minimum for AU38
					nostrilL3ty += intensityStepYL;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilR3ty + intensityStepYR <= 0 ) // register value is above its minimum for AU38
					nostrilR3ty += intensityStepYR;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilL3tx + intensityStepXL <= 0 ) // register value is above its minimum for AU38
					nostrilL3tx += intensityStepXL;
				//else
				//	nostrilL3tx = 0; // Prevent passing the minimum intensity

				//if( nostrilR3tx - intensityStepXR >= 0 ) // register value is above its minimum for AU38
					nostrilR3tx -= intensityStepXR;
				//else
				//	nostrilR3tx = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && nostrilL3tyEnd > nostrilL3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( nostrilL3ty + intensityStepYL <= 0 ) // register value is above its minimum for AU38
					nostrilL3ty += intensityStepYL;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilL3tx + intensityStepXL <= 0 ) // register value is above its minimum for AU38
					nostrilL3tx += intensityStepXL;
				//else
				//	nostrilL3tx = 0; // Prevent passing the minimum intensity
			}
			else if( nostrilR3tyEnd > nostrilR3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( nostrilR3ty + intensityStepYR <= 0 ) // register value is above its minimum for AU38
					nostrilR3ty += intensityStepYR;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilR3tx - intensityStepXR >= 0 ) // register value is above its minimum for AU38
					nostrilR3tx -= intensityStepXR;
				//else
				//	nostrilR3tx = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Bilateral" && nostrilL3tyEnd < nostrilL3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( nostrilL3ty - intensityStepYL >= Constants.nostrilL3ty_MAX_AU38 ) // register value is below its maximum for AU38
					nostrilL3ty -= intensityStepYL;
				//else
				//	nostrilL3ty = Constants.nostrilL3ty_MAX_AU38; // Prevent passing the maximum intensity

				//if( nostrilR3ty - intensityStepYR >= Constants.nostrilR3ty_MAX_AU38 ) // register value is below its maximum for AU38
					nostrilR3ty -= intensityStepYR;
				//else
				//	nostrilL3ty = Constants.nostrilR3ty_MAX_AU38; // Prevent passing the minimum intensity

				//if( nostrilL3tx - intensityStepXL >= Constants.nostrilL3tx_MAX_AU38 ) // register value is below its maximum for AU38
					nostrilL3tx -= intensityStepXL;
				//else
				//	nostrilL3tx = Constants.nostrilL3tx_MAX_AU38; // Prevent passing the maximum intensity

				//if( nostrilR3tx + intensityStepXR <= Constants.nostrilR3tx_MAX_AU38 ) // register value is below its maximum for AU38
					nostrilR3tx += intensityStepXR;
				//else
				//	nostrilR3tx = Constants.nostrilR3tx_MAX_AU38; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && nostrilL3tyEnd < nostrilL3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( nostrilL3ty - intensityStepYL >= Constants.nostrilL3ty_MAX_AU38 ) // register value is below its maximum for AU38
					nostrilL3ty -= intensityStepYL;
				//else
				//	nostrilL3ty = Constants.nostrilL3ty_MAX_AU38; // Prevent passing the maximum intensity

				//if( nostrilL3tx - intensityStepXL >= Constants.nostrilL3tx_MAX_AU38 ) // register value is below its maximum for AU38
					nostrilL3tx -= intensityStepXL;
				//else
				//	nostrilL3tx = Constants.nostrilL3tx_MAX_AU38; // Prevent passing the maximum intensity
			}
			else if( nostrilR3tyEnd < nostrilR3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( nostrilR3ty - intensityStepYR >= Constants.nostrilR3ty_MAX_AU38 ) // register value is below its maximum for AU38
					nostrilR3ty -= intensityStepYR;
				//else
				//	nostrilL3ty = Constants.nostrilR3ty_MAX_AU38; // Prevent passing the maximum intensity

				//if( nostrilR3tx + intensityStepXR <= Constants.nostrilR3tx_MAX_AU38 ) // register value is below its maximum for AU38
					nostrilR3tx += intensityStepXR;
				//else
				//	nostrilR3tx = Constants.nostrilR3tx_MAX_AU38; // Prevent passing the maximum intensity
			}

			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= nostrilL3ty   f0= " + nostrilL3ty + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= nostrilR3ty   f0= " + nostrilR3ty + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= nostrilL3tx   f0= " + nostrilL3tx + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= nostrilR3tx   f0= " + nostrilR3tx + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= nostrilL3ty   f0= " + nostrilL3ty + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= nostrilL3tx   f0= " + nostrilL3tx + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= nostrilR3ty   f0= " + nostrilR3ty + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= nostrilR3tx   f0= " + nostrilR3tx + "]" );
			}

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU39 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU39 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU39 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU39.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU39.
		/// </param>
		/// <param name="side"> 
		///     Side of AU39 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU39 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU39Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double nostrilL3tyStart = convertIntensityToNumber( Constants.nostrilL3ty_MAX_AU39, startIntensity );
			double nostrilL3tyEnd = convertIntensityToNumber( Constants.nostrilL3ty_MAX_AU39, endIntensity );
			double nostrilR3tyStart = convertIntensityToNumber( Constants.nostrilR3ty_MAX_AU39, startIntensity );
			double nostrilR3tyEnd = convertIntensityToNumber( Constants.nostrilR3ty_MAX_AU39, endIntensity );
			double nostrilL3txStart = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU39, startIntensity );
			double nostrilL3txEnd = convertIntensityToNumber( Constants.nostrilL3tx_MAX_AU39, endIntensity );
			double nostrilR3txStart = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU39, startIntensity );
			double nostrilR3txEnd = convertIntensityToNumber( Constants.nostrilR3tx_MAX_AU39, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepYL = Math.Abs( nostrilL3tyEnd - nostrilL3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepYR = Math.Abs( nostrilR3tyEnd - nostrilR3tyStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepXL = Math.Abs( nostrilL3txEnd - nostrilL3txStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepXR = Math.Abs( nostrilR3txEnd - nostrilR3txStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU39 registes with the starting values.
			if( startTime == currentTime )
			{
				nostrilL3ty = nostrilL3tyStart; nostrilR3ty = nostrilR3tyStart; nostrilL3tx = nostrilL3txStart; nostrilR3tx = nostrilR3txStart;
			}

			if( side == "Bilateral" && nostrilR3tyEnd < nostrilR3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( nostrilL3ty - intensityStepYL >= Constants.nostrilL3ty_MAX_AU39 ) // register value is below its maximum for AU38
					nostrilL3ty -= intensityStepYL;
				//else
				//	nostrilL3ty = Constants.nostrilL3ty_MAX_AU39; // Prevent passing the maximum intensity

				//if( nostrilR3ty - intensityStepYR >= Constants.nostrilR3ty_MAX_AU39 ) // register value is below its maximum for AU38
					nostrilR3ty -= intensityStepYR;
				//else
				//	nostrilL3ty = Constants.nostrilR3ty_MAX_AU39; // Prevent passing the minimum intensity

				//if( nostrilL3tx - intensityStepXL >= Constants.nostrilL3tx_MAX_AU39 ) // register value is below its maximum for AU38
					nostrilL3tx -= intensityStepXL;
				//else
				//	nostrilL3tx = Constants.nostrilL3tx_MAX_AU39; // Prevent passing the maximum intensity

				//if( nostrilR3tx + intensityStepXR <= Constants.nostrilR3tx_MAX_AU39 ) // register value is below its maximum for AU38
					nostrilR3tx += intensityStepXR;
				//else
				//	nostrilR3tx = Constants.nostrilR3tx_MAX_AU39; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && nostrilL3txEnd < nostrilL3txStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( nostrilL3ty - intensityStepYL >= Constants.nostrilL3ty_MAX_AU39 ) // register value is below its maximum for AU38
					nostrilL3ty -= intensityStepYL;
				//else
				//	nostrilL3ty = Constants.nostrilL3ty_MAX_AU39; // Prevent passing the maximum intensity

				//if( nostrilL3tx - intensityStepXL >= Constants.nostrilL3tx_MAX_AU39 ) // register value is below its maximum for AU38
					nostrilL3tx -= intensityStepXL;
				//else
				//	nostrilL3tx = Constants.nostrilL3tx_MAX_AU39; // Prevent passing the maximum intensity
			}
			else if( nostrilR3tyEnd < nostrilR3tyStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( nostrilR3ty - intensityStepYR >= Constants.nostrilR3ty_MAX_AU39 ) // register value is below its maximum for AU38
					nostrilR3ty -= intensityStepYR;
				//else
				//	nostrilL3ty = Constants.nostrilR3ty_MAX_AU39; // Prevent passing the maximum intensity

				//if( nostrilR3tx + intensityStepXR <= Constants.nostrilR3tx_MAX_AU39 ) // register value is below its maximum for AU38
					nostrilR3tx += intensityStepXR;
				//else
				//	nostrilR3tx = Constants.nostrilR3tx_MAX_AU39; // Prevent passing the maximum intensity
			}
			else if( side == "Bilateral" && nostrilR3tyEnd > nostrilR3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( nostrilL3ty + intensityStepYL <= 0 ) // register value is above its minimum for AU38
					nostrilL3ty += intensityStepYL;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilR3ty + intensityStepYR <= 0 ) // register value is above its minimum for AU38
					nostrilR3ty += intensityStepYR;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilL3tx + intensityStepXL <= 0 ) // register value is above its minimum for AU38
					nostrilL3tx += intensityStepXL;
				//else
				//	nostrilL3tx = 0; // Prevent passing the minimum intensity

				//if( nostrilR3tx - intensityStepXR >= 0 ) // register value is above its minimum for AU38
					nostrilR3tx -= intensityStepXR;
				//else
				//	nostrilR3tx = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && nostrilL3tyEnd > nostrilL3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( nostrilL3ty + intensityStepYL <= 0 ) // register value is above its minimum for AU38
					nostrilL3ty += intensityStepYL;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilL3tx + intensityStepXL <= 0 ) // register value is above its minimum for AU38
					nostrilL3tx += intensityStepXL;
				//else
				//	nostrilL3tx = 0; // Prevent passing the minimum intensity
			}
			else if( nostrilR3tyEnd > nostrilR3tyStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( nostrilR3ty + intensityStepYR <= 0 ) // register value is above its minimum for AU38
					nostrilR3ty += intensityStepYR;
				//else
				//	nostrilL3ty = 0; // Prevent passing the minimum intensity

				//if( nostrilR3tx - intensityStepXR >= 0 ) // register value is above its minimum for AU38
					nostrilR3tx -= intensityStepXR;
				//else
				//	nostrilR3tx = 0; // Prevent passing the minimum intensity
			}

			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= nostrilL3ty   f0= " + nostrilL3ty + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= nostrilR3ty   f0= " + nostrilR3ty + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= nostrilL3tx   f0= " + nostrilL3tx + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= nostrilR3tx   f0= " + nostrilR3tx + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= nostrilL3ty   f0= " + nostrilL3ty + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= nostrilL3tx   f0= " + nostrilL3tx + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= nostrilR3ty   f0= " + nostrilR3ty + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= nostrilR3tx   f0= " + nostrilR3tx + "]" );
			}

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU41 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU41 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU41 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU41.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU41.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU41 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU41Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double trustStart = convertIntensityToNumber( Constants.trust_MAX_AU41, startIntensity );
			double trustEnd = convertIntensityToNumber( Constants.trust_MAX_AU41, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( trustEnd - trustStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU39 registes with the starting values.
			if( startTime == currentTime )
			{
				trust = trustStart;
			}

			if( trustEnd > trustStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( trust + intensityStep <= Constants.trust_MAX_AU41 ) // register value is below its maximum for AU41
					trust += intensityStep;
				//else 
				//	trust = Constants.trust_MAX_AU41; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( trust - intensityStep >= 0 ) // register value is above its minimum for AU41
					trust -= intensityStep;
				//else
				//	trust = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= trust   f0= " + trust + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU42 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU42 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU42 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU42.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU42.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU42 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU42Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double MidBrowUDStart = convertIntensityToNumber( Constants.MidBrowUD_MAX_AU42, startIntensity );
			double MidBrowUDEnd = convertIntensityToNumber( Constants.MidBrowUD_MAX_AU42, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( MidBrowUDEnd - MidBrowUDStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU39 registes with the starting values.
			if( startTime == currentTime )
			{
				MidBrowUD = MidBrowUDStart;
			}

			if( MidBrowUDEnd > MidBrowUDStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( MidBrowUD + intensityStep <= Constants.MidBrowUD_MAX_AU42 ) // register value is below its maximum for AU42
					MidBrowUD += intensityStep;
				//else 
				//	MidBrowUD = Constants.MidBrowUD_MAX_AU42; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( MidBrowUD - intensityStep >= 0 ) // register value is above its minimum for AU42
					MidBrowUD -= intensityStep;
				//else
				//	MidBrowUD = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= MidBrowUD   f0= " + MidBrowUD + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU43 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU43 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU43 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU43.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU43.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU43 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU43Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double blinkStart = convertIntensityToNumber( Constants.blink_MAX_AU43, startIntensity );
			double blinkEnd = convertIntensityToNumber( Constants.blink_MAX_AU43, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( blinkEnd - blinkStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU39 registes with the starting values.
			if( startTime == currentTime )
			{
				blink = blinkStart;
			}

			if( blinkEnd > blinkStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( blink + intensityStep<= Constants.blink_MAX_AU43 ) // register value is below its maximum for AU43
					blink += intensityStep;
				//else 
				//	blink = Constants.blink_MAX_AU43; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( blink - intensityStep >= 0 ) // register value is above its minimum for AU43
					blink -= intensityStep;
				//else
				//	blink = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= blink   f0= " + blink + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU44 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU44 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU44 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU44.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU44.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU44 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU44Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double MidBrowUDStart = convertIntensityToNumber( Constants.MidBrowUD_MAX_AU44, startIntensity );
			double MidBrowUDEnd = convertIntensityToNumber( Constants.MidBrowUD_MAX_AU44, endIntensity );
			double LBrowUDStart = convertIntensityToNumber( Constants.LBrowUD_MAX_AU44, startIntensity );
			double LBrowUDEnd = convertIntensityToNumber( Constants.LBrowUD_MAX_AU44, endIntensity );
			double RBrowUDStart = convertIntensityToNumber( Constants.RBrowUD_MAX_AU44, startIntensity );
			double RBrowUDEnd = convertIntensityToNumber( Constants.RBrowUD_MAX_AU44, endIntensity );
			double eyes_sadStart = convertIntensityToNumber( Constants.eyes_sad_MAX_AU44, startIntensity );
			double eyes_sadEnd = convertIntensityToNumber( Constants.eyes_sad_MAX_AU44, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepM = Math.Abs( MidBrowUDEnd - MidBrowUDStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepL = Math.Abs( LBrowUDEnd - LBrowUDStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( RBrowUDEnd - RBrowUDStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepE = Math.Abs( eyes_sadEnd - eyes_sadStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU39 registes with the starting values.
			if( startTime == currentTime )
			{
				MidBrowUD = MidBrowUDStart; LBrowUD = LBrowUDStart; RBrowUD = RBrowUDStart; eyes_sad = eyes_sadStart;
			}

			if( MidBrowUDEnd > MidBrowUDStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( MidBrowUD + intensityStepM <= Constants.MidBrowUD_MAX_AU44 ) // register value is below its maximum for AU44 (RBrowUD and LBrowUD are negative in AU44)
					MidBrowUD += intensityStepM;
				//else 
				//	MidBrowUD = Constants.MidBrowUD_MAX_AU44; // Prevent passing the maximum intensity

				//if( LBrowUD - intensityStepL >= Constants.LBrowUD_MAX_AU44 ) // register value is below its maximum for AU44 (RBrowUD and LBrowUD are negative in AU44)
					LBrowUD -= intensityStepL;
				//else
				//	LBrowUD = Constants.LBrowUD_MAX_AU44; // Prevent passing the maximum intensity

				//if( RBrowUD - intensityStepR >= Constants.RBrowUD_MAX_AU44 ) // register value is below its maximum for AU44 (RBrowUD and LBrowUD are negative in AU44)
					RBrowUD -= intensityStepR;
				//else
				//	RBrowUD = Constants.RBrowUD_MAX_AU44; // Prevent passing the maximum intensity

				//if ( eyes_sad + intensityStepE <= Constants.eyes_sad_MAX_AU44 ) // register value is below its maximum for AU44 (RBrowUD and LBrowUD are negative in AU44)
					eyes_sad += intensityStepE;
				//else
				//	eyes_sad = Constants.eyes_sad_MAX_AU44; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( MidBrowUD - intensityStepM >= 0 ) // register value is above its minimum for AU44 (RBrowUD and LBrowUD are negative in AU44)
					MidBrowUD -= intensityStepM;
				//else
				//	MidBrowUD = 0; // Prevent passing the minimum intensity

				//if( LBrowUD + intensityStepL <= 0 ) // register value is above its minimum for AU44 (RBrowUD and LBrowUD are negative in AU44)
					LBrowUD += intensityStepL;
				//else
				//	LBrowUD = 0; // Prevent passing the minimum intensity

				//if( RBrowUD + intensityStepR <= 0 ) // register value is above its minimum for AU44 (RBrowUD and LBrowUD are negative in AU44)
					RBrowUD += intensityStepR;
				//else
				//	RBrowUD = 0; // Prevent passing the minimum intensity

				//if( eyes_sad - intensityStepE >= 0 ) // register value is above its minimum for AU44 (RBrowUD and LBrowUD are negative in AU44)
					eyes_sad -= intensityStepE;
				//else
				//	eyes_sad = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= MidBrowUD   f0= " + MidBrowUD + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= LBrowUD   f0= " + LBrowUD + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\setreg[name= RBrowUD   f0= " + RBrowUD + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0045 ) + "]   \\setreg[name= eyes_sad   f0= " + eyes_sad + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU45 at a specific time.
		/// </summary>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU45 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU45Video( double currentTime )
		{
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\SetSwitch[switch= blinks state=CloseEye]" );
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU45 at a specific time.
		/// </summary>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <param name="side"> 
		///     Side of AU45 to be animated (i.e., left or right).
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU45 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU46Video( String side, double currentTime )
		{
			if( side == "Left" )
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\SetSwitch[switch= blinks state=winkleftfast] " );
			else
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\SetSwitch[switch= blinks state=winkrightfast] " );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU51 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU51 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU51 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU51.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU51.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU51 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU51Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double HeadTwistStart = convertIntensityToNumber( Constants.HeadTwist_MAX_AU51, startIntensity );
			double HeadTwistEnd = convertIntensityToNumber( Constants.HeadTwist_MAX_AU51, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( HeadTwistEnd - HeadTwistStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU51 registes with the starting values.
			if( startTime == currentTime )
			{
				HeadTwist = HeadTwistStart;
			}

			if( HeadTwist > HeadTwistStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( HeadTwist + intensityStep >= Constants.HeadTwist_MAX_AU51 ) // register value is below its maximum for AU51 (HeadTwist is negative in AU51)
					HeadTwist += intensityStep;
				//else 
				//	HeadTwist = Constants.HeadTwist_MAX_AU51; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a lower intensity to a higher intensity
			{
				//if( HeadTwist - intensityStep <= 0 ) // register value is above its minimum for AU51 (HeadTwist is negative in AU51)
					HeadTwist -= intensityStep;
				//else
				//	HeadTwist = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= HeadTwist   f0= " + HeadTwist + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU52 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU52 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU52 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU52.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU52.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU52 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU52Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double HeadTwistStart = convertIntensityToNumber( Constants.HeadTwist_MAX_AU52, startIntensity );
			double HeadTwistEnd = convertIntensityToNumber( Constants.HeadTwist_MAX_AU52, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( HeadTwistEnd - HeadTwistStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU52 registes with the starting values.
			if( startTime == currentTime )
			{
				HeadTwist = HeadTwistStart;
			}

			if( HeadTwistEnd > HeadTwistStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( HeadTwist + intensityStep <= Constants.HeadTwist_MAX_AU52 ) // register value is below its maximum for AU52
					HeadTwist += intensityStep;
				//else 
				//	HeadTwist = Constants.HeadTwist_MAX_AU52; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( HeadTwist - intensityStep >= 0 ) // register value is above its minimum for AU52
					HeadTwist -= intensityStep;
				//else
				//	HeadTwist = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= HeadTwist   f0= " + HeadTwist + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU53 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU53 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU53 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU53.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU53.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU53 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU53Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double HeadForwardStart = convertIntensityToNumber( Constants.HeadForward_MAX_AU53, startIntensity );
			double HeadForwardEnd = convertIntensityToNumber( Constants.HeadForward_MAX_AU53, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( HeadForwardEnd - HeadForwardStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU53 registes with the starting values.
			if( startTime == currentTime )
			{
				HeadForward = HeadForwardStart;
			}

			if( HeadForwardEnd > HeadForwardStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( HeadForward + intensityStep >= Constants.HeadForward_MAX_AU53 ) // register value is below its maximum for AU53
					HeadForward += intensityStep;
				//else
				//	HeadForward = Constants.HeadForward_MAX_AU53; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a lower intensity to a higher intensity
			{
				//if( HeadForward - intensityStep <= 0 ) // register value is above its minimum for AU53
					HeadForward -= intensityStep;
				//else
				//	HeadForward = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= HeadForward   f0= " + HeadForward + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU54 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU54 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU54 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU54.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU54.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU54 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU54Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double HeadForwardStart = convertIntensityToNumber( Constants.HeadForward_MAX_AU54, startIntensity );
			double HeadForwardEnd = convertIntensityToNumber( Constants.HeadForward_MAX_AU54, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( HeadForwardEnd - HeadForwardStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU54 registes with the starting values.
			if( startTime == currentTime )
			{
				HeadForward = HeadForwardStart;
			}

			if( HeadForwardEnd > HeadForwardStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( HeadForward + intensityStep <= Constants.HeadForward_MAX_AU54 ) // register value is below its maximum for AU54
					HeadForward += intensityStep;
				//else
				//	HeadForward = Constants.HeadForward_MAX_AU54; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( HeadForward - intensityStep >= 0 ) // register value is above its minimum for AU54
					HeadForward -= intensityStep;
				//else
				//	HeadForward = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= HeadForward   f0= " + HeadForward + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU55 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU55 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU55 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU55.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU55.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU55 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU55Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double HeadSideBendStart = convertIntensityToNumber( Constants.HeadSideBend_MAX_AU55, startIntensity );
			double HeadSideBendEnd = convertIntensityToNumber( Constants.HeadSideBend_MAX_AU55, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( HeadSideBendEnd - HeadSideBendStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU55 registes with the starting values.
			if( startTime == currentTime )
			{
				HeadSideBend = HeadSideBendStart;
			}

			if( HeadSideBendEnd > HeadSideBendStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( HeadSideBend + intensityStep <= Constants.HeadSideBend_MAX_AU55 ) // register value is below its maximum for AU55
					HeadSideBend += intensityStep;
				//else
				//	HeadSideBend = Constants.HeadSideBend_MAX_AU55; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( HeadSideBend - intensityStep >= 0 ) // register value is above its minimum for AU55
					HeadSideBend -= intensityStep;
				//else
				//	HeadSideBend = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= HeadSideBend   f0= " + HeadSideBend + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU56 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU56 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU56 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU56.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU56.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU56 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU56Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double HeadSideBendStart = convertIntensityToNumber( Constants.HeadSideBend_MAX_AU56, startIntensity );
			double HeadSideBendEnd = convertIntensityToNumber( Constants.HeadSideBend_MAX_AU56, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStep = Math.Abs( HeadSideBendEnd - HeadSideBendStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU56 registes with the starting values.
			if( startTime == currentTime )
			{
				HeadSideBend = HeadSideBendStart;
			}

			if( HeadSideBendEnd > HeadSideBendStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( HeadSideBend + intensityStep >= Constants.HeadSideBend_MAX_AU56 ) // register value is below its maximum for AU56
					HeadSideBend += intensityStep;
				//else
				//	HeadSideBend = Constants.HeadSideBend_MAX_AU56; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a lower intensity to a higher intensity
			{
				//if( HeadSideBend - intensityStep <= 0 ) // register value is above its minimum for AU56
					HeadSideBend -= intensityStep;
				//else
				//	HeadSideBend = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= HeadSideBend   f0= " + HeadSideBend + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU57 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU57 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU57 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU57.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU57.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU57 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU57Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double HeadForwardStart = convertIntensityToNumber( Constants.HeadForward_MAX_AU57, startIntensity );
			double HeadForwardEnd = convertIntensityToNumber( Constants.HeadForward_MAX_AU57, endIntensity );
			double NeckForwardStart = convertIntensityToNumber( Constants.NeckForward_MAX_AU57, startIntensity );
			double NeckForwardEnd = convertIntensityToNumber( Constants.NeckForward_MAX_AU57, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepH = Math.Abs( HeadForwardEnd - HeadForwardStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepN = Math.Abs( NeckForwardEnd - NeckForwardStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU56 registes with the starting values.
			if( startTime == currentTime )
			{
				HeadForward = HeadForwardStart; NeckForward = NeckForwardStart;
			}

			if( NeckForwardEnd > NeckForwardStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( HeadForward - intensityStepH >= Constants.HeadForward_MAX_AU57 ) // register value is below its maximum for AU57 (HeadForward is negative in AU57)
					HeadForward -= intensityStepH;
				//else 
				//	HeadForward = Constants.HeadForward_MAX_AU57; // Prevent passing the maximum intensity

				//if ( NeckForward + intensityStepN <= Constants.NeckForward_MAX_AU57 ) // register value is below its maximum for AU57 (HeadForward is negative in AU57)
					NeckForward += intensityStepN;
				//else
				//	NeckForward = Constants.NeckForward_MAX_AU57; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( HeadForward + intensityStepH <= 0 ) // register value is above its minimum for AU57 (HeadForward is negative in AU57)
					HeadForward += intensityStepH;
				//else
				//	HeadForward = 0; // Prevent passing the minimum intensity

				//if( NeckForward - intensityStepN >= 0 ) // register value is above its minimum for AU57 (HeadForward is negative in AU57)
					NeckForward -= intensityStepN;
				//else
				//	NeckForward = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= HeadForward   f0= " + HeadForward + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= NeckForward   f0= " + NeckForward + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU58 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU58 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU58 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU58.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU58.
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU58 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU58Video( string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double HeadForwardStart = convertIntensityToNumber( Constants.HeadForward_MAX_AU58, startIntensity );
			double HeadForwardEnd = convertIntensityToNumber( Constants.HeadForward_MAX_AU58, endIntensity );
			double NeckForwardStart = convertIntensityToNumber( Constants.NeckForward_MAX_AU58, startIntensity );
			double NeckForwardEnd = convertIntensityToNumber( Constants.NeckForward_MAX_AU58, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepH = Math.Abs( HeadForwardEnd - HeadForwardStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepN = Math.Abs( NeckForwardEnd - NeckForwardStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU56 registes with the starting values.
			if( startTime == currentTime )
			{
				HeadForward = HeadForwardStart; NeckForward = NeckForwardStart;
			}

			if( HeadForwardEnd > HeadForwardStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( HeadForward + intensityStepH <= Constants.HeadForward_MAX_AU58 ) // register value is below its maximum for AU58 (NeckForward is negative in AU58)
					HeadForward += intensityStepH;
				//else 
				//	HeadForward = Constants.HeadForward_MAX_AU58; // Prevent passing the maximum intensity

				//if ( NeckForward - intensityStepN >= Constants.NeckForward_MAX_AU58 ) // register value is below its maximum for AU58 (NeckForward is negative in AU58)
					NeckForward -= intensityStepN;
				//else
				//	NeckForward = Constants.NeckForward_MAX_AU58; // Prevent passing the maximum intensity
			}
			else // Activating the AU from a higher intensity to a lower intensity
			{
				//if( HeadForward - intensityStepH >= 0 ) // register value is above its minimum for AU58 (NeckForward is negative in AU58)
					HeadForward -= intensityStepH;
				//else
				//	HeadForward = 0; // Prevent passing the minimum intensity

				//if( NeckForward + intensityStepN <= 0 ) // register value is above its minimum for AU58 (NeckForward is negative in AU58)
					NeckForward += intensityStepN;
				//else
				//	NeckForward = 0; // Prevent passing the minimum intensity
			}

			scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= HeadForward   f0= " + HeadForward + "]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= NeckForward   f0= " + NeckForward + "]" );

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU59 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU59 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AUM59Video( double currentTime )
		{
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\SetSwitch[switch= stop state=gestures_on]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\SetSwitch[switch= gestures state=nod]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\SetSwitch[switch= stop state=stop]" );
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU60 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU60 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AUM60Video( double currentTime )
		{
			scriptText.Add( "\\clock [t= " + currentTime + "]   \\SetSwitch[switch= stop state=gestures_on]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\SetSwitch[switch= gestures state=shake]" );
			scriptText.Add( "\\clock [t= " + ( currentTime + 0.0030 ) + "]   \\SetSwitch[switch= stop state=stop]" );
			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU61 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU61 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU61 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU61.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU61.
		/// </param>
		/// <param name="side"> 
		///     Side of AU61 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU61 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU61Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double LEyeBallLRStart = convertIntensityToNumber( Constants.LEyeBallLR_MAX_AU61, startIntensity );
			double LEyeBallLREnd = convertIntensityToNumber( Constants.LEyeBallLR_MAX_AU61, endIntensity );
			double REyeBallLRStart = convertIntensityToNumber( Constants.REyeBallLR_MAX_AU61, startIntensity );
			double REyeBallLREnd = convertIntensityToNumber( Constants.REyeBallLR_MAX_AU61, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepL = Math.Abs( LEyeBallLREnd - LEyeBallLRStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( REyeBallLREnd - REyeBallLRStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU61 registes with the starting values.
			if( startTime == currentTime )
			{
				LEyeBallLR = LEyeBallLRStart; REyeBallLR = REyeBallLRStart;
			}

			if( side == "Bilateral" && LEyeBallLREnd > LEyeBallLRStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( LEyeBallLR + intensityStepL <= 0 ) // register value is above its minimum for AU61  
					LEyeBallLR += intensityStepL;
				//else
				//	LEyeBallLR = 0; // Prevent passing the minimum intensity

				//if( REyeBallLR + intensityStepR <= 0 ) // register value is above its minimum for AU61  
					REyeBallLR += intensityStepR;
				//else
				//	REyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && LEyeBallLREnd > LEyeBallLRStart )
			{
				//if( LEyeBallLR + intensityStepL <= 0 ) // register value is above its minimum for AU61
					LEyeBallLR += intensityStepL;
				//else
				//	LEyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( REyeBallLREnd > REyeBallLRStart )// Right
			{
				//if( REyeBallLR + intensityStepR <= 0 ) // register value is above its minimum for AU61  
					REyeBallLR += intensityStepR;
				//else
				//	REyeBallLR = 0; // Prevent passing the minimum intensity
			}

			else if( side == "Bilateral" && LEyeBallLREnd < LEyeBallLRStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( LEyeBallLR - intensityStepL >= Constants.LEyeBallLR_MAX_AU61 ) // register value is below its maximum for AU61 
					LEyeBallLR -= intensityStepL;
				//else
				//	LEyeBallLR = Constants.LEyeBallLR_MAX_AU61; // Prevent passing the maximum intensity

				//if( REyeBallLR - intensityStepR >= Constants.REyeBallLR_MAX_AU61 ) // register value is below its maximum for AU61 
					REyeBallLR -= intensityStepR;
				//else
				//	REyeBallLR = Constants.REyeBallLR_MAX_AU61; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && LEyeBallLREnd < LEyeBallLRStart )
			{
				//if( LEyeBallLR - intensityStepL >= Constants.LEyeBallLR_MAX_AU61 ) // register value is below its maximum for AU61 
					LEyeBallLR -= intensityStepL;
				//else
				//	LEyeBallLR = Constants.LEyeBallLR_MAX_AU61; // Prevent passing the maximum intensity
			}
			else if( REyeBallLREnd < REyeBallLRStart )// Right
			{
				//if( REyeBallLR - intensityStepR >= Constants.REyeBallLR_MAX_AU61 ) // register value is below its maximum for AU61  
					REyeBallLR -= intensityStepR;
				//else
				//	REyeBallLR = Constants.REyeBallLR_MAX_AU61; // Prevent passing the maximum intensity
			}

			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallLR   f0= " + LEyeBallLR + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= REyeBallLR   f0= " + REyeBallLR + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallLR   f0= " + LEyeBallLR + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= REyeBallLR   f0= " + REyeBallLR + "]" );
			}

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU62 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU62 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU62 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU62.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU62.
		/// </param>
		/// <param name="side"> 
		///     Side of AU62 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU62 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU62Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double LEyeBallLRStart = convertIntensityToNumber( Constants.LEyeBallLR_MAX_AU62, startIntensity );
			double LEyeBallLREnd = convertIntensityToNumber( Constants.LEyeBallLR_MAX_AU62, endIntensity );
			double REyeBallLRStart = convertIntensityToNumber( Constants.REyeBallLR_MAX_AU62, startIntensity );
			double REyeBallLREnd = convertIntensityToNumber( Constants.REyeBallLR_MAX_AU62, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepL = Math.Abs( LEyeBallLREnd - LEyeBallLRStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( REyeBallLREnd - REyeBallLRStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU62 registes with the starting values.
			if( startTime == currentTime )
			{
				LEyeBallLR = LEyeBallLRStart; REyeBallLR = REyeBallLRStart;
			}

			if( side == "Bilateral" && LEyeBallLREnd > LEyeBallLRStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( LEyeBallLR + intensityStepL <= 0 ) // register value is above its minimum for AU62  
					LEyeBallLR += intensityStepL;
				//else
				//	LEyeBallLR = 0; // Prevent passing the minimum intensity

				//if( REyeBallLR + intensityStepR <= 0 ) // register value is above its minimum for AU62  
					REyeBallLR += intensityStepR;
				//else
				//	REyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && LEyeBallLREnd > LEyeBallLRStart )
			{
				//if( LEyeBallLR + intensityStepL <= 0 ) // register value is above its minimum for AU62
					LEyeBallLR += intensityStepL;
				//else
				//	LEyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( REyeBallLREnd > REyeBallLRStart )// Right
			{
				//if( REyeBallLR + intensityStepR <= 0 ) // register value is above its minimum for AU62  
					REyeBallLR += intensityStepR;
				//else
				//	REyeBallLR = 0; // Prevent passing the minimum intensity
			}

			else if( side == "Bilateral" && LEyeBallLREnd < LEyeBallLRStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( LEyeBallLR - intensityStepL >= Constants.LEyeBallLR_MAX_AU62 ) // register value is below its maximum for AU62 
					LEyeBallLR -= intensityStepL;
				//else
				//	LEyeBallLR = Constants.LEyeBallLR_MAX_AU62; // Prevent passing the maximum intensity

				//if( REyeBallLR - intensityStepR >= Constants.REyeBallLR_MAX_AU62 ) // register value is below its maximum for AU62 
					REyeBallLR -= intensityStepR;
				//else
				//	REyeBallLR = Constants.REyeBallLR_MAX_AU62; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && LEyeBallLREnd < LEyeBallLRStart )
			{
				//if( LEyeBallLR - intensityStepL >= Constants.LEyeBallLR_MAX_AU62 ) // register value is below its maximum for AU62 
					LEyeBallLR -= intensityStepL;
				//else
				//	LEyeBallLR = Constants.LEyeBallLR_MAX_AU62; // Prevent passing the maximum intensity
			}
			else if( REyeBallLREnd < REyeBallLRStart )// Right
			{
				//if( REyeBallLR - intensityStepR >= Constants.REyeBallLR_MAX_AU62 ) // register value is below its maximum for AU62  
					REyeBallLR -= intensityStepR;
				//else
				//	REyeBallLR = Constants.REyeBallLR_MAX_AU62; // Prevent passing the maximum intensity
			}

			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallLR   f0= " + LEyeBallLR + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= REyeBallLR   f0= " + REyeBallLR + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallLR   f0= " + LEyeBallLR + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= REyeBallLR   f0= " + REyeBallLR + "]" );
			}

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU63 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU63 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU63 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU63.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU63.
		/// </param>
		/// <param name="side"> 
		///     Side of AU63 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU63 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU63Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double LEyeBallUDStart = convertIntensityToNumber( Constants.LEyeBallUD_MAX_AU63, startIntensity );
			double LEyeBallUDEnd = convertIntensityToNumber( Constants.LEyeBallUD_MAX_AU63, endIntensity );
			double REyeBallUDStart = convertIntensityToNumber( Constants.REyeBallUD_MAX_AU63, startIntensity );
			double REyeBallUDEnd = convertIntensityToNumber( Constants.REyeBallUD_MAX_AU63, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepL = Math.Abs( LEyeBallUDEnd - LEyeBallUDStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( REyeBallUDEnd - REyeBallUDStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU62 registes with the starting values.
			if( startTime == currentTime )
			{
				LEyeBallUD = LEyeBallUDStart; REyeBallUD = REyeBallUDStart;
			}

			if( side == "Bilateral" && LEyeBallUDEnd > LEyeBallUDStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( LEyeBallUD + intensityStepL <= 0 ) // register value is above its minimum for AU63  
					LEyeBallUD += intensityStepL;
				//else
				//	LEyeBallUD = 0; // Prevent passing the minimum intensity

				//if( REyeBallUD + intensityStepR <= 0 ) // register value is above its minimum for AU63  
					REyeBallUD += intensityStepR;
				//else
				//	REyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && LEyeBallUDEnd > LEyeBallUDStart )
			{
				//if( LEyeBallUD + intensityStepL <= 0 ) // register value is above its minimum for AU63
					LEyeBallUD += intensityStepL;
				//else
				//	LEyeBallUD = 0; // Prevent passing the minimum intensity
			}
			else if( REyeBallUDEnd > REyeBallUDStart )// Right
			{
				//if( REyeBallUD + intensityStepR <= 0 ) // register value is above its minimum for AU63  
					REyeBallUD += intensityStepR;
				//else
				//	REyeBallUD = 0; // Prevent passing the minimum intensity
			}

			else if( side == "Bilateral" && LEyeBallUDEnd < LEyeBallUDStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( LEyeBallUD - intensityStepL >= Constants.LEyeBallUD_MAX_AU63 ) // register value is below its maximum for AU63 
					LEyeBallUD -= intensityStepL;
				//else
				//	LEyeBallUD = Constants.LEyeBallUD_MAX_AU63; // Prevent passing the maximum intensity

				//if( REyeBallUD - intensityStepR >= Constants.REyeBallUD_MAX_AU63 ) // register value is below its maximum for AU63 
					REyeBallUD -= intensityStepR;
				//else
				//	REyeBallUD = Constants.REyeBallUD_MAX_AU63; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && LEyeBallUDEnd < LEyeBallUDStart )
			{
				//if( LEyeBallUD - intensityStepL >= Constants.LEyeBallUD_MAX_AU63 ) // register value is below its maximum for AU63 
					LEyeBallUD -= intensityStepL;
				//else
				//	LEyeBallUD = Constants.LEyeBallUD_MAX_AU63; // Prevent passing the maximum intensity
			}
			else if( REyeBallUDEnd < REyeBallUDStart )// Right
			{
				//if( REyeBallUD - intensityStepR >= Constants.REyeBallUD_MAX_AU63 ) // register value is below its maximum for AU63  
					REyeBallUD -= intensityStepR;
				//else
				//	REyeBallUD = Constants.REyeBallUD_MAX_AU63; // Prevent passing the maximum intensity
			}

			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallUD   f0= " + LEyeBallUD + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= REyeBallUD   f0= " + REyeBallUD + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallUD   f0= " + LEyeBallUD + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= REyeBallUD   f0= " + REyeBallUD + "]" );
			}

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU64 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU64 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU64 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU64.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU64.
		/// </param>
		/// <param name="side"> 
		///     Side of AU64 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU64 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU64Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double LEyeBallUDStart = convertIntensityToNumber( Constants.LEyeBallUD_MAX_AU64, startIntensity );
			double LEyeBallUDEnd = convertIntensityToNumber( Constants.LEyeBallUD_MAX_AU64, endIntensity );
			double REyeBallUDStart = convertIntensityToNumber( Constants.REyeBallUD_MAX_AU64, startIntensity );
			double REyeBallUDEnd = convertIntensityToNumber( Constants.REyeBallUD_MAX_AU64, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepL = Math.Abs( LEyeBallUDEnd - LEyeBallUDStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( REyeBallUDEnd - REyeBallUDStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU62 registes with the starting values.
			if( startTime == currentTime )
			{
				LEyeBallUD = LEyeBallUDStart; REyeBallUD = REyeBallUDStart;
			}

			if( side == "Bilateral" && LEyeBallUDEnd > LEyeBallUDStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( LEyeBallUD + intensityStepL <= 0 ) // register value is above its minimum for AU64  
					LEyeBallUD += intensityStepL;
				//else
				//	LEyeBallUD = 0; // Prevent passing the minimum intensity

				//if( REyeBallUD + intensityStepR <= 0 ) // register value is above its minimum for AU64  
					REyeBallUD += intensityStepR;
				//else
				//	REyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && LEyeBallUDEnd > LEyeBallUDStart )
			{
				//if( LEyeBallUD + intensityStepL <= 0 ) // register value is above its minimum for AU64
					LEyeBallUD += intensityStepL;
				//else
				//	LEyeBallUD = 0; // Prevent passing the minimum intensity
			}
			else if( REyeBallUDEnd > REyeBallUDStart )// Right
			{
				//if( REyeBallUD + intensityStepR <= 0 ) // register value is above its minimum for AU64  
					REyeBallUD += intensityStepR;
				//else
				//	REyeBallUD = 0; // Prevent passing the minimum intensity
			}

			else if( side == "Bilateral" && LEyeBallUDEnd < LEyeBallUDStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( LEyeBallUD - intensityStepL >= Constants.LEyeBallUD_MAX_AU64 ) // register value is below its maximum for AU64 
					LEyeBallUD -= intensityStepL;
				//else
				//	LEyeBallUD = Constants.LEyeBallUD_MAX_AU64; // Prevent passing the maximum intensity

				//if( REyeBallUD - intensityStepR >= Constants.REyeBallUD_MAX_AU64 ) // register value is below its maximum for AU64 
					REyeBallUD -= intensityStepR;
				//else
				//	REyeBallUD = Constants.REyeBallUD_MAX_AU64; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && LEyeBallUDEnd < LEyeBallUDStart )
			{
				//if( LEyeBallUD - intensityStepL >= Constants.LEyeBallUD_MAX_AU64 ) // register value is below its maximum for AU64 
					LEyeBallUD -= intensityStepL;
				//else
				//	LEyeBallUD = Constants.LEyeBallUD_MAX_AU64; // Prevent passing the maximum intensity
			}
			else if( REyeBallUDEnd < REyeBallUDStart )// Right
			{
				//if( REyeBallUD - intensityStepR >= Constants.REyeBallUD_MAX_AU64 ) // register value is below its maximum for AU64  
					REyeBallUD -= intensityStepR;
				//else
				//	REyeBallUD = Constants.REyeBallUD_MAX_AU64; // Prevent passing the maximum intensity
			}

			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallUD   f0= " + LEyeBallUD + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= REyeBallUD   f0= " + REyeBallUD + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallUD   f0= " + LEyeBallUD + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= REyeBallUD   f0= " + REyeBallUD + "]" );
			}

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU65 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU65 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU65 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU65.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU65.
		/// </param>
		/// <param name="side"> 
		///     Side of AU65 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU65 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU65Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double LEyeBallLRStart = convertIntensityToNumber( Constants.LEyeBallLR_MAX_AU65, startIntensity );
			double LEyeBallLREnd = convertIntensityToNumber( Constants.LEyeBallLR_MAX_AU65, endIntensity );
			double REyeBallLRStart = convertIntensityToNumber( Constants.REyeBallLR_MAX_AU65, startIntensity );
			double REyeBallLREnd = convertIntensityToNumber( Constants.REyeBallLR_MAX_AU65, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepL = Math.Abs( LEyeBallLREnd - LEyeBallLRStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( REyeBallLREnd - REyeBallLRStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU65 registes with the starting values.
			if( startTime == currentTime )
			{
				LEyeBallLR = LEyeBallLRStart; REyeBallLR = REyeBallLRStart;
			}

			if( side == "Bilateral" && REyeBallLREnd > REyeBallLRStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( LEyeBallLR + intensityStepL <= 0 ) // register value is above its minimum for AU65  
					LEyeBallLR -= intensityStepL;
				//else
				//	LEyeBallLR = 0; // Prevent passing the minimum intensity

				//if( REyeBallLR - intensityStepR >= 0 ) // register value is above its minimum for AU65  
					REyeBallLR += intensityStepR;
				//else
				//	REyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Right" && REyeBallLREnd > REyeBallLRStart )// Right
			{
				//if( REyeBallLR - intensityStepR >= 0 ) // register value is above its minimum for AU65  
				REyeBallLR += intensityStepR;
				//else
				//	REyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Bilateral" && REyeBallLREnd < REyeBallLRStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( LEyeBallLR - intensityStepL >= Constants.LEyeBallLR_MAX_AU65 ) // register value is below its maximum for AU65 
				LEyeBallLR += intensityStepL;
				//else
				//	LEyeBallLR = Constants.LEyeBallLR_MAX_AU65; // Prevent passing the maximum intensity

				//if( REyeBallLR + intensityStepR <= Constants.REyeBallLR_MAX_AU65 ) // register value is below its maximum for AU65 
				REyeBallLR -= intensityStepR;
				//else
				//	REyeBallLR = Constants.REyeBallLR_MAX_AU65; // Prevent passing the maximum intensity
			}
			else if( side == "Right" && REyeBallLREnd < REyeBallLRStart )// Right
			{
				//if( REyeBallLR + intensityStepR <= Constants.REyeBallLR_MAX_AU65 ) // register value is below its maximum for AU65  
				REyeBallLR -= intensityStepR;
				//else
				//	REyeBallLR = Constants.REyeBallLR_MAX_AU65; // Prevent passing the maximum intensity
			}
			else if( LEyeBallLREnd > LEyeBallLRStart )
			{
				//if( LEyeBallLR + intensityStepL <= 0 ) // register value is above its minimum for AU65
					LEyeBallLR += intensityStepL;
				//else
				//	LEyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( LEyeBallLREnd < LEyeBallLRStart )
			{
				//if( LEyeBallLR - intensityStepL >= Constants.LEyeBallLR_MAX_AU65 ) // register value is below its maximum for AU65 
					LEyeBallLR -= intensityStepL;
				//else
				//	LEyeBallLR = Constants.LEyeBallLR_MAX_AU65; // Prevent passing the maximum intensity
			}
			

			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallLR   f0= " + LEyeBallLR + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= REyeBallLR   f0= " + REyeBallLR + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallLR   f0= " + LEyeBallLR + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= REyeBallLR   f0= " + REyeBallLR + "]" );
			}

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate AU66 from a starting intensity, to an ending intensity between start time and end time.
		/// </summary>
		/// <param name="startIntensity"> 
		///     Start intensity of AU66 in the animation. 
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of AU66 in the animation .
		/// </param>
		/// <param name="startTime"> 
		///     Starting time of animating AU66.
		/// </param>
		/// <param name="endTime"> 
		///     Ending time of animating AU66.
		/// </param>
		/// <param name="side"> 
		///     Side of AU66 to be animated (i.e., left or right).
		/// </param>
		/// <param name="currentTime"> 
		///     Current time in the whole animation which will be created at the end.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the AU66 animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList AU66Video( string side, string startIntensity, string endIntensity, double startTime, double endTime, double currentTime )
		{
			double LEyeBallLRStart = convertIntensityToNumber( Constants.LEyeBallLR_MAX_AU66, startIntensity );
			double LEyeBallLREnd = convertIntensityToNumber( Constants.LEyeBallLR_MAX_AU66, endIntensity );
			double REyeBallLRStart = convertIntensityToNumber( Constants.REyeBallLR_MAX_AU66, startIntensity );
			double REyeBallLREnd = convertIntensityToNumber( Constants.REyeBallLR_MAX_AU66, endIntensity );
			double sectionDuration = endTime - startTime;
			// Calculating the intensity step which will be added to the current AU registers at each timestamp
			double intensityStepL = Math.Abs( LEyeBallLREnd - LEyeBallLRStart ) * animationDuration / ( sectionDuration * 100 );
			double intensityStepR = Math.Abs( REyeBallLREnd - REyeBallLRStart ) * animationDuration / ( sectionDuration * 100 );

			// If it is the begining of activating the AU, over-write the AU66 registes with the starting values.
			if( startTime == currentTime )
			{
				LEyeBallLR = LEyeBallLRStart; REyeBallLR = REyeBallLRStart;
			}

			if( side == "Bilateral" && LEyeBallLREnd > LEyeBallLRStart ) // Activating the AU from a higher intensity to a lower intensity
			{
				//if( LEyeBallLR - intensityStepL >= 0 ) // register value is above its minimum for AU66  
					LEyeBallLR -= intensityStepL;
				//else
				//	LEyeBallLR = 0; // Prevent passing the minimum intensity

				//if( REyeBallLR + intensityStepR <= 0 ) // register value is above its minimum for AU66  
					REyeBallLR += intensityStepR;
				//else
				//	REyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Left" && LEyeBallLREnd > LEyeBallLRStart )
			{
				//if( LEyeBallLR - intensityStepL >= 0 ) // register value is above its minimum for AU66
					LEyeBallLR -= intensityStepL;
				//else
				//	LEyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( side == "Bilateral" && LEyeBallLREnd < LEyeBallLRStart ) // Activating the AU from a lower intensity to a higher intensity
			{
				//if( LEyeBallLR + intensityStepL <= Constants.LEyeBallLR_MAX_AU66 ) // register value is below its maximum for AU66 
				LEyeBallLR += intensityStepL;
				//else
				//	LEyeBallLR = Constants.LEyeBallLR_MAX_AU66; // Prevent passing the maximum intensity

				//if( REyeBallLR - intensityStepR >= Constants.REyeBallLR_MAX_AU66 ) // register value is below its maximum for AU66 
				REyeBallLR -= intensityStepR;
				//else
				//	REyeBallLR = Constants.REyeBallLR_MAX_AU66; // Prevent passing the maximum intensity
			}
			else if( side == "Left" && LEyeBallLREnd < LEyeBallLRStart )
			{
				//if( LEyeBallLR + intensityStepL <= Constants.LEyeBallLR_MAX_AU66 ) // register value is below its maximum for AU66 
				LEyeBallLR += intensityStepL;
				//else
				//	LEyeBallLR = Constants.LEyeBallLR_MAX_AU66; // Prevent passing the maximum intensity
			}
			else if( REyeBallLREnd > REyeBallLRStart )// Right
			{
				//if( REyeBallLR + intensityStepR <= 0 ) // register value is above its minimum for AU66  
					REyeBallLR += intensityStepR;
				//else
				//	REyeBallLR = 0; // Prevent passing the minimum intensity
			}
			else if( REyeBallLREnd < REyeBallLRStart )// Right
			{
				//if( REyeBallLR - intensityStepR >= Constants.REyeBallLR_MAX_AU66 ) // register value is below its maximum for AU66  
					REyeBallLR -= intensityStepR;
				//else
				//	REyeBallLR = Constants.REyeBallLR_MAX_AU66; // Prevent passing the maximum intensity
			}

			if( side == "Bilateral" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallLR   f0= " + LEyeBallLR + "]" );
				scriptText.Add( "\\clock [t= " + ( currentTime + 0.0015 ) + "]   \\setreg[name= REyeBallLR   f0= " + REyeBallLR + "]" );
			}
			else if( side == "Left" )
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= LEyeBallLR   f0= " + LEyeBallLR + "]" );
			}
			else // Right
			{
				scriptText.Add( "\\clock [t= " + currentTime + "]   \\setreg[name= REyeBallLR   f0= " + REyeBallLR + "]" );
			}

			return scriptText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hypertext to be added to the Haptek animation file in order to animate speaking at a start time.
		/// </summary>
		/// <param name="startTime"> 
		///     Starting time of animating speaking.
		/// </param>
		/// <param name="textToSpeak"> 
		///     Text to be spoken by the character at the given time.
		/// </param>
		/// <returns>
		///     The Haptek hypertext including the speaking animation until the current time.
		/// </returns>
		/// <remarks></remarks>
		public ArrayList SpeakVideo( double startTime, string textToSpeak )
		{
			scriptText.Add( "\\clock [t= " + startTime + "]   \\Q2[s0= [" + textToSpeak + "]]" );

			return scriptText;
		}

		#endregion
	}
}
