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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HapFACS
{
	/// <author>
	///     Reza Amini (ramin001@fiu.edu) 
	///     Affective Social Computing lab at Florida International University
	/// </author>
	/// <summary>
	///     HapFACS object simulate all the Facial Action Coding System Action Units. It includes all the methods needed to manipulate different AUs with different intensities.
	/// </summary>
	/// <remarks></remarks>
	class HapFACS
	{
		#region Local variables

		private double MidBrowUD = 0;
		private double LBrowUD = 0;
		private double RBrowUD = 0;
		private double eyes_sad = 0;
		private double lipcornerL3ty = 0;
		private double lipcornerR3ty = 0;
		private double trust = 0;
		private double distrust = 0;
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
		private double blink = 0;
		private string blinks = "";
		private double HeadTwist = 0;
		private double HeadForward = 0;
		private double HeadSideBend = 0;
		private double NeckForward = 0;
		private double LEyeBallLR = 0;
		private double REyeBallLR = 0;
		private double LEyeBallUD = 0;
		private double REyeBallUD = 0;

		#endregion

		#region Methods to activate and reset Action Units
		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 1 (Inner Brow Raiser) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 1 (Inner Brow Raiser) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU1( string intensity, string lastIntensity )
		{
			double MidBrowUDChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU1 / Constants.AIntensity;
					break;
				case ( "B" ):
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU1 / Constants.BIntensity;
					break;
				case ( "C" ):
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU1 / Constants.CIntensity;
					break;
				case ( "D" ):
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU1 / Constants.DIntensity;
					break;
				case ( "E" ):
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU1;
					break;
				case ( "0" ):
					break;
				default:
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU1 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU1 / Constants.AIntensity;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU1 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU1 / Constants.BIntensity;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU1 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU1 / Constants.CIntensity;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU1 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU1 / Constants.DIntensity;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU1 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU1;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU1 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= MidBrowUD f=0] ";
					break;
				default:
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU1 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU1 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			MidBrowUD += MidBrowUDChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 2 (Outer Brow Raiser) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 2 (Outer Brow Raiser) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU2( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double LBrowUDChange = 0;
			double RBrowUDChange = 0;
			string hyperText = "";
			switch( lastLeftIntensity )
			{
				case ( "A" ):
					LBrowUDChange = -Constants.LBrowUD_MAX_AU2 / Constants.AIntensity;
					break;
				case ( "B" ):
					LBrowUDChange = -Constants.LBrowUD_MAX_AU2 / Constants.BIntensity;
					break;
				case ( "C" ):
					LBrowUDChange = -Constants.LBrowUD_MAX_AU2 / Constants.CIntensity;
					break;
				case ( "D" ):
					LBrowUDChange = -Constants.LBrowUD_MAX_AU2 / Constants.DIntensity;
					break;
				case ( "E" ):
					LBrowUDChange = -Constants.LBrowUD_MAX_AU2;
					break;
				case ( "0" ):
					break;
				default:
					LBrowUDChange = -Constants.LBrowUD_MAX_AU2 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					RBrowUDChange = -Constants.RBrowUD_MAX_AU2 / Constants.AIntensity;
					break;
				case ( "B" ):
					RBrowUDChange = -Constants.RBrowUD_MAX_AU2 / Constants.BIntensity;
					break;
				case ( "C" ):
					RBrowUDChange = -Constants.RBrowUD_MAX_AU2 / Constants.CIntensity;
					break;
				case ( "D" ):
					RBrowUDChange = -Constants.RBrowUD_MAX_AU2 / Constants.DIntensity;
					break;
				case ( "E" ):
					RBrowUDChange = -Constants.RBrowUD_MAX_AU2;
					break;
				case ( "0" ):
					break;
				default:
					RBrowUDChange = -Constants.RBrowUD_MAX_AU2 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					LBrowUDChange += Constants.LBrowUD_MAX_AU2 / Constants.AIntensity;
					hyperText = @"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU2 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					LBrowUDChange += Constants.LBrowUD_MAX_AU2 / Constants.BIntensity;
					hyperText = @"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU2 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					LBrowUDChange += Constants.LBrowUD_MAX_AU2 / Constants.CIntensity;
					hyperText = @"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU2 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					LBrowUDChange += Constants.LBrowUD_MAX_AU2 / Constants.DIntensity;
					hyperText = @"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU2 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					LBrowUDChange += Constants.LBrowUD_MAX_AU2;
					hyperText = @"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU2 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= LBrowUD f=0] ";
					break;
				default:
					LBrowUDChange += Constants.LBrowUD_MAX_AU2 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU2 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					RBrowUDChange += Constants.RBrowUD_MAX_AU2 / Constants.AIntensity;
					hyperText += @"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU2 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					RBrowUDChange += Constants.RBrowUD_MAX_AU2 / Constants.BIntensity;
					hyperText += @"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU2 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					RBrowUDChange += Constants.RBrowUD_MAX_AU2 / Constants.CIntensity;
					hyperText += @"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU2 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					RBrowUDChange += Constants.RBrowUD_MAX_AU2 / Constants.DIntensity;
					hyperText += @"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU2 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					RBrowUDChange += Constants.RBrowUD_MAX_AU2;
					hyperText += @"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU2 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= RBrowUD f=0] ";
					break;
				default:
					RBrowUDChange += Constants.RBrowUD_MAX_AU2 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU2 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			LBrowUD += LBrowUDChange;
			RBrowUD += RBrowUDChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 4 (Brow Lowerer) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 4 (Brow Lowerer) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU4( string intensity, string lastIntensity )
		{
			double eyes_sadChange = 0;
			double MidBrowUDChange = 0;
			double RBrowUDChange = 0;
			double LBrowUDChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					eyes_sadChange = -Constants.eyes_sad_MAX_AU4 / Constants.AIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU4 / Constants.AIntensity;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU4 / Constants.AIntensity;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU4 / Constants.AIntensity;
					break;
				case ( "B" ):
					eyes_sadChange = -Constants.eyes_sad_MAX_AU4 / Constants.BIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU4 / Constants.BIntensity;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU4 / Constants.BIntensity;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU4 / Constants.BIntensity;
					break;
				case ( "C" ):
					eyes_sadChange = -Constants.eyes_sad_MAX_AU4 / Constants.CIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU4 / Constants.CIntensity;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU4 / Constants.CIntensity;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU4 / Constants.CIntensity;
					break;
				case ( "D" ):
					eyes_sadChange = -Constants.eyes_sad_MAX_AU4 / Constants.DIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU4 / Constants.DIntensity;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU4 / Constants.DIntensity;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU4 / Constants.DIntensity;
					break;
				case ( "E" ):
					eyes_sadChange = -Constants.eyes_sad_MAX_AU4;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU4;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU4;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU4;
					break;
				case ( "0" ):
					break;
				default:
					eyes_sadChange = -Constants.eyes_sad_MAX_AU4 * Convert.ToDouble( lastIntensity ) / 100;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU4 * Convert.ToDouble( lastIntensity ) / 100;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU4 * Convert.ToDouble( lastIntensity ) / 100;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU4 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					eyes_sadChange += Constants.eyes_sad_MAX_AU4 / Constants.AIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU4 / Constants.AIntensity;
					RBrowUDChange += Constants.RBrowUD_MAX_AU4 / Constants.AIntensity;
					LBrowUDChange += Constants.LBrowUD_MAX_AU4 / Constants.AIntensity;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU4 / Constants.AIntensity + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU4 / Constants.AIntensity + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU4 / Constants.AIntensity + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU4 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					eyes_sadChange += Constants.eyes_sad_MAX_AU4 / Constants.BIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU4 / Constants.BIntensity;
					RBrowUDChange += Constants.RBrowUD_MAX_AU4 / Constants.BIntensity;
					LBrowUDChange += Constants.LBrowUD_MAX_AU4 / Constants.BIntensity;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU4 / Constants.BIntensity + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU4 / Constants.BIntensity + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU4 / Constants.BIntensity + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU4 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					eyes_sadChange += Constants.eyes_sad_MAX_AU4 / Constants.CIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU4 / Constants.CIntensity;
					RBrowUDChange += Constants.RBrowUD_MAX_AU4 / Constants.CIntensity;
					LBrowUDChange += Constants.LBrowUD_MAX_AU4 / Constants.CIntensity;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU4 / Constants.CIntensity + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU4 / Constants.CIntensity + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU4 / Constants.CIntensity + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU4 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					eyes_sadChange += Constants.eyes_sad_MAX_AU4 / Constants.DIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU4 / Constants.DIntensity;
					RBrowUDChange += Constants.RBrowUD_MAX_AU4 / Constants.DIntensity;
					LBrowUDChange += Constants.LBrowUD_MAX_AU4 / Constants.DIntensity;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU4 / Constants.DIntensity + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU4 / Constants.DIntensity + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU4 / Constants.DIntensity + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU4 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					eyes_sadChange += Constants.eyes_sad_MAX_AU4;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU4;
					RBrowUDChange += Constants.RBrowUD_MAX_AU4;
					LBrowUDChange += Constants.LBrowUD_MAX_AU4;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU4 + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU4 + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU4 + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU4 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= eyes_sad f=0] " +
								@"\SetReg[name= MidBrowUD f=0] " +
								@"\SetReg[name= RBrowUD f=0] " +
								@"\SetReg[name= LBrowUD f=0] ";
					break;
				default:
					eyes_sadChange += Constants.eyes_sad_MAX_AU4 * Convert.ToDouble( intensity ) / 100;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU4 * Convert.ToDouble( intensity ) / 100;
					RBrowUDChange += Constants.RBrowUD_MAX_AU4 * Convert.ToDouble( intensity ) / 100;
					LBrowUDChange += Constants.LBrowUD_MAX_AU4 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU4 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU4 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU4 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU4 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			eyes_sad += eyes_sadChange;
			MidBrowUD += MidBrowUDChange;
			RBrowUD += RBrowUDChange;
			LBrowUD += LBrowUDChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 5 (Upper Lid Raiser) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 5 (Upper Lid Raiser) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU5( string intensity, string lastIntensity )
		{
			double trustChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					trustChange = -Constants.trust_MAX_AU5 / Constants.AIntensity;
					break;
				case ( "B" ):
					trustChange = -Constants.trust_MAX_AU5 / Constants.BIntensity;
					break;
				case ( "C" ):
					trustChange = -Constants.trust_MAX_AU5 / Constants.CIntensity;
					break;
				case ( "D" ):
					trustChange = -Constants.trust_MAX_AU5 / Constants.DIntensity;
					break;
				case ( "E" ):
					trustChange = -Constants.trust_MAX_AU5;
					break;
				case ( "0" ):
					break;
				default:
					trustChange = -Constants.trust_MAX_AU5 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					trustChange += Constants.trust_MAX_AU5 / Constants.AIntensity;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU5 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					trustChange += Constants.trust_MAX_AU5 / Constants.BIntensity;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU5 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					trustChange += Constants.trust_MAX_AU5 / Constants.CIntensity;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU5 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					trustChange += Constants.trust_MAX_AU5 / Constants.DIntensity;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU5 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					trustChange += Constants.trust_MAX_AU5;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU5 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= trust f=0] ";
					break;
				default:
					trustChange += Constants.trust_MAX_AU5 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU5 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}
			trust += trustChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets Action Unit 5 (Upper Lid Raiser) on the Haptek face.
		/// </summary>
		/// <returns>
		///     Hypertext to reset Action Unit 5 (Upper Lid Raiser) on the Haptek face.
		/// </returns>
		/// <remarks></remarks>
		public string resetAU5( )
		{
			trust = 0;
			return @"\SetReg[name= trust f=0] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 6 (Cheek Raiser and Lid Compressor) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 6 (Cheek Raiser and Lid Compressor) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU6( string intensity, string lastIntensity )
		{
			double lipcornerL3tyChange = 0;
			double lipcornerR3tyChange = 0;
			double smile3Change = 0;
			double kissChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU6 / Constants.AIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU6 / Constants.AIntensity;
					smile3Change = -Constants.smile3_MAX_AU6 / Constants.AIntensity;
					kissChange = -Constants.kiss_MAX_AU6 / Constants.AIntensity;
					break;
				case ( "B" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU6 / Constants.BIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU6 / Constants.BIntensity;
					smile3Change = -Constants.smile3_MAX_AU6 / Constants.BIntensity;
					kissChange = -Constants.kiss_MAX_AU6 / Constants.BIntensity;
					break;
				case ( "C" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU6 / Constants.CIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU6 / Constants.CIntensity;
					smile3Change = -Constants.smile3_MAX_AU6 / Constants.CIntensity;
					kissChange = -Constants.kiss_MAX_AU6 / Constants.CIntensity;
					break;
				case ( "D" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU6 / Constants.DIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU6 / Constants.DIntensity;
					smile3Change = -Constants.smile3_MAX_AU6 / Constants.DIntensity;
					kissChange = -Constants.kiss_MAX_AU6 / Constants.DIntensity;
					break;
				case ( "E" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU6;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU6;
					smile3Change = -Constants.smile3_MAX_AU6;
					kissChange = -Constants.kiss_MAX_AU6;
					break;
				case ( "0" ):
					break;
				default:
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU6 * Convert.ToDouble( lastIntensity ) / 100;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU6 * Convert.ToDouble( lastIntensity ) / 100;
					smile3Change = -Constants.smile3_MAX_AU6 * Convert.ToDouble( lastIntensity ) / 100;
					kissChange = -Constants.kiss_MAX_AU6 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU6 / Constants.AIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU6 / Constants.AIntensity;
					smile3Change += Constants.smile3_MAX_AU6 / Constants.AIntensity;
					kissChange += Constants.kiss_MAX_AU6 / Constants.AIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU6 / Constants.AIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU6 / Constants.AIntensity + "] " +
								@"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU6 / Constants.AIntensity + "] " +
								@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU6 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU6 / Constants.BIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU6 / Constants.BIntensity;
					smile3Change += Constants.smile3_MAX_AU6 / Constants.BIntensity;
					kissChange += Constants.kiss_MAX_AU6 / Constants.BIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU6 / Constants.BIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU6 / Constants.BIntensity + "] " +
								@"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU6 / Constants.BIntensity + "] " +
								@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU6 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU6 / Constants.CIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU6 / Constants.CIntensity;
					smile3Change += Constants.smile3_MAX_AU6 / Constants.CIntensity;
					kissChange += Constants.kiss_MAX_AU6 / Constants.CIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU6 / Constants.CIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU6 / Constants.CIntensity + "] " +
								@"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU6 / Constants.CIntensity + "] " +
								@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU6 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU6 / Constants.DIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU6 / Constants.DIntensity;
					smile3Change += Constants.smile3_MAX_AU6 / Constants.DIntensity;
					kissChange += Constants.kiss_MAX_AU6 / Constants.DIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU6 / Constants.DIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU6 / Constants.DIntensity + "] " +
								@"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU6 / Constants.DIntensity + "] " +
								@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU6 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU6;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU6;
					smile3Change += Constants.smile3_MAX_AU6;
					kissChange += Constants.kiss_MAX_AU6;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU6 + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU6 + "] " +
								@"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU6 + "] " +
								@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU6 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= lipcornerL3ty f=0] " +
								@"\SetReg[name= lipcornerR3ty f=0] " +
								@"\SetReg[name= smile3 f=0] " +
								@"\SetReg[name= kiss f=0] ";
					break;
				default:
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU6 * Convert.ToDouble( intensity ) / 100;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU6 * Convert.ToDouble( intensity ) / 100;
					smile3Change += Constants.smile3_MAX_AU6 * Convert.ToDouble( intensity ) / 100;
					kissChange += Constants.kiss_MAX_AU6 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU6 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU6 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU6 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU6 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			lipcornerL3ty += lipcornerL3tyChange;
			lipcornerR3ty += lipcornerR3tyChange;
			smile3 += smile3Change;
			kiss += kissChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 7 (Lid Tightener) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 7 (Lid Tightener) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU7( string intensity, string lastIntensity )
		{
			double distrustChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					distrustChange = -Constants.distrust_MAX_AU7 / Constants.AIntensity;
					break;
				case ( "B" ):
					distrustChange = -Constants.distrust_MAX_AU7 / Constants.BIntensity;
					break;
				case ( "C" ):
					distrustChange = -Constants.distrust_MAX_AU7 / Constants.CIntensity;
					break;
				case ( "D" ):
					distrustChange = -Constants.distrust_MAX_AU7 / Constants.DIntensity;
					break;
				case ( "E" ):
					distrustChange = -Constants.distrust_MAX_AU7;
					break;
				case ( "0" ):
					break;
				default:
					distrustChange = -Constants.distrust_MAX_AU7 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					distrustChange += Constants.distrust_MAX_AU7 / Constants.AIntensity;
					hyperText = @"\SetReg[name= distrust f=" + Constants.distrust_MAX_AU7 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					distrustChange += Constants.distrust_MAX_AU7 / Constants.BIntensity;
					hyperText = @"\SetReg[name= distrust f=" + Constants.distrust_MAX_AU7 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					distrustChange += Constants.distrust_MAX_AU7 / Constants.CIntensity;
					hyperText = @"\SetReg[name= distrust f=" + Constants.distrust_MAX_AU7 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					distrustChange += Constants.distrust_MAX_AU7 / Constants.DIntensity;
					hyperText = @"\SetReg[name= distrust f=" + Constants.distrust_MAX_AU7 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					distrustChange += Constants.distrust_MAX_AU7;
					hyperText = @"\SetReg[name= distrust f=" + Constants.distrust_MAX_AU7 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= distrust f=0] ";
					break;
				default:
					distrustChange += Constants.distrust_MAX_AU7 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= distrust f=" + Constants.distrust_MAX_AU7 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			distrust += distrustChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 8 (Lips Toward Each Other) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 8 (Lips Toward Each Other) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU8( string intensity, string lastIntensity )
		{
			double bChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					bChange = -Constants.b_MAX_AU8 / Constants.AIntensity;
					break;
				case ( "B" ):
					bChange = -Constants.b_MAX_AU8 / Constants.BIntensity;
					break;
				case ( "C" ):
					bChange = -Constants.b_MAX_AU8 / Constants.CIntensity;
					break;
				case ( "D" ):
					bChange = -Constants.b_MAX_AU8 / Constants.DIntensity;
					break;
				case ( "E" ):
					bChange = -Constants.b_MAX_AU8;
					break;
				case ( "0" ):
					break;
				default:
					bChange = -Constants.b_MAX_AU8 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					bChange += Constants.b_MAX_AU8 / Constants.AIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU8 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					bChange += Constants.b_MAX_AU8 / Constants.BIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU8 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					bChange += Constants.b_MAX_AU8 / Constants.CIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU8 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					bChange += Constants.b_MAX_AU8 / Constants.DIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU8 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					bChange += Constants.b_MAX_AU8;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU8 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= b f=0] ";
					break;
				default:
					bChange += Constants.b_MAX_AU8 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU8 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			b += bChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 9 (Nose Wrinkler) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 9 (Nose Wrinkler) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU9( string intensity, string lastIntensity )
		{
			double nostrilL3tyChange = 0;
			double nostrilR3tyChange = 0;
			double nostrilL3txChange = 0;
			double nostrilR3txChange = 0;
			double MidBrowUDChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU9 / Constants.AIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU9 / Constants.AIntensity;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU9 / Constants.AIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU9 / Constants.AIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU9 / Constants.AIntensity;
					break;
				case ( "B" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU9 / Constants.BIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU9 / Constants.BIntensity;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU9 / Constants.BIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU9 / Constants.BIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU9 / Constants.BIntensity;
					break;
				case ( "C" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU9 / Constants.CIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU9 / Constants.CIntensity;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU9 / Constants.CIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU9 / Constants.CIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU9 / Constants.CIntensity;
					break;
				case ( "D" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU9 / Constants.DIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU9 / Constants.DIntensity;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU9 / Constants.DIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU9 / Constants.DIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU9 / Constants.DIntensity;
					break;
				case ( "E" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU9;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU9;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU9;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU9;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU9;
					break;
				case ( "0" ):
					break;
				default:
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU9 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU9 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU9 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU9 * Convert.ToDouble( lastIntensity ) / 100;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU9 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}

			switch( intensity )
			{
				case ( "A" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU9 / Constants.AIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU9 / Constants.AIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU9 / Constants.AIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU9 / Constants.AIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU9 / Constants.AIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU9 / Constants.AIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU9 / Constants.AIntensity + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU9 / Constants.AIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU9 / Constants.AIntensity + "] " +
								 @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU9 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU9 / Constants.BIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU9 / Constants.BIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU9 / Constants.BIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU9 / Constants.BIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU9 / Constants.BIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU9 / Constants.BIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU9 / Constants.BIntensity + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU9 / Constants.BIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU9 / Constants.BIntensity + "] " +
								 @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU9 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU9 / Constants.CIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU9 / Constants.CIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU9 / Constants.CIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU9 / Constants.CIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU9 / Constants.CIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU9 / Constants.CIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU9 / Constants.CIntensity + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU9 / Constants.CIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU9 / Constants.CIntensity + "] " +
								 @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU9 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU9 / Constants.DIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU9 / Constants.DIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU9 / Constants.DIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU9 / Constants.DIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU9 / Constants.DIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU9 / Constants.DIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU9 / Constants.DIntensity + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU9 / Constants.DIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU9 / Constants.DIntensity + "] " +
								 @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU9 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU9;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU9;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU9;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU9;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU9;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU9 + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU9 + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU9 + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU9 + "] " +
								 @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU9 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= nostrilR3ty f=0] " +
								 @"\SetReg[name= nostrilR3tx f=0] " +
								 @"\SetReg[name= nostrilL3ty f=0] " +
								 @"\SetReg[name= nostrilL3tx f=0] " +
								 @"\SetReg[name= MidBrowUD f=0] ";
					break;
				default:
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU9 * Convert.ToDouble( intensity ) / 100;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU9 * Convert.ToDouble( intensity ) / 100;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU9 * Convert.ToDouble( intensity ) / 100;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU9 * Convert.ToDouble( intensity ) / 100;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU9 * Convert.ToDouble( intensity ) / 100;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU9 * Convert.ToDouble( intensity ) / 100 + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU9 * Convert.ToDouble( intensity ) / 100 + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU9 * Convert.ToDouble( intensity ) / 100 + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU9 * Convert.ToDouble( intensity ) / 100 + "] " +
								 @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU9 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			nostrilL3ty += nostrilL3tyChange;
			nostrilR3ty += nostrilR3tyChange;
			nostrilL3tx += nostrilL3txChange;
			nostrilR3tx += nostrilR3txChange;
			MidBrowUD += MidBrowUDChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 10 (Upper Lip Raiser) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 10 (Upper Lip Raiser) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU10( string intensity, string lastIntensity )
		{
			double uhChange = 0;
			double dChange = 0;
			double nostrilL3tyChange = 0;
			double nostrilR3tyChange = 0;
			double nostrilL3txChange = 0;
			double nostrilR3txChange = 0;
			double owChange = 0;
			double iyChange = 0;
			string hyperText = "";

			switch( lastIntensity )
			{
				case ( "A" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU10 / Constants.AIntensity;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU10 / Constants.AIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU10 / Constants.AIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU10 / Constants.AIntensity;
					uhChange = -Constants.uh_MAX_AU10 / Constants.AIntensity;
					dChange = -Constants.d_MAX_AU10 / Constants.AIntensity;
					owChange = -Constants.ow_MAX_AU10 / Constants.AIntensity;
					iyChange = -Constants.iy_MAX_AU10 / Constants.AIntensity;
					break;
				case ( "B" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU10 / Constants.BIntensity;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU10 / Constants.BIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU10 / Constants.BIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU10 / Constants.BIntensity;
					uhChange = -Constants.uh_MAX_AU10 / Constants.BIntensity;
					dChange = -Constants.d_MAX_AU10 / Constants.BIntensity;
					owChange = -Constants.ow_MAX_AU10 / Constants.BIntensity;
					iyChange = -Constants.iy_MAX_AU10 / Constants.BIntensity;
					break;
				case ( "C" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU10 / Constants.CIntensity;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU10 / Constants.CIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU10 / Constants.CIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU10 / Constants.CIntensity;
					uhChange = -Constants.uh_MAX_AU10 / Constants.CIntensity;
					dChange = -Constants.d_MAX_AU10 / Constants.CIntensity;
					owChange = -Constants.ow_MAX_AU10 / Constants.CIntensity;
					iyChange = -Constants.iy_MAX_AU10 / Constants.CIntensity;
					break;
				case ( "D" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU10 / Constants.DIntensity;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU10 / Constants.DIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU10 / Constants.DIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU10 / Constants.DIntensity;
					uhChange = -Constants.uh_MAX_AU10 / Constants.DIntensity;
					dChange = -Constants.d_MAX_AU10 / Constants.DIntensity;
					owChange = -Constants.ow_MAX_AU10 / Constants.DIntensity;
					iyChange = -Constants.iy_MAX_AU10 / Constants.DIntensity;
					break;
				case ( "E" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU10;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU10;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU10;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU10;
					uhChange = -Constants.uh_MAX_AU10;
					dChange = -Constants.d_MAX_AU10;
					owChange = -Constants.ow_MAX_AU10;
					iyChange = -Constants.iy_MAX_AU10;
					break;
				case ( "0" ):
					break;
				default:
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU10 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU10 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU10 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU10 * Convert.ToDouble( lastIntensity ) / 100;
					uhChange = -Constants.uh_MAX_AU10 * Convert.ToDouble( lastIntensity ) / 100;
					dChange = -Constants.d_MAX_AU10 * Convert.ToDouble( lastIntensity ) / 100;
					owChange = -Constants.ow_MAX_AU10 * Convert.ToDouble( lastIntensity ) / 100;
					iyChange = -Constants.iy_MAX_AU10 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU10 / Constants.AIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU10 / Constants.AIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU10 / Constants.AIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU10 / Constants.AIntensity;
					uhChange += Constants.uh_MAX_AU10 / Constants.AIntensity;
					dChange += Constants.d_MAX_AU10 / Constants.AIntensity;
					owChange += Constants.ow_MAX_AU10 / Constants.AIntensity;
					iyChange += Constants.iy_MAX_AU10 / Constants.AIntensity;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU10 / Constants.AIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU10 / Constants.AIntensity + "] " +
								@"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU10 / Constants.AIntensity + "] " +
								@"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU10 / Constants.AIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU10 / Constants.AIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU10 / Constants.AIntensity + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU10 / Constants.AIntensity + "] " +
								@"\SetReg[name= iy f=" + Constants.iy_MAX_AU10 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU10 / Constants.BIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU10 / Constants.BIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU10 / Constants.BIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU10 / Constants.BIntensity;
					uhChange += Constants.uh_MAX_AU10 / Constants.BIntensity;
					dChange += Constants.d_MAX_AU10 / Constants.BIntensity;
					owChange += Constants.ow_MAX_AU10 / Constants.BIntensity;
					iyChange += Constants.iy_MAX_AU10 / Constants.BIntensity;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU10 / Constants.BIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU10 / Constants.BIntensity + "] " +
								@"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU10 / Constants.BIntensity + "] " +
								@"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU10 / Constants.BIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU10 / Constants.BIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU10 / Constants.BIntensity + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU10 / Constants.BIntensity + "] " +
								@"\SetReg[name= iy f=" + Constants.iy_MAX_AU10 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU10 / Constants.CIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU10 / Constants.CIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU10 / Constants.CIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU10 / Constants.CIntensity;
					uhChange += Constants.uh_MAX_AU10 / Constants.CIntensity;
					dChange += Constants.d_MAX_AU10 / Constants.CIntensity;
					owChange += Constants.ow_MAX_AU10 / Constants.CIntensity;
					iyChange += Constants.iy_MAX_AU10 / Constants.CIntensity;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU10 / Constants.CIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU10 / Constants.CIntensity + "] " +
								@"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU10 / Constants.CIntensity + "] " +
								@"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU10 / Constants.CIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU10 / Constants.CIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU10 / Constants.CIntensity + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU10 / Constants.CIntensity + "] " +
								@"\SetReg[name= iy f=" + Constants.iy_MAX_AU10 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU10 / Constants.DIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU10 / Constants.DIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU10 / Constants.DIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU10 / Constants.DIntensity;
					uhChange += Constants.uh_MAX_AU10 / Constants.DIntensity;
					dChange += Constants.d_MAX_AU10 / Constants.DIntensity;
					owChange += Constants.ow_MAX_AU10 / Constants.DIntensity;
					iyChange += Constants.iy_MAX_AU10 / Constants.DIntensity;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU10 / Constants.DIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU10 / Constants.DIntensity + "] " +
								@"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU10 / Constants.DIntensity + "] " +
								@"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU10 / Constants.DIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU10 / Constants.DIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU10 / Constants.DIntensity + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU10 / Constants.DIntensity + "] " +
								@"\SetReg[name= iy f=" + Constants.iy_MAX_AU10 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU10;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU10;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU10;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU10;
					uhChange += Constants.uh_MAX_AU10;
					dChange += Constants.d_MAX_AU10;
					owChange += Constants.ow_MAX_AU10;
					iyChange += Constants.iy_MAX_AU10;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU10 + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU10 + "] " +
								@"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU10 + "] " +
								@"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU10 + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU10 + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU10 + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU10 + "] " +
								@"\SetReg[name= iy f=" + Constants.iy_MAX_AU10 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= uh f=0] " +
								@"\SetReg[name= d f=0] " +
								@"\SetReg[name= nostrilL3ty f=0] " +
								@"\SetReg[name= nostrilR3ty f=0] " +
								@"\SetReg[name= nostrilL3tx f=0] " +
								@"\SetReg[name= nostrilR3tx f=0] " +
								@"\SetReg[name= ow f=0] " +
								@"\SetReg[name= iy f=0] ";
					break;
				default:
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU10 * Convert.ToDouble( intensity ) / 100;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU10 * Convert.ToDouble( intensity ) / 100;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU10 * Convert.ToDouble( intensity ) / 100;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU10 * Convert.ToDouble( intensity ) / 100;
					uhChange += Constants.uh_MAX_AU10 * Convert.ToDouble( intensity ) / 100;
					dChange += Constants.d_MAX_AU10 * Convert.ToDouble( intensity ) / 100;
					owChange += Constants.ow_MAX_AU10 * Convert.ToDouble( intensity ) / 100;
					iyChange += Constants.iy_MAX_AU10 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU10 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU10 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU10 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU10 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU10 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU10 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU10 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= iy f=" + Constants.iy_MAX_AU10 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			nostrilL3ty += nostrilL3tyChange;
			nostrilR3ty += nostrilR3tyChange;
			nostrilL3tx += nostrilL3txChange;
			nostrilR3tx += nostrilR3txChange;
			uh += uhChange;
			d += dChange;
			ow += owChange;
			iy += iyChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 11 (Nasolabial Furrow Deepener) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 11 (Nasolabial Furrow Deepener) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU11( string intensity, string lastIntensity )
		{
			double uhChange = 0;
			double dChange = 0;
			double nostrilL3txChange = 0;
			double nostrilR3txChange = 0;
			string hyperText = "";

			switch( lastIntensity )
			{
				case ( "A" ):
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU11 / Constants.AIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU11 / Constants.AIntensity;
					uhChange = -Constants.uh_MAX_AU11 / Constants.AIntensity;
					dChange = -Constants.d_MAX_AU11 / Constants.AIntensity;
					break;
				case ( "B" ):
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU11 / Constants.BIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU11 / Constants.BIntensity;
					uhChange = -Constants.uh_MAX_AU11 / Constants.BIntensity;
					dChange = -Constants.d_MAX_AU11 / Constants.BIntensity;
					break;
				case ( "C" ):
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU11 / Constants.CIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU11 / Constants.CIntensity;
					uhChange = -Constants.uh_MAX_AU11 / Constants.CIntensity;
					dChange = -Constants.d_MAX_AU11 / Constants.CIntensity;
					break;
				case ( "D" ):
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU11 / Constants.DIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU11 / Constants.DIntensity;
					uhChange = -Constants.uh_MAX_AU11 / Constants.DIntensity;
					dChange = -Constants.d_MAX_AU11 / Constants.DIntensity;
					break;
				case ( "E" ):
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU11;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU11;
					uhChange = -Constants.uh_MAX_AU11;
					dChange = -Constants.d_MAX_AU11;
					break;
				case ( "0" ):
					break;
				default:
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU11 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU11 * Convert.ToDouble( lastIntensity ) / 100;
					uhChange = -Constants.uh_MAX_AU11 * Convert.ToDouble( lastIntensity ) / 100;
					dChange = -Constants.d_MAX_AU11 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU11 / Constants.AIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU11 / Constants.AIntensity;
					uhChange += Constants.uh_MAX_AU11 / Constants.AIntensity;
					dChange += Constants.d_MAX_AU11 / Constants.AIntensity;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU11 / Constants.AIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU11 / Constants.AIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU11 / Constants.AIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU11 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU11 / Constants.BIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU11 / Constants.BIntensity;
					uhChange += Constants.uh_MAX_AU11 / Constants.BIntensity;
					dChange += Constants.d_MAX_AU11 / Constants.BIntensity;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU11 / Constants.BIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU11 / Constants.BIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU11 / Constants.BIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU11 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU11 / Constants.CIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU11 / Constants.CIntensity;
					uhChange += Constants.uh_MAX_AU11 / Constants.CIntensity;
					dChange += Constants.d_MAX_AU11 / Constants.CIntensity;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU11 / Constants.CIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU11 / Constants.CIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU11 / Constants.CIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU11 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU11 / Constants.DIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU11 / Constants.DIntensity;
					uhChange += Constants.uh_MAX_AU11 / Constants.DIntensity;
					dChange += Constants.d_MAX_AU11 / Constants.DIntensity;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU11 / Constants.DIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU11 / Constants.DIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU11 / Constants.DIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU11 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU11;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU11;
					uhChange += Constants.uh_MAX_AU11;
					dChange += Constants.d_MAX_AU11;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU11 + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU11 + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU11 + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU11 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= uh f=0] " + @"\SetReg[name= d f=0] " +
						@"\SetReg[name= nostrilL3tx f=0] " + @"\SetReg[name= nostrilR3tx f=0] ";
					break;
				default:
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU11 * Convert.ToDouble( intensity ) / 100;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU11 * Convert.ToDouble( intensity ) / 100;
					uhChange += Constants.uh_MAX_AU11 * Convert.ToDouble( intensity ) / 100;
					dChange += Constants.d_MAX_AU11 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= uh f=" + Constants.uh_MAX_AU11 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU11 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU11 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU11 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			nostrilL3tx += nostrilL3txChange;
			nostrilR3tx += nostrilR3txChange;
			uh += uhChange;
			d += dChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 12 (Lip Corner Puller) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 12 (Lip Corner Puller) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU12( string intensity, string lastIntensity )
		{
			double smile3Change = 0;
			//double eyChange = 0;
			//double iyChange = 0;
			//double lipcornerL3tyChange = 0;
			//double lipcornerR3tyChange = 0;
			//double nostrilL3txChange = 0;
			//double nostrilR3txChange = 0;

			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					smile3Change = -Constants.smile3_MAX_AU12 / Constants.AIntensity;
					//eyChange = -Constants.ey_MAX_AU12 / Constants.AIntensity;
					//iyChange = -Constants.iy_MAX_AU12 / Constants.AIntensity;
					//lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU12 / Constants.AIntensity;
					//lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU12 / Constants.AIntensity;
					//nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU12 / Constants.AIntensity;
					//nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU12 / Constants.AIntensity;
					break;
				case ( "B" ):
					smile3Change = -Constants.smile3_MAX_AU12 / Constants.BIntensity;
					//eyChange = -Constants.ey_MAX_AU12 / Constants.BIntensity;
					//iyChange = -Constants.iy_MAX_AU12 / Constants.BIntensity;
					//lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU12 / Constants.BIntensity;
					//lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU12 / Constants.BIntensity;
					//nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU12 / Constants.BIntensity;
					//nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU12 / Constants.BIntensity;
					break;
				case ( "C" ):
					smile3Change = -Constants.smile3_MAX_AU12 / Constants.CIntensity;
					//eyChange = -Constants.ey_MAX_AU12 / Constants.CIntensity;
					//iyChange = -Constants.iy_MAX_AU12 / Constants.CIntensity;
					//lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU12 / Constants.CIntensity;
					//lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU12 / Constants.CIntensity;
					//nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU12 / Constants.CIntensity;
					//nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU12 / Constants.CIntensity;
					break;
				case ( "D" ):
					smile3Change = -Constants.smile3_MAX_AU12 / Constants.DIntensity;
					//eyChange = -Constants.ey_MAX_AU12 / Constants.DIntensity;
					//iyChange = -Constants.iy_MAX_AU12 / Constants.DIntensity;
					//lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU12 / Constants.DIntensity;
					//lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU12 / Constants.DIntensity;
					//nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU12 / Constants.DIntensity;
					//nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU12 / Constants.DIntensity;
					break;
				case ( "E" ):
					smile3Change = -Constants.smile3_MAX_AU12;
					//eyChange = -Constants.ey_MAX_AU12;
					//iyChange = -Constants.iy_MAX_AU12;
					//lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU12;
					//lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU12;
					//nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU12;
					//nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU12;
					break;
				case ( "0" ):
					break;
				default:
					smile3Change = -Constants.smile3_MAX_AU12 * Convert.ToDouble( lastIntensity ) / 100;
					//eyChange = -Constants.ey_MAX_AU12 * Convert.ToDouble( lastIntensity ) / 100;
					//iyChange = -Constants.iy_MAX_AU12 * Convert.ToDouble( lastIntensity ) / 100;
					//lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU12 * Convert.ToDouble( lastIntensity ) / 100;
					//lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU12 * Convert.ToDouble( lastIntensity ) / 100;
					//nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU12 * Convert.ToDouble( lastIntensity ) / 100;
					//nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU12 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					smile3Change += Constants.smile3_MAX_AU12 / Constants.AIntensity;
					hyperText = @"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU12 / Constants.AIntensity + "] ";
					//eyChange += Constants.ey_MAX_AU12 / Constants.AIntensity;
					//iyChange += Constants.iy_MAX_AU12 / Constants.AIntensity;
					//lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU12 / Constants.AIntensity;
					//lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU12 / Constants.AIntensity;
					//nostrilL3txChange += Constants.nostrilL3tx_MAX_AU12 / Constants.AIntensity;
					//nostrilR3txChange += Constants.nostrilR3tx_MAX_AU12 / Constants.AIntensity;
					//hyperText = @"\SetReg[name= ey f=" + Constants.ey_MAX_AU12 / Constants.AIntensity + "] " +
					//            @"\SetReg[name= iy f=" + Constants.iy_MAX_AU12 / Constants.AIntensity + "] " +
					//            @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU12 / Constants.AIntensity + "] " +
					//            @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU12 / Constants.AIntensity + "] " +
					//            @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU12 / Constants.AIntensity + "] " +
					//            @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU12 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					smile3Change += Constants.smile3_MAX_AU12 / Constants.BIntensity;
					hyperText = @"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU12 / Constants.BIntensity + "] ";
					//eyChange += Constants.ey_MAX_AU12 / Constants.BIntensity;
					//iyChange += Constants.iy_MAX_AU12 / Constants.BIntensity;
					//lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU12 / Constants.BIntensity;
					//lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU12 / Constants.BIntensity;
					//nostrilL3txChange += Constants.nostrilL3tx_MAX_AU12 / Constants.BIntensity;
					//nostrilR3txChange += Constants.nostrilR3tx_MAX_AU12 / Constants.BIntensity;
					//hyperText = @"\SetReg[name= ey f=" + Constants.ey_MAX_AU12 / Constants.BIntensity + "] " +
					//            @"\SetReg[name= iy f=" + Constants.iy_MAX_AU12 / Constants.BIntensity + "] " +
					//            @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU12 / Constants.BIntensity + "] " +
					//            @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU12 / Constants.BIntensity + "] " +
					//            @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU12 / Constants.BIntensity + "] " +
					//            @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU12 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					smile3Change += Constants.smile3_MAX_AU12 / Constants.CIntensity;
					hyperText = @"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU12 / Constants.CIntensity + "] ";
					//eyChange += Constants.ey_MAX_AU12 / Constants.CIntensity;
					//iyChange += Constants.iy_MAX_AU12 / Constants.CIntensity;
					//lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU12 / Constants.CIntensity;
					//lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU12 / Constants.CIntensity;
					//nostrilL3txChange += Constants.nostrilL3tx_MAX_AU12 / Constants.CIntensity;
					//nostrilR3txChange += Constants.nostrilR3tx_MAX_AU12 / Constants.CIntensity;
					//hyperText = @"\SetReg[name= ey f=" + Constants.ey_MAX_AU12 / Constants.CIntensity + "] " +
					//            @"\SetReg[name= iy f=" + Constants.iy_MAX_AU12 / Constants.CIntensity + "] " +
					//            @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU12 / Constants.CIntensity + "] " +
					//            @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU12 / Constants.CIntensity + "] " +
					//            @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU12 / Constants.CIntensity + "] " +
					//            @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU12 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					smile3Change += Constants.smile3_MAX_AU12 / Constants.DIntensity;
					hyperText = @"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU12 / Constants.DIntensity + "] ";
					//eyChange += Constants.ey_MAX_AU12 / Constants.DIntensity;
					//iyChange += Constants.iy_MAX_AU12 / Constants.DIntensity;
					//lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU12 / Constants.DIntensity;
					//lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU12 / Constants.DIntensity;
					//nostrilL3txChange += Constants.nostrilL3tx_MAX_AU12 / Constants.DIntensity;
					//nostrilR3txChange += Constants.nostrilR3tx_MAX_AU12 / Constants.DIntensity;
					//hyperText = @"\SetReg[name= ey f=" + Constants.ey_MAX_AU12 / Constants.DIntensity + "] " +
					//            @"\SetReg[name= iy f=" + Constants.iy_MAX_AU12 / Constants.DIntensity + "] " +
					//            @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU12 / Constants.DIntensity + "] " +
					//            @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU12 / Constants.DIntensity + "] " +
					//            @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU12 / Constants.DIntensity + "] " +
					//            @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU12 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					smile3Change += Constants.smile3_MAX_AU12;
					hyperText = @"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU12 + "] ";
					//eyChange += Constants.ey_MAX_AU12;
					//iyChange += Constants.iy_MAX_AU12;
					//lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU12;
					//lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU12;
					//nostrilL3txChange += Constants.nostrilL3tx_MAX_AU12;
					//nostrilR3txChange += Constants.nostrilR3tx_MAX_AU12;
					//hyperText = @"\SetReg[name= ey f=" + Constants.ey_MAX_AU12 + "] " +
					//            @"\SetReg[name= iy f=" + Constants.iy_MAX_AU12 + "] " +
					//            @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU12 + "] " +
					//            @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU12 + "] " +
					//            @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU12 + "] " +
					//            @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU12 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= smile3 f=0] ";
					//hyperText = @"\SetReg[name= ey f= 0] " +
					//            @"\SetReg[name= iy f= 0] " +
					//            @"\SetReg[name= lipcornerL3ty f= 0] " +
					//            @"\SetReg[name= lipcornerR3ty f= 0] " +
					//            @"\SetReg[name= nostrilL3tx f= 0] " +
					//            @"\SetReg[name= nostrilR3tx f= 0] ";
					break;
				default:
					smile3Change += Constants.smile3_MAX_AU12 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= smile3 f=" + Constants.smile3_MAX_AU12 * Convert.ToDouble( intensity ) / 100 + "] ";
					//eyChange += Constants.ey_MAX_AU12 * Convert.ToDouble( intensity ) / 100;
					//iyChange += Constants.iy_MAX_AU12 * Convert.ToDouble( intensity ) / 100;
					//lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU12 * Convert.ToDouble( intensity ) / 100;
					//lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU12 * Convert.ToDouble( intensity ) / 100;
					//nostrilL3txChange += Constants.nostrilL3tx_MAX_AU12 * Convert.ToDouble( intensity ) / 100;
					//nostrilR3txChange += Constants.nostrilR3tx_MAX_AU12 * Convert.ToDouble( intensity ) / 100;
					//hyperText = @"\SetReg[name= ey f=" + Constants.ey_MAX_AU12 * Convert.ToDouble( intensity ) / 100 + "] " +
					//            @"\SetReg[name= iy f=" + Constants.iy_MAX_AU12 * Convert.ToDouble( intensity ) / 100 + "] " +
					//            @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU12 * Convert.ToDouble( intensity ) / 100 + "] " +
					//            @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU12 * Convert.ToDouble( intensity ) / 100 + "] " +
					//            @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU12 * Convert.ToDouble( intensity ) / 100 + "] " +
					//            @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU12 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			smile3 += smile3Change;
			//ey += eyChange;
			//iy += iyChange;
			//lipcornerL3ty += lipcornerL3tyChange;
			//lipcornerR3ty += lipcornerR3tyChange;
			//nostrilL3tx += nostrilL3txChange;
			//nostrilR3tx += nostrilR3txChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets Action Unit 12 (Lip Corner Puller) on the Haptek face.
		/// </summary>
		/// <returns>
		///     Hypertext to reset Action Unit 12 (Lip Corner Puller) on the Haptek face.
		/// </returns>
		/// <remarks></remarks>
		public string resetAU12( )
		{
			smile3 = 0;
			return @"\SetReg[name= smile3 f=0] ";
			//return @"\SetReg[name= ey f= 0] " +
			//       @"\SetReg[name= iy f= 0] " +
			//       @"\SetReg[name= lipcornerL3ty f= 0] " +
			//       @"\SetReg[name= lipcornerR3ty f= 0] " +
			//       @"\SetReg[name= nostrilL3tx f= 0] " +
			//       @"\SetReg[name= nostrilR3tx f= 0] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 13 (Sharp Lip Puller) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 13 (Sharp Lip Puller) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU13( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double lipcornerL3tyChange = 0;
			double lipcornerR3tyChange = 0;
			string hyperText = "";
			switch( lastLeftIntensity )
			{
				case ( "A" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU13 / Constants.AIntensity;
					break;
				case ( "B" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU13 / Constants.BIntensity;
					break;
				case ( "C" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU13 / Constants.CIntensity;
					break;
				case ( "D" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU13 / Constants.DIntensity;
					break;
				case ( "E" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU13;
					break;
				case ( "0" ):
					break;
				default:
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU13 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU13 / Constants.AIntensity;
					break;
				case ( "B" ):
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU13 / Constants.BIntensity;
					break;
				case ( "C" ):
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU13 / Constants.CIntensity;
					break;
				case ( "D" ):
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU13 / Constants.DIntensity;
					break;
				case ( "E" ):
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU13;
					break;
				case ( "0" ):
					break;
				default:
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU13 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU13 / Constants.AIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU13 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU13 / Constants.BIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU13 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU13 / Constants.CIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU13 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU13 / Constants.DIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU13 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU13;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU13 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= lipcornerL3ty f=0] ";
					break;
				default:
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU13 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU13 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU13 / Constants.AIntensity;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU13 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU13 / Constants.BIntensity;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU13 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU13 / Constants.CIntensity;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU13 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU13 / Constants.DIntensity;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU13 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU13;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU13 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= lipcornerR3ty f=0] ";
					break;
				default:
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU13 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU13 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			lipcornerL3ty += lipcornerL3tyChange;
			lipcornerR3ty += lipcornerR3tyChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 14 (Dimpler) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 14 (Dimpler) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU14( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double smirkChange = 0;
			double smirkLChange = 0;
			string hyperText = "";
			switch( lastLeftIntensity )
			{
				case ( "A" ):
					smirkLChange = -Constants.smirkL_MAX_AU14 / Constants.AIntensity;
					break;
				case ( "B" ):
					smirkLChange = -Constants.smirkL_MAX_AU14 / Constants.BIntensity;
					break;
				case ( "C" ):
					smirkLChange = -Constants.smirkL_MAX_AU14 / Constants.CIntensity;
					break;
				case ( "D" ):
					smirkLChange = -Constants.smirkL_MAX_AU14 / Constants.DIntensity;
					break;
				case ( "E" ):
					smirkLChange = -Constants.smirkL_MAX_AU14;
					break;
				case ( "0" ):
					break;
				default:
					smirkLChange = -Constants.smirkL_MAX_AU14 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					smirkChange = -Constants.smirk_MAX_AU14 / Constants.AIntensity;
					break;
				case ( "B" ):
					smirkChange = -Constants.smirk_MAX_AU14 / Constants.BIntensity;
					break;
				case ( "C" ):
					smirkChange = -Constants.smirk_MAX_AU14 / Constants.CIntensity;
					break;
				case ( "D" ):
					smirkChange = -Constants.smirk_MAX_AU14 / Constants.DIntensity;
					break;
				case ( "E" ):
					smirkChange = -Constants.smirk_MAX_AU14;
					break;
				case ( "0" ):
					break;
				default:
					smirkChange = -Constants.smirk_MAX_AU14 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					smirkLChange += Constants.smirkL_MAX_AU14 / Constants.AIntensity;
					hyperText = @"\SetReg[name= smirkL f=" + Constants.smirkL_MAX_AU14 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					smirkLChange += Constants.smirkL_MAX_AU14 / Constants.BIntensity;
					hyperText = @"\SetReg[name= smirkL f=" + Constants.smirkL_MAX_AU14 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					smirkLChange += Constants.smirkL_MAX_AU14 / Constants.CIntensity;
					hyperText = @"\SetReg[name= smirkL f=" + Constants.smirkL_MAX_AU14 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					smirkLChange += Constants.smirkL_MAX_AU14 / Constants.DIntensity;
					hyperText = @"\SetReg[name= smirkL f=" + Constants.smirkL_MAX_AU14 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					smirkLChange += Constants.smirkL_MAX_AU14;
					hyperText = @"\SetReg[name= smirkL f=" + Constants.smirkL_MAX_AU14 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= smirkL f=0] ";
					break;
				default:
					smirkLChange += Constants.smirkL_MAX_AU14 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= smirkL f=" + Constants.smirkL_MAX_AU14 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					smirkChange += Constants.smirk_MAX_AU14 / Constants.AIntensity;
					hyperText += @"\SetReg[name= smirk f=" + Constants.smirk_MAX_AU14 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					smirkChange += Constants.smirk_MAX_AU14 / Constants.BIntensity;
					hyperText += @"\SetReg[name= smirk f=" + Constants.smirk_MAX_AU14 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					smirkChange += Constants.smirk_MAX_AU14 / Constants.CIntensity;
					hyperText += @"\SetReg[name= smirk f=" + Constants.smirk_MAX_AU14 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					smirkChange += Constants.smirk_MAX_AU14 / Constants.DIntensity;
					hyperText += @"\SetReg[name= smirk f=" + Constants.smirk_MAX_AU14 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					smirkChange += Constants.smirk_MAX_AU14;
					hyperText += @"\SetReg[name= smirk f=" + Constants.smirk_MAX_AU14 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= smirk f=0] ";
					break;
				default:
					smirkChange += Constants.smirk_MAX_AU14 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= smirk f=" + Constants.smirk_MAX_AU14 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			smirk += smirkChange;
			smirkL += smirkLChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 15 (Lip Corner Depressor) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 15 (Lip Corner Depressor) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU15( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double lipcornerL3tyChange = 0;
			double lipcornerR3tyChange = 0;
			string hyperText = "";
			switch( lastLeftIntensity )
			{
				case ( "A" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU15 / Constants.AIntensity;
					break;
				case ( "B" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU15 / Constants.BIntensity;
					break;
				case ( "C" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU15 / Constants.CIntensity;
					break;
				case ( "D" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU15 / Constants.DIntensity;
					break;
				case ( "E" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU15;
					break;
				case ( "0" ):
					break;
				default:
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU15 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU15 / Constants.AIntensity;
					break;
				case ( "B" ):
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU15 / Constants.BIntensity;
					break;
				case ( "C" ):
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU15 / Constants.CIntensity;
					break;
				case ( "D" ):
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU15 / Constants.DIntensity;
					break;
				case ( "E" ):
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU15;
					break;
				case ( "0" ):
					break;
				default:
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU15 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU15 / Constants.AIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU15 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU15 / Constants.BIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU15 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU15 / Constants.CIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU15 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU15 / Constants.DIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU15 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU15;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU15 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= lipcornerL3ty f=0] ";
					break;
				default:
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU15 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU15 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU15 / Constants.AIntensity;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU15 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU15 / Constants.BIntensity;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU15 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU15 / Constants.CIntensity;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU15 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU15 / Constants.DIntensity;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU15 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU15;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU15 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= lipcornerR3ty f=0] ";
					break;
				default:
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU15 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU15 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			lipcornerL3ty += lipcornerL3tyChange;
			lipcornerR3ty += lipcornerR3tyChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets Action Unit 15 (Lip Corner Depressor) on the Haptek face.
		/// </summary>
		/// <returns>
		///     Hypertext to reset Action Unit 15 (Lip Corner Depressor) on the Haptek face.
		/// </returns>
		/// <remarks></remarks>
		public string resetAU15( )
		{
			lipcornerL3ty = 0;
			lipcornerR3ty = 0;
			return @"\SetReg[name= lipcornerR3ty f=0] " + @"\SetReg[name= lipcornerL3ty f=0] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 16 (Lower Lip Depressor) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 16 (Lower Lip Depressor) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU16( string intensity, string lastIntensity )
		{
			double thChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					thChange = -Constants.th_MAX_AU16 / Constants.AIntensity;
					break;
				case ( "B" ):
					thChange = -Constants.th_MAX_AU16 / Constants.BIntensity;
					break;
				case ( "C" ):
					thChange = -Constants.th_MAX_AU16 / Constants.CIntensity;
					break;
				case ( "D" ):
					thChange = -Constants.th_MAX_AU16 / Constants.DIntensity;
					break;
				case ( "E" ):
					thChange = -Constants.th_MAX_AU16;
					break;
				case ( "0" ):
					break;
				default:
					thChange = -Constants.th_MAX_AU16 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					thChange += Constants.th_MAX_AU16 / Constants.AIntensity;
					hyperText = @"\SetReg[name= th f=" + Constants.th_MAX_AU16 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					thChange += Constants.th_MAX_AU16 / Constants.BIntensity;
					hyperText = @"\SetReg[name= th f=" + Constants.th_MAX_AU16 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					thChange += Constants.th_MAX_AU16 / Constants.CIntensity;
					hyperText = @"\SetReg[name= th f=" + Constants.th_MAX_AU16 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					thChange += Constants.th_MAX_AU16 / Constants.DIntensity;
					hyperText = @"\SetReg[name= th f=" + Constants.th_MAX_AU16 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					thChange += Constants.th_MAX_AU16;
					hyperText = @"\SetReg[name= th f=" + Constants.th_MAX_AU16 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= th f=0] ";
					break;
				default:
					thChange += Constants.th_MAX_AU16 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= th f=" + Constants.th_MAX_AU16 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			th += thChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 17 (Chin Raiser) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 17 (Chin Raiser) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU17( string intensity, string lastIntensity )
		{
			double lipcornerL3tyChange = 0;
			double lipcornerR3tyChange = 0;
			double aaChange = 0;
			double owChange = 0;
			double mouth2tyChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU17 / Constants.AIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU17 / Constants.AIntensity;
					aaChange = -Constants.aa_MAX_AU17 / Constants.AIntensity;
					owChange = -Constants.ow_MAX_AU17 / Constants.AIntensity;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU17 / Constants.AIntensity;
					break;
				case ( "B" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU17 / Constants.BIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU17 / Constants.BIntensity;
					aaChange = -Constants.aa_MAX_AU17 / Constants.BIntensity;
					owChange = -Constants.ow_MAX_AU17 / Constants.BIntensity;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU17 / Constants.BIntensity;
					break;
				case ( "C" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU17 / Constants.CIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU17 / Constants.CIntensity;
					aaChange = -Constants.aa_MAX_AU17 / Constants.CIntensity;
					owChange = -Constants.ow_MAX_AU17 / Constants.CIntensity;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU17 / Constants.CIntensity;
					break;
				case ( "D" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU17 / Constants.DIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU17 / Constants.DIntensity;
					aaChange = -Constants.aa_MAX_AU17 / Constants.DIntensity;
					owChange = -Constants.ow_MAX_AU17 / Constants.DIntensity;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU17 / Constants.DIntensity;
					break;
				case ( "E" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU17;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU17;
					aaChange = -Constants.aa_MAX_AU17;
					owChange = -Constants.ow_MAX_AU17;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU17;
					break;
				case ( "0" ):
					break;
				default:
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU17 * Convert.ToDouble( lastIntensity ) / 100;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU17 * Convert.ToDouble( lastIntensity ) / 100;
					aaChange = -Constants.aa_MAX_AU17 * Convert.ToDouble( lastIntensity ) / 100;
					owChange = -Constants.ow_MAX_AU17 * Convert.ToDouble( lastIntensity ) / 100;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU17 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU17 / Constants.AIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU17 / Constants.AIntensity;
					aaChange += Constants.aa_MAX_AU17 / Constants.AIntensity;
					owChange += Constants.ow_MAX_AU17 / Constants.AIntensity;
					mouth2tyChange += Constants.mouth2ty_MAX_AU17 / Constants.AIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU17 / Constants.AIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU17 / Constants.AIntensity + "] " +
								@"\SetReg[name= aa f=" + Constants.aa_MAX_AU17 / Constants.AIntensity + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU17 / Constants.AIntensity + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU17 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU17 / Constants.BIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU17 / Constants.BIntensity;
					aaChange += Constants.aa_MAX_AU17 / Constants.BIntensity;
					owChange += Constants.ow_MAX_AU17 / Constants.BIntensity;
					mouth2tyChange += Constants.mouth2ty_MAX_AU17 / Constants.BIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU17 / Constants.BIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU17 / Constants.BIntensity + "] " +
								@"\SetReg[name= aa f=" + Constants.aa_MAX_AU17 / Constants.BIntensity + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU17 / Constants.BIntensity + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU17 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU17 / Constants.CIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU17 / Constants.CIntensity;
					aaChange += Constants.aa_MAX_AU17 / Constants.CIntensity;
					owChange += Constants.ow_MAX_AU17 / Constants.CIntensity;
					mouth2tyChange += Constants.mouth2ty_MAX_AU17 / Constants.CIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU17 / Constants.CIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU17 / Constants.CIntensity + "] " +
								@"\SetReg[name= aa f=" + Constants.aa_MAX_AU17 / Constants.CIntensity + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU17 / Constants.CIntensity + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU17 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU17 / Constants.DIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU17 / Constants.DIntensity;
					aaChange += Constants.aa_MAX_AU17 / Constants.DIntensity;
					owChange += Constants.ow_MAX_AU17 / Constants.DIntensity;
					mouth2tyChange += Constants.mouth2ty_MAX_AU17 / Constants.DIntensity;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU17 / Constants.DIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU17 / Constants.DIntensity + "] " +
								@"\SetReg[name= aa f=" + Constants.aa_MAX_AU17 / Constants.DIntensity + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU17 / Constants.DIntensity + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU17 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU17;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU17;
					aaChange += Constants.aa_MAX_AU17;
					owChange += Constants.ow_MAX_AU17;
					mouth2tyChange += Constants.mouth2ty_MAX_AU17;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU17 + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU17 + "] " +
								@"\SetReg[name= aa f=" + Constants.aa_MAX_AU17 + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU17 + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU17 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= lipcornerL3ty f=0] " + @"\SetReg[name= lipcornerR3ty f=0] "
						+ @"\SetReg[name= aa f=0] " + @"\SetReg[name= ow f=0] " + @"\SetReg[name= mouth2ty f=0] ";
					break;
				default:
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU17 * Convert.ToDouble( intensity ) / 100;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU17 * Convert.ToDouble( intensity ) / 100;
					aaChange += Constants.aa_MAX_AU17 * Convert.ToDouble( intensity ) / 100;
					owChange += Constants.ow_MAX_AU17 * Convert.ToDouble( intensity ) / 100;
					mouth2tyChange += Constants.mouth2ty_MAX_AU17 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU17 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU17 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= aa f=" + Constants.aa_MAX_AU17 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= ow f=" + Constants.ow_MAX_AU17 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU17 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			lipcornerL3ty += lipcornerL3tyChange;
			lipcornerR3ty += lipcornerR3tyChange;
			aa += aaChange;
			ow += owChange;
			mouth2ty += mouth2tyChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 18 (Lip Pucker) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 18 (Lip Pucker) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU18( string intensity, string lastIntensity )
		{
			double kissChange = 0;
			string hypertext = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					kissChange = -Constants.kiss_MAX_AU18 / Constants.AIntensity;
					break;
				case ( "B" ):
					kissChange = -Constants.kiss_MAX_AU18 / Constants.BIntensity;
					break;
				case ( "C" ):
					kissChange = -Constants.kiss_MAX_AU18 / Constants.CIntensity;
					break;
				case ( "D" ):
					kissChange = -Constants.kiss_MAX_AU18 / Constants.DIntensity;
					break;
				case ( "E" ):
					kissChange = -Constants.kiss_MAX_AU18;
					break;
				case ( "0" ):
					break;
				default:
					kissChange = -Constants.kiss_MAX_AU18 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					kissChange += Constants.kiss_MAX_AU18 / Constants.AIntensity;
					hypertext = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU18 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					kissChange += Constants.kiss_MAX_AU18 / Constants.BIntensity;
					hypertext = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU18 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					kissChange += Constants.kiss_MAX_AU18 / Constants.CIntensity;
					hypertext = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU18 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					kissChange += Constants.kiss_MAX_AU18 / Constants.DIntensity;
					hypertext = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU18 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					kissChange += Constants.kiss_MAX_AU18;
					hypertext = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU18 + "] ";
					break;
				case ( "0" ):
					hypertext = @"\SetReg[name= kiss f=0] ";
					break;
				default:
					kissChange += Constants.kiss_MAX_AU18 * Convert.ToDouble( intensity ) / 100;
					hypertext = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU18 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			kiss += kissChange;
			return hypertext;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 20 (Lip Stretcher) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 20 (Lip Stretcher) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU20( string intensity, string lastIntensity )
		{
			double lipcornerL3tyChange = 0;
			double lipcornerR3tyChange = 0;
			double bChange = 0;
			double owChange = 0;
			double mouth2tyChange = 0;
			double nostrilL3txChange = 0;
			double nostrilR3txChange = 0;
			double thChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU20 / Constants.AIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU20 / Constants.AIntensity;
					bChange = -Constants.b_MAX_AU20 / Constants.AIntensity;
					owChange = -Constants.ow_MAX_AU20 / Constants.AIntensity;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU20 / Constants.AIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU20 / Constants.AIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU20 / Constants.AIntensity;
					thChange = -Constants.th_MAX_AU20 / Constants.AIntensity;
					break;
				case ( "B" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU20 / Constants.BIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU20 / Constants.BIntensity;
					bChange = -Constants.b_MAX_AU20 / Constants.BIntensity;
					owChange = -Constants.ow_MAX_AU20 / Constants.BIntensity;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU20 / Constants.BIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU20 / Constants.BIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU20 / Constants.BIntensity;
					thChange = -Constants.th_MAX_AU20 / Constants.BIntensity;
					break;
				case ( "C" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU20 / Constants.CIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU20 / Constants.CIntensity;
					bChange = -Constants.b_MAX_AU20 / Constants.CIntensity;
					owChange = -Constants.ow_MAX_AU20 / Constants.CIntensity;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU20 / Constants.CIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU20 / Constants.CIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU20 / Constants.CIntensity;
					thChange = -Constants.th_MAX_AU20 / Constants.CIntensity;
					break;
				case ( "D" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU20 / Constants.DIntensity;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU20 / Constants.DIntensity;
					bChange = -Constants.b_MAX_AU20 / Constants.DIntensity;
					owChange = -Constants.ow_MAX_AU20 / Constants.DIntensity;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU20 / Constants.DIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU20 / Constants.DIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU20 / Constants.DIntensity;
					thChange = -Constants.th_MAX_AU20 / Constants.DIntensity;
					break;
				case ( "E" ):
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU20;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU20;
					bChange = -Constants.b_MAX_AU20;
					owChange = -Constants.ow_MAX_AU20;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU20;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU20;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU20;
					thChange = -Constants.th_MAX_AU20;
					break;
				case ( "0" ):
					break;
				default:
					lipcornerL3tyChange = -Constants.lipcornerL3ty_MAX_AU20 * Convert.ToDouble( lastIntensity ) / 100;
					lipcornerR3tyChange = -Constants.lipcornerR3ty_MAX_AU20 * Convert.ToDouble( lastIntensity ) / 100;
					bChange = -Constants.b_MAX_AU20 * Convert.ToDouble( lastIntensity ) / 100;
					owChange = -Constants.ow_MAX_AU20 * Convert.ToDouble( lastIntensity ) / 100;
					mouth2tyChange = -Constants.mouth2ty_MAX_AU20 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU20 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU20 * Convert.ToDouble( lastIntensity ) / 100;
					thChange = -Constants.th_MAX_AU20 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}

			switch( intensity )
			{
				case ( "A" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU20 / Constants.AIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU20 / Constants.AIntensity;
					bChange += Constants.b_MAX_AU20 / Constants.AIntensity;
					owChange += Constants.ow_MAX_AU20 / Constants.AIntensity;
					mouth2tyChange += Constants.mouth2ty_MAX_AU20 / Constants.AIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU20 / Constants.AIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU20 / Constants.AIntensity;
					thChange += Constants.th_MAX_AU20 / Constants.AIntensity;
					hyperText = @"\SetReg[name= ow f=" + Constants.ow_MAX_AU20 / Constants.AIntensity + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU20 / Constants.AIntensity + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU20 / Constants.AIntensity + "] " +
								@"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU20 / Constants.AIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.AIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.AIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.AIntensity + "] " +
								@"\SetReg[name= th f=" + Constants.th_MAX_AU20 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU20 / Constants.BIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU20 / Constants.BIntensity;
					bChange += Constants.b_MAX_AU20 / Constants.BIntensity;
					owChange += Constants.ow_MAX_AU20 / Constants.BIntensity;
					mouth2tyChange += Constants.mouth2ty_MAX_AU20 / Constants.BIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU20 / Constants.BIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU20 / Constants.BIntensity;
					thChange += Constants.th_MAX_AU20 / Constants.BIntensity;
					hyperText = @"\SetReg[name= ow f=" + Constants.ow_MAX_AU20 / Constants.BIntensity + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU20 / Constants.BIntensity + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU20 / Constants.BIntensity + "] " +
								@"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU20 / Constants.BIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.BIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.BIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.BIntensity + "] " +
								@"\SetReg[name= th f=" + Constants.th_MAX_AU20 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU20 / Constants.CIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU20 / Constants.CIntensity;
					bChange += Constants.b_MAX_AU20 / Constants.CIntensity;
					owChange += Constants.ow_MAX_AU20 / Constants.CIntensity;
					mouth2tyChange += Constants.mouth2ty_MAX_AU20 / Constants.CIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU20 / Constants.CIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU20 / Constants.CIntensity;
					thChange += Constants.th_MAX_AU20 / Constants.CIntensity;
					hyperText = @"\SetReg[name= ow f=" + Constants.ow_MAX_AU20 / Constants.CIntensity + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU20 / Constants.CIntensity + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU20 / Constants.CIntensity + "] " +
								@"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU20 / Constants.CIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.CIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.CIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.CIntensity + "] " +
								@"\SetReg[name= th f=" + Constants.th_MAX_AU20 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU20 / Constants.DIntensity;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU20 / Constants.DIntensity;
					bChange += Constants.b_MAX_AU20 / Constants.DIntensity;
					owChange += Constants.ow_MAX_AU20 / Constants.DIntensity;
					mouth2tyChange += Constants.mouth2ty_MAX_AU20 / Constants.DIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU20 / Constants.DIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU20 / Constants.DIntensity;
					thChange += Constants.th_MAX_AU20 / Constants.DIntensity;
					hyperText = @"\SetReg[name= ow f=" + Constants.ow_MAX_AU20 / Constants.DIntensity + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU20 / Constants.DIntensity + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU20 / Constants.DIntensity + "] " +
								@"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU20 / Constants.DIntensity + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.DIntensity + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.DIntensity + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.lipcornerR3ty_MAX_AU20 / Constants.DIntensity + "] " +
								@"\SetReg[name= th f=" + Constants.th_MAX_AU20 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU20;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU20;
					bChange += Constants.b_MAX_AU20;
					owChange += Constants.ow_MAX_AU20;
					mouth2tyChange += Constants.mouth2ty_MAX_AU20;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU20;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU20;
					thChange += Constants.th_MAX_AU20;
					hyperText = @"\SetReg[name= ow f=" + Constants.ow_MAX_AU20 + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU20 + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU20 + "] " +
								@"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU20 + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU20 + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.lipcornerR3ty_MAX_AU20 + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.lipcornerR3ty_MAX_AU20 + "] " +
								@"\SetReg[name= th f=" + Constants.th_MAX_AU20 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= ow f=0] " + @"\SetReg[name= mouth2ty f=0] " + @"\SetReg[name= b f=0] " +
								@"\SetReg[name= lipcornerL3ty f=0] " + @"\SetReg[name= lipcornerR3ty f=0] " +
								@"\SetReg[name= nostrilL3tx f=0] " + @"\SetReg[name= nostrilR3tx f=0] " + @"\SetReg[name= th f=0] ";
					break;
				default:
					lipcornerL3tyChange += Constants.lipcornerL3ty_MAX_AU20 * Convert.ToDouble( intensity ) / 100;
					lipcornerR3tyChange += Constants.lipcornerR3ty_MAX_AU20 * Convert.ToDouble( intensity ) / 100;
					bChange += Constants.b_MAX_AU20 * Convert.ToDouble( intensity ) / 100;
					owChange += Constants.ow_MAX_AU20 * Convert.ToDouble( intensity ) / 100;
					mouth2tyChange += Constants.mouth2ty_MAX_AU20 * Convert.ToDouble( intensity ) / 100;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU20 * Convert.ToDouble( intensity ) / 100;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU20 * Convert.ToDouble( intensity ) / 100;
					thChange += Constants.th_MAX_AU20 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= ow f=" + Constants.ow_MAX_AU20 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= mouth2ty f=" + Constants.mouth2ty_MAX_AU20 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU20 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= lipcornerL3ty f=" + Constants.lipcornerL3ty_MAX_AU20 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= lipcornerR3ty f=" + Constants.lipcornerR3ty_MAX_AU20 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= nostrilL3tx f=" + Constants.lipcornerR3ty_MAX_AU20 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= nostrilR3tx f=" + Constants.lipcornerR3ty_MAX_AU20 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= th f=" + Constants.th_MAX_AU20 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			lipcornerL3ty += lipcornerL3tyChange;
			lipcornerR3ty += lipcornerR3tyChange;
			b += bChange;
			ow += owChange;
			mouth2ty += mouth2tyChange;
			nostrilL3tx += nostrilL3txChange;
			nostrilR3tx += nostrilR3txChange;
			th += thChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets Action Unit 20 (Lip Stretcher) on the Haptek face.
		/// </summary>
		/// <returns>
		///     Hypertext to reset Action Unit 20 (Lip Stretcher) on the Haptek face.
		/// </returns>
		/// <remarks></remarks>
		public string resetAU20( )
		{
			lipcornerL3ty = 0;
			lipcornerR3ty = 0;
			b = 0;
			ow = 0;
			mouth2ty = 0;
			nostrilL3tx = 0;
			nostrilR3tx = 0;
			th = 0;

			return @"\SetReg[name= ow f=0] " + @"\SetReg[name= mouth2ty f=0] " + @"\SetReg[name= b f=0] " +
				   @"\SetReg[name= lipcornerL3ty f=0] " + @"\SetReg[name= lipcornerR3ty f=0] " +
				   @"\SetReg[name= nostrilL3tx f=0] " + @"\SetReg[name= nostrilR3tx f=0] " + @"\SetReg[name= th f=0] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 22 (Lip Funneler) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 22 (Lip Funneler) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU22( string intensity, string lastIntensity )
		{
			double chChange = 0;
			double uwChange = 0;
			string hyperTexts = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					chChange = -Constants.ch_MAX_AU22 / Constants.AIntensity;
					uwChange = -Constants.uw_MAX_AU22 / Constants.AIntensity;
					break;
				case ( "B" ):
					chChange = -Constants.ch_MAX_AU22 / Constants.BIntensity;
					uwChange = -Constants.uw_MAX_AU22 / Constants.BIntensity;
					break;
				case ( "C" ):
					chChange = -Constants.ch_MAX_AU22 / Constants.CIntensity;
					uwChange = -Constants.uw_MAX_AU22 / Constants.CIntensity;
					break;
				case ( "D" ):
					chChange = -Constants.ch_MAX_AU22 / Constants.DIntensity;
					uwChange = -Constants.uw_MAX_AU22 / Constants.DIntensity;
					break;
				case ( "E" ):
					chChange = -Constants.ch_MAX_AU22;
					uwChange = -Constants.uw_MAX_AU22;
					break;
				case ( "0" ):
					break;
				default:
					chChange = -Constants.ch_MAX_AU22 * Convert.ToDouble( lastIntensity ) / 100;
					uwChange = -Constants.uw_MAX_AU22 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					chChange += Constants.ch_MAX_AU22 / Constants.AIntensity;
					uwChange += Constants.uw_MAX_AU22 / Constants.AIntensity;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU22 / Constants.AIntensity + "] " +
								 @"\SetReg[name= uw f=" + Constants.uw_MAX_AU22 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					chChange += Constants.ch_MAX_AU22 / Constants.BIntensity;
					uwChange += Constants.uw_MAX_AU22 / Constants.BIntensity;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU22 / Constants.BIntensity + "] " +
								 @"\SetReg[name= uw f=" + Constants.uw_MAX_AU22 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					chChange += Constants.ch_MAX_AU22 / Constants.CIntensity;
					uwChange += Constants.uw_MAX_AU22 / Constants.CIntensity;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU22 / Constants.CIntensity + "] " +
								 @"\SetReg[name= uw f=" + Constants.uw_MAX_AU22 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					chChange += Constants.ch_MAX_AU22 / Constants.DIntensity;
					uwChange += Constants.uw_MAX_AU22 / Constants.DIntensity;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU22 / Constants.DIntensity + "] " +
								 @"\SetReg[name= uw f=" + Constants.uw_MAX_AU22 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					chChange += Constants.ch_MAX_AU22;
					uwChange += Constants.uw_MAX_AU22;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU22 + "] " +
								 @"\SetReg[name= uw f=" + Constants.uw_MAX_AU22 + "] ";
					break;
				case ( "0" ):
					hyperTexts = @"\SetReg[name= ch f=0] " +
								 @"\SetReg[name= uw f=0] ";
					break;
				default:
					chChange += Constants.ch_MAX_AU22 * Convert.ToDouble( intensity ) / 100;
					uwChange += Constants.uw_MAX_AU22 * Convert.ToDouble( intensity ) / 100;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU22 * Convert.ToDouble( intensity ) / 100 + "] " +
								 @"\SetReg[name= uw f=" + Constants.uw_MAX_AU22 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			ch += chChange;
			uw += uwChange;
			return hyperTexts;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 23 (Lip Tightener) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 23 (Lip Tightener) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU23( string intensity, string lastIntensity )
		{
			//double kissChange = 0;
			double bChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					//kissChange = -Constants.kiss_MAX_AU23 / Constants.AIntensity;
					bChange = -Constants.b_MAX_AU23 / Constants.AIntensity;
					break;
				case ( "B" ):
					//kissChange = -Constants.kiss_MAX_AU23 / Constants.BIntensity;
					bChange = -Constants.b_MAX_AU23 / Constants.BIntensity;
					break;
				case ( "C" ):
					//kissChange = -Constants.kiss_MAX_AU23 / Constants.CIntensity;
					bChange = -Constants.b_MAX_AU23 / Constants.CIntensity;
					break;
				case ( "D" ):
					//kissChange = -Constants.kiss_MAX_AU23 / Constants.DIntensity;
					bChange = -Constants.b_MAX_AU23 / Constants.DIntensity;
					break;
				case ( "E" ):
					//kissChange = -Constants.kiss_MAX_AU23;
					bChange = -Constants.b_MAX_AU23;
					break;
				case ( "0" ):
					break;
				default:
					//kissChange = -Constants.kiss_MAX_AU23 * Convert.ToDouble( lastIntensity ) / 100;
					bChange = -Constants.b_MAX_AU23 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					//kissChange += Constants.kiss_MAX_AU23 / Constants.AIntensity;
					bChange += Constants.b_MAX_AU23 / Constants.AIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU23 / Constants.AIntensity + "] ";
					//@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU23 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					//kissChange += Constants.kiss_MAX_AU23 / Constants.BIntensity;
					bChange += Constants.b_MAX_AU23 / Constants.BIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU23 / Constants.BIntensity + "] ";
					//@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU23 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					//kissChange += Constants.kiss_MAX_AU23 / Constants.CIntensity;
					bChange += Constants.b_MAX_AU23 / Constants.CIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU23 / Constants.CIntensity + "] ";
					//@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU23 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					//kissChange += Constants.kiss_MAX_AU23 / Constants.DIntensity;
					bChange += Constants.b_MAX_AU23 / Constants.DIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU23 / Constants.DIntensity + "] ";
					//@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU23 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					//kissChange += Constants.kiss_MAX_AU23;
					bChange += Constants.b_MAX_AU23;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU23 + "] ";
					//@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU23 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= kiss f=0] " + @"\SetReg[name= b f=0] ";
					break;
				default:
					//kissChange += Constants.kiss_MAX_AU23 * Convert.ToDouble( intensity ) / 100;
					bChange += Constants.b_MAX_AU23 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU23 * Convert.ToDouble( intensity ) / 100 + "] ";
					//@"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU23 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			//kiss += kissChange;
			b += bChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 24 (Lip Presser) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 24 (Lip Presser) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU24( string intensity, string lastIntensity )
		{
			double kissChange = 0;
			double bChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					kissChange = -Constants.kiss_MAX_AU24 / Constants.AIntensity;
					bChange = -Constants.b_MAX_AU24 / Constants.AIntensity;
					break;
				case ( "B" ):
					kissChange = -Constants.kiss_MAX_AU24 / Constants.BIntensity;
					bChange = -Constants.b_MAX_AU24 / Constants.BIntensity;
					break;
				case ( "C" ):
					kissChange = -Constants.kiss_MAX_AU24 / Constants.CIntensity;
					bChange = -Constants.b_MAX_AU24 / Constants.CIntensity;
					break;
				case ( "D" ):
					kissChange = -Constants.kiss_MAX_AU24 / Constants.DIntensity;
					bChange = -Constants.b_MAX_AU24 / Constants.DIntensity;
					break;
				case ( "E" ):
					kissChange = -Constants.kiss_MAX_AU24;
					bChange = -Constants.b_MAX_AU24;
					break;
				case ( "0" ):
					break;
				default:
					kissChange = -Constants.kiss_MAX_AU24 * Convert.ToDouble( lastIntensity ) / 100;
					bChange = -Constants.b_MAX_AU24 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					kissChange += Constants.kiss_MAX_AU24 / Constants.AIntensity;
					bChange += Constants.b_MAX_AU24 / Constants.AIntensity;
					hyperText = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU24 / Constants.AIntensity + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU24 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					kissChange += Constants.kiss_MAX_AU24 / Constants.BIntensity;
					bChange += Constants.b_MAX_AU24 / Constants.BIntensity;
					hyperText = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU24 / Constants.BIntensity + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU24 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					kissChange += Constants.kiss_MAX_AU24 / Constants.CIntensity;
					bChange += Constants.b_MAX_AU24 / Constants.CIntensity;
					hyperText = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU24 / Constants.CIntensity + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU24 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					kissChange += Constants.kiss_MAX_AU24 / Constants.DIntensity;
					bChange += Constants.b_MAX_AU24 / Constants.DIntensity;
					hyperText = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU24 / Constants.DIntensity + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU24 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					kissChange += Constants.kiss_MAX_AU24;
					bChange += Constants.b_MAX_AU24;
					hyperText = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU24 + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU24 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= kiss f=0] " + @"\SetReg[name= b f=0] ";
					break;
				default:
					kissChange += Constants.kiss_MAX_AU24 * Convert.ToDouble( intensity ) / 100;
					bChange += Constants.b_MAX_AU24 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= kiss f=" + Constants.kiss_MAX_AU24 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= b f=" + Constants.b_MAX_AU24 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			kiss += kissChange;
			b += bChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 25 (Lips Part) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 25 (Lips Part) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU25( string intensity, string lastIntensity )
		{
			double chChange = 0;
			double nostrilL3tyChange = 0;
			double nostrilR3tyChange = 0;
			string hyperTexts = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					chChange = -Constants.ch_MAX_AU25 / Constants.AIntensity;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU25 / Constants.AIntensity;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU25 / Constants.AIntensity;
					break;
				case ( "B" ):
					chChange = -Constants.ch_MAX_AU25 / Constants.BIntensity;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU25 / Constants.BIntensity;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU25 / Constants.BIntensity;
					break;
				case ( "C" ):
					chChange = -Constants.ch_MAX_AU25 / Constants.CIntensity;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU25 / Constants.CIntensity;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU25 / Constants.CIntensity;
					break;
				case ( "D" ):
					chChange = -Constants.ch_MAX_AU25 / Constants.DIntensity;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU25 / Constants.DIntensity;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU25 / Constants.DIntensity;
					break;
				case ( "E" ):
					chChange = -Constants.ch_MAX_AU25;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU25;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU25;
					break;
				case ( "0" ):
					break;
				default:
					chChange = -Constants.ch_MAX_AU25 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU25 * Convert.ToDouble( lastIntensity ) / 100;
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU25 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}

			switch( intensity )
			{
				case ( "A" ):
					chChange += Constants.ch_MAX_AU25 / Constants.AIntensity;
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU25 / Constants.AIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU25 / Constants.AIntensity;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU25 / Constants.AIntensity + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU25 / Constants.AIntensity + "] " +
								 @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU25 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					chChange += Constants.ch_MAX_AU25 / Constants.BIntensity;
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU25 / Constants.BIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU25 / Constants.BIntensity;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU25 / Constants.BIntensity + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU25 / Constants.BIntensity + "] " +
								 @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU25 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					chChange += Constants.ch_MAX_AU25 / Constants.CIntensity;
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU25 / Constants.CIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU25 / Constants.CIntensity;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU25 / Constants.CIntensity + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU25 / Constants.CIntensity + "] " +
								 @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU25 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					chChange += Constants.ch_MAX_AU25 / Constants.DIntensity;
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU25 / Constants.DIntensity;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU25 / Constants.DIntensity;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU25 / Constants.DIntensity + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU25 / Constants.DIntensity + "] " +
								 @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU25 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					chChange += Constants.ch_MAX_AU25;
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU25;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU25;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU25 + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU25 + "] " +
								 @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU25 + "] ";
					break;
				case ( "0" ):
					hyperTexts = @"\SetReg[name= ch f= 0] " +
								 @"\SetReg[name= nostrilL3ty f= 0] " +
								 @"\SetReg[name= nostrilR3ty f= 0] ";
					break;
				default:
					chChange += Constants.ch_MAX_AU25 * Convert.ToDouble( intensity ) / 100;
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU25 * Convert.ToDouble( intensity ) / 100;
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU25 * Convert.ToDouble( intensity ) / 100;
					hyperTexts = @"\SetReg[name= ch f=" + Constants.ch_MAX_AU25 * Convert.ToDouble( intensity ) / 100 + "] " +
								 @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU25 * Convert.ToDouble( intensity ) / 100 + "] " +
								 @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU25 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			ch += chChange;
			nostrilL3ty += nostrilL3tyChange;
			nostrilR3ty += nostrilR3tyChange;
			return hyperTexts;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 26 (Jaw Drop) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 26 (Jaw Drop) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU26( string intensity, string lastIntensity )
		{
			double aaChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					aaChange = -Constants.aa_MAX_AU26 / Constants.AIntensity;
					break;
				case ( "B" ):
					aaChange = -Constants.aa_MAX_AU26 / Constants.BIntensity;
					break;
				case ( "C" ):
					aaChange = -Constants.aa_MAX_AU26 / Constants.CIntensity;
					break;
				case ( "D" ):
					aaChange = -Constants.aa_MAX_AU26 / Constants.DIntensity;
					break;
				case ( "E" ):
					aaChange = -Constants.aa_MAX_AU26;
					break;
				case ( "0" ):
					break;
				default:
					aaChange = -Constants.aa_MAX_AU26 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					aaChange += Constants.aa_MAX_AU26 / Constants.AIntensity;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU26 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					aaChange += Constants.aa_MAX_AU26 / Constants.BIntensity;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU26 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					aaChange += Constants.aa_MAX_AU26 / Constants.CIntensity;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU26 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					aaChange += Constants.aa_MAX_AU26 / Constants.DIntensity;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU26 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					aaChange += Constants.aa_MAX_AU26;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU26 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= aa f=0] ";
					break;
				default:
					aaChange += Constants.aa_MAX_AU26 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU26 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			aa += aaChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets Action Unit 26 (Jaw Drop) on the Haptek face.
		/// </summary>
		/// <returns>
		///     Hypertext to reset Action Unit 26 (Jaw Drop) on the Haptek face.
		/// </returns>
		/// <remarks></remarks>
		public string resetAU26( )
		{
			aa = 0;
			return @"\SetReg[name= aa f=0] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 27 (Mouth Stretch) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 27 (Mouth Stretch) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU27( string intensity, string lastIntensity )
		{
			double aaChange = 0;
			double eyChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					aaChange = -Constants.aa_MAX_AU27 / Constants.AIntensity;
					eyChange = -Constants.ey_MAX_AU27 / Constants.AIntensity;
					break;
				case ( "B" ):
					aaChange = -Constants.aa_MAX_AU27 / Constants.BIntensity;
					eyChange = -Constants.ey_MAX_AU27 / Constants.BIntensity;
					break;
				case ( "C" ):
					aaChange = -Constants.aa_MAX_AU27 / Constants.CIntensity;
					eyChange = -Constants.ey_MAX_AU27 / Constants.CIntensity;
					break;
				case ( "D" ):
					aaChange = -Constants.aa_MAX_AU27 / Constants.DIntensity;
					eyChange = -Constants.ey_MAX_AU27 / Constants.DIntensity;
					break;
				case ( "E" ):
					aaChange = -Constants.aa_MAX_AU27;
					eyChange = -Constants.ey_MAX_AU27;
					break;
				case ( "0" ):
					break;
				default:
					aaChange = -Constants.aa_MAX_AU27 * Convert.ToDouble( lastIntensity ) / 100;
					eyChange = -Constants.ey_MAX_AU27 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					aaChange += Constants.aa_MAX_AU27 / Constants.AIntensity;
					eyChange += Constants.ey_MAX_AU27 / Constants.AIntensity;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU27 / Constants.AIntensity + "] " +
							   @"\SetReg[name= ey f=" + Constants.ey_MAX_AU27 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					aaChange += Constants.aa_MAX_AU27 / Constants.BIntensity;
					eyChange += Constants.ey_MAX_AU27 / Constants.BIntensity;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU27 / Constants.BIntensity + "] " +
							   @"\SetReg[name= ey f=" + Constants.ey_MAX_AU27 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					aaChange += Constants.aa_MAX_AU27 / Constants.CIntensity;
					eyChange += Constants.ey_MAX_AU27 / Constants.CIntensity;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU27 / Constants.CIntensity + "] " +
							   @"\SetReg[name= ey f=" + Constants.ey_MAX_AU27 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					aaChange += Constants.aa_MAX_AU27 / Constants.DIntensity;
					eyChange += Constants.ey_MAX_AU27 / Constants.DIntensity;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU27 / Constants.DIntensity + "] " +
							   @"\SetReg[name= ey f=" + Constants.ey_MAX_AU27 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					aaChange += Constants.aa_MAX_AU27;
					eyChange += Constants.ey_MAX_AU27;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU27 + "] " +
							   @"\SetReg[name= ey f=" + Constants.ey_MAX_AU27 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= aa f=0] " + @"\SetReg[name= ey f=0] ";
					break;
				default:
					aaChange += Constants.aa_MAX_AU27 * Convert.ToDouble( intensity ) / 100;
					eyChange += Constants.ey_MAX_AU27 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= aa f=" + Constants.aa_MAX_AU27 * Convert.ToDouble( intensity ) / 100 + "] " +
							   @"\SetReg[name= ey f=" + Constants.ey_MAX_AU27 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			aa += aaChange;
			ey += eyChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 28 (Lips Suck) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 28 (Lips Suck) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU28( string intensity, string lastIntensity )
		{
			double bChange = 0;
			double dChange = 0;
			double fChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					bChange = -Constants.b_MAX_AU28 / Constants.AIntensity;
					dChange = -Constants.d_MAX_AU28 / Constants.AIntensity;
					fChange = -Constants.f_MAX_AU28 / Constants.AIntensity;
					break;
				case ( "B" ):
					bChange = -Constants.b_MAX_AU28 / Constants.BIntensity;
					dChange = -Constants.d_MAX_AU28 / Constants.BIntensity;
					fChange = -Constants.f_MAX_AU28 / Constants.BIntensity;
					break;
				case ( "C" ):
					bChange = -Constants.b_MAX_AU28 / Constants.CIntensity;
					dChange = -Constants.d_MAX_AU28 / Constants.CIntensity;
					fChange = -Constants.f_MAX_AU28 / Constants.CIntensity;
					break;
				case ( "D" ):
					bChange = -Constants.b_MAX_AU28 / Constants.DIntensity;
					dChange = -Constants.d_MAX_AU28 / Constants.DIntensity;
					fChange = -Constants.f_MAX_AU28 / Constants.DIntensity;
					break;
				case ( "E" ):
					bChange = -Constants.b_MAX_AU28;
					dChange = -Constants.d_MAX_AU28;
					fChange = -Constants.f_MAX_AU28;
					break;
				case ( "0" ):
					break;
				default:
					bChange = -Constants.b_MAX_AU28 * Convert.ToDouble( lastIntensity ) / 100;
					dChange = -Constants.d_MAX_AU28 * Convert.ToDouble( lastIntensity ) / 100;
					fChange = -Constants.f_MAX_AU28 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					bChange += Constants.b_MAX_AU28 / Constants.AIntensity;
					dChange += Constants.d_MAX_AU28 / Constants.AIntensity;
					fChange += Constants.f_MAX_AU28 / Constants.AIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU28 / Constants.AIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU28 / Constants.AIntensity + "] " +
								@"\SetReg[name= f f=" + Constants.f_MAX_AU28 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					bChange += Constants.b_MAX_AU28 / Constants.BIntensity;
					dChange += Constants.d_MAX_AU28 / Constants.BIntensity;
					fChange += Constants.f_MAX_AU28 / Constants.BIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU28 / Constants.BIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU28 / Constants.BIntensity + "] " +
								@"\SetReg[name= f f=" + Constants.f_MAX_AU28 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					bChange += Constants.b_MAX_AU28 / Constants.CIntensity;
					dChange += Constants.d_MAX_AU28 / Constants.CIntensity;
					fChange += Constants.f_MAX_AU28 / Constants.CIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU28 / Constants.CIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU28 / Constants.CIntensity + "] " +
								@"\SetReg[name= f f=" + Constants.f_MAX_AU28 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					bChange += Constants.b_MAX_AU28 / Constants.DIntensity;
					dChange += Constants.d_MAX_AU28 / Constants.DIntensity;
					fChange += Constants.f_MAX_AU28 / Constants.DIntensity;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU28 / Constants.DIntensity + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU28 / Constants.DIntensity + "] " +
								@"\SetReg[name= f f=" + Constants.f_MAX_AU28 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					bChange += Constants.b_MAX_AU28;
					dChange += Constants.d_MAX_AU28;
					fChange += Constants.f_MAX_AU28;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU28 + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU28 + "] " +
								@"\SetReg[name= f f=" + Constants.f_MAX_AU28 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= b f=0] " +
								@"\SetReg[name= d f=0] " +
								@"\SetReg[name= f f=0] ";
					break;
				default:
					bChange += Constants.b_MAX_AU28 * Convert.ToDouble( intensity ) / 100;
					dChange += Constants.d_MAX_AU28 * Convert.ToDouble( intensity ) / 100;
					fChange += Constants.f_MAX_AU28 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= b f=" + Constants.b_MAX_AU28 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= d f=" + Constants.d_MAX_AU28 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= f f=" + Constants.f_MAX_AU28 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			b += bChange;
			d += dChange;
			f += fChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 38 (Nostril Dilator) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 38 (Nostril Dilator) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU38( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double nostrilL3tyChange = 0;
			double nostrilR3tyChange = 0;
			double nostrilL3txChange = 0;
			double nostrilR3txChange = 0;
			string hyperText = "";

			switch( lastLeftIntensity )
			{
				case ( "A" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU38 / Constants.AIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU38 / Constants.AIntensity;
					break;
				case ( "B" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU38 / Constants.BIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU38 / Constants.BIntensity;
					break;
				case ( "C" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU38 / Constants.CIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU38 / Constants.CIntensity;
					break;
				case ( "D" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU38 / Constants.DIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU38 / Constants.DIntensity;
					break;
				case ( "E" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU38;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU38;
					break;
				case ( "0" ):
					break;
				default:
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU38 * Convert.ToDouble( lastLeftIntensity ) / 100;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU38 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU38 / Constants.AIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU38 / Constants.AIntensity;
					break;
				case ( "B" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU38 / Constants.BIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU38 / Constants.BIntensity;
					break;
				case ( "C" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU38 / Constants.CIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU38 / Constants.CIntensity;
					break;
				case ( "D" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU38 / Constants.DIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU38 / Constants.DIntensity;
					break;
				case ( "E" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU38;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU38;
					break;
				case ( "0" ):
					break;
				default:
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU38 * Convert.ToDouble( lastRightIntensity ) / 100;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU38 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU38 / Constants.AIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU38 / Constants.AIntensity;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU38 / Constants.AIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU38 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU38 / Constants.BIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU38 / Constants.BIntensity;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU38 / Constants.BIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU38 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU38 / Constants.CIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU38 / Constants.CIntensity;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU38 / Constants.CIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU38 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU38 / Constants.DIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU38 / Constants.DIntensity;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU38 / Constants.DIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU38 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU38;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU38;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU38 + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU38 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= nostrilL3ty f=0] " +
								 @"\SetReg[name= nostrilL3tx f=0] ";
					break;
				default:
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU38 * Convert.ToDouble( leftIntensity ) / 100;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU38 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU38 * Convert.ToDouble( leftIntensity ) / 100 + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU38 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU38 / Constants.AIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU38 / Constants.AIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU38 / Constants.AIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU38 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU38 / Constants.BIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU38 / Constants.BIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU38 / Constants.BIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU38 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU38 / Constants.CIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU38 / Constants.CIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU38 / Constants.CIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU38 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU38 / Constants.DIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU38 / Constants.DIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU38 / Constants.DIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU38 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU38;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU38;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU38 + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU38 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= nostrilR3ty f=0] " +
								 @"\SetReg[name= nostrilR3tx f=0] ";
					break;
				default:
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU38 * Convert.ToDouble( rightIntensity ) / 100;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU38 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU38 * Convert.ToDouble( rightIntensity ) / 100 + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU38 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			nostrilL3ty += nostrilL3tyChange;
			nostrilR3ty += nostrilR3tyChange;
			nostrilL3tx += nostrilL3txChange;
			nostrilR3tx += nostrilR3txChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 39 (Nostril Compressor) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 39 (Nostril Compressor) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU39( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double nostrilL3tyChange = 0;
			double nostrilR3tyChange = 0;
			double nostrilL3txChange = 0;
			double nostrilR3txChange = 0;
			string hyperText = "";

			switch( lastLeftIntensity )
			{
				case ( "A" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU39 / Constants.AIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU39 / Constants.AIntensity;
					break;
				case ( "B" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU39 / Constants.BIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU39 / Constants.BIntensity;
					break;
				case ( "C" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU39 / Constants.CIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU39 / Constants.CIntensity;
					break;
				case ( "D" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU39 / Constants.DIntensity;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU39 / Constants.DIntensity;
					break;
				case ( "E" ):
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU39;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU39;
					break;
				case ( "0" ):
					break;
				default:
					nostrilL3tyChange = -Constants.nostrilL3ty_MAX_AU39 * Convert.ToDouble( lastLeftIntensity ) / 100;
					nostrilL3txChange = -Constants.nostrilL3tx_MAX_AU39 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU39 / Constants.AIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU39 / Constants.AIntensity;
					break;
				case ( "B" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU39 / Constants.BIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU39 / Constants.BIntensity;
					break;
				case ( "C" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU39 / Constants.CIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU39 / Constants.CIntensity;
					break;
				case ( "D" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU39 / Constants.DIntensity;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU39 / Constants.DIntensity;
					break;
				case ( "E" ):
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU39;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU39;
					break;
				case ( "0" ):
					break;
				default:
					nostrilR3tyChange = -Constants.nostrilR3ty_MAX_AU39 * Convert.ToDouble( lastRightIntensity ) / 100;
					nostrilR3txChange = -Constants.nostrilR3tx_MAX_AU39 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU39 / Constants.AIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU39 / Constants.AIntensity;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU39 / Constants.AIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU39 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU39 / Constants.BIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU39 / Constants.BIntensity;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU39 / Constants.BIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU39 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU39 / Constants.CIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU39 / Constants.CIntensity;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU39 / Constants.CIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU39 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU39 / Constants.DIntensity;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU39 / Constants.DIntensity;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU39 / Constants.DIntensity + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU39 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU39;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU39;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU39 + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU39 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= nostrilL3ty f=0] " +
								 @"\SetReg[name= nostrilL3tx f=0] ";
					break;
				default:
					nostrilL3tyChange += Constants.nostrilL3ty_MAX_AU39 * Convert.ToDouble( leftIntensity ) / 100;
					nostrilL3txChange += Constants.nostrilL3tx_MAX_AU39 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= nostrilL3ty f=" + Constants.nostrilL3ty_MAX_AU39 * Convert.ToDouble( leftIntensity ) / 100 + "] " +
								 @"\SetReg[name= nostrilL3tx f=" + Constants.nostrilL3tx_MAX_AU39 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU39 / Constants.AIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU39 / Constants.AIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU39 / Constants.AIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU39 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU39 / Constants.BIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU39 / Constants.BIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU39 / Constants.BIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU39 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU39 / Constants.CIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU39 / Constants.CIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU39 / Constants.CIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU39 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU39 / Constants.DIntensity;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU39 / Constants.DIntensity;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU39 / Constants.DIntensity + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU39 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU39;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU39;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU39 + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU39 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= nostrilR3ty f=0] " +
								 @"\SetReg[name= nostrilR3tx f=0] ";
					break;
				default:
					nostrilR3tyChange += Constants.nostrilR3ty_MAX_AU39 * Convert.ToDouble( rightIntensity ) / 100;
					nostrilR3txChange += Constants.nostrilR3tx_MAX_AU39 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= nostrilR3ty f=" + Constants.nostrilR3ty_MAX_AU39 * Convert.ToDouble( rightIntensity ) / 100 + "] " +
								 @"\SetReg[name= nostrilR3tx f=" + Constants.nostrilR3tx_MAX_AU39 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			nostrilL3ty += nostrilL3tyChange;
			nostrilR3ty += nostrilR3tyChange;
			nostrilL3tx += nostrilL3txChange;
			nostrilR3tx += nostrilR3txChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 41 (Glabella Lowerer) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 41 (Glabella Lowerer) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU41( string intensity, string lastIntensity )
		{
			double trustChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					trustChange = -Constants.trust_MAX_AU41 / Constants.AIntensity;
					break;
				case ( "B" ):
					trustChange = -Constants.trust_MAX_AU41 / Constants.BIntensity;
					break;
				case ( "C" ):
					trustChange = -Constants.trust_MAX_AU41 / Constants.CIntensity;
					break;
				case ( "D" ):
					trustChange = -Constants.trust_MAX_AU41 / Constants.DIntensity;
					break;
				case ( "E" ):
					trustChange = -Constants.trust_MAX_AU41;
					break;
				case ( "0" ):
					break;
				default:
					trustChange = -Constants.trust_MAX_AU41 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					trustChange += Constants.trust_MAX_AU41 / Constants.AIntensity;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU41 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					trustChange += Constants.trust_MAX_AU41 / Constants.BIntensity;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU41 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					trustChange += Constants.trust_MAX_AU41 / Constants.CIntensity;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU41 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					trustChange += Constants.trust_MAX_AU41 / Constants.DIntensity;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU41 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					trustChange += Constants.trust_MAX_AU41;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU41 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= trust f=0] ";
					break;
				default:
					trustChange += Constants.trust_MAX_AU41 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= trust f=" + Constants.trust_MAX_AU41 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			trust += trustChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 42 (Inner Eyebrow Lowerer) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 42 (Inner Eyebrow Lowerer) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU42( string intensity, string lastIntensity )
		{
			double MidBrowUDChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU42 / Constants.AIntensity;
					break;
				case ( "B" ):
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU42 / Constants.BIntensity;
					break;
				case ( "C" ):
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU42 / Constants.CIntensity;
					break;
				case ( "D" ):
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU42 / Constants.DIntensity;
					break;
				case ( "E" ):
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU42;
					break;
				case ( "0" ):
					break;
				default:
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU42 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU42 / Constants.AIntensity;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU42 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU42 / Constants.BIntensity;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU42 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU42 / Constants.CIntensity;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU42 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU42 / Constants.DIntensity;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU42 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU42;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU42 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= MidBrowUD f=0] ";
					break;
				default:
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU42 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU42 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			MidBrowUD += MidBrowUDChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 43 (Eye Closure) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 43 (Eye Closure) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU43( string intensity, string lastIntensity )
		{
			double blinkChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					blinkChange = -Constants.blink_MAX_AU43 / Constants.AIntensity;
					break;
				case ( "B" ):
					blinkChange = -Constants.blink_MAX_AU43 / Constants.BIntensity;
					break;
				case ( "C" ):
					blinkChange = -Constants.blink_MAX_AU43 / Constants.CIntensity;
					break;
				case ( "D" ):
					blinkChange = -Constants.blink_MAX_AU43 / Constants.DIntensity;
					break;
				case ( "E" ):
					blinkChange = -Constants.blink_MAX_AU43;
					break;
				case ( "0" ):
					break;
				default:
					blinkChange = -Constants.blink_MAX_AU43 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					blinkChange += Constants.blink_MAX_AU43 / Constants.AIntensity;
					hyperText = @"\SetReg[name= Blink f=" + Constants.blink_MAX_AU43 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					blinkChange += Constants.blink_MAX_AU43 / Constants.BIntensity;
					hyperText = @"\SetReg[name= Blink f=" + Constants.blink_MAX_AU43 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					blinkChange += Constants.blink_MAX_AU43 / Constants.CIntensity;
					hyperText = @"\SetReg[name= Blink f=" + Constants.blink_MAX_AU43 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					blinkChange += Constants.blink_MAX_AU43 / Constants.DIntensity;
					hyperText = @"\SetReg[name= Blink f=" + Constants.blink_MAX_AU43 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					blinkChange += Constants.blink_MAX_AU43;
					hyperText = @"\SetReg[name= Blink f=" + Constants.blink_MAX_AU43 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= Blink f=0] ";
					break;
				default:
					blinkChange += Constants.blink_MAX_AU43 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= Blink f=" + Constants.blink_MAX_AU43 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			blink += blinkChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 44 (Eyebrow Gatherer) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 44 (Eyebrow Gatherer) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU44( string intensity, string lastIntensity )
		{
			double eyes_sadChange = 0;
			double MidBrowUDChange = 0;
			double RBrowUDChange = 0;
			double LBrowUDChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					eyes_sadChange = -Constants.eyes_sad_MAX_AU44 / Constants.AIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU44 / Constants.AIntensity;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU44 / Constants.AIntensity;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU44 / Constants.AIntensity;
					break;
				case ( "B" ):
					eyes_sadChange = -Constants.eyes_sad_MAX_AU44 / Constants.BIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU44 / Constants.BIntensity;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU44 / Constants.BIntensity;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU44 / Constants.BIntensity;
					break;
				case ( "C" ):
					eyes_sadChange = -Constants.eyes_sad_MAX_AU44 / Constants.CIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU44 / Constants.CIntensity;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU44 / Constants.CIntensity;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU44 / Constants.CIntensity;
					break;
				case ( "D" ):
					eyes_sadChange = -Constants.eyes_sad_MAX_AU44 / Constants.DIntensity;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU44 / Constants.DIntensity;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU44 / Constants.DIntensity;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU44 / Constants.DIntensity;
					break;
				case ( "E" ):
					eyes_sadChange = -Constants.eyes_sad_MAX_AU44;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU44;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU44;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU44;
					break;
				case ( "0" ):
					break;
				default:
					eyes_sadChange = -Constants.eyes_sad_MAX_AU44 * Convert.ToDouble( lastIntensity ) / 100;
					MidBrowUDChange = -Constants.MidBrowUD_MAX_AU44 * Convert.ToDouble( lastIntensity ) / 100;
					RBrowUDChange = -Constants.RBrowUD_MAX_AU44 * Convert.ToDouble( lastIntensity ) / 100;
					LBrowUDChange = -Constants.LBrowUD_MAX_AU44 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					eyes_sadChange += Constants.eyes_sad_MAX_AU44 / Constants.AIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU44 / Constants.AIntensity;
					RBrowUDChange += Constants.RBrowUD_MAX_AU44 / Constants.AIntensity;
					LBrowUDChange += Constants.LBrowUD_MAX_AU44 / Constants.AIntensity;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU44 / Constants.AIntensity + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU44 / Constants.AIntensity + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU44 / Constants.AIntensity + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU44 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					eyes_sadChange += Constants.eyes_sad_MAX_AU44 / Constants.BIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU44 / Constants.BIntensity;
					RBrowUDChange += Constants.RBrowUD_MAX_AU44 / Constants.BIntensity;
					LBrowUDChange += Constants.LBrowUD_MAX_AU44 / Constants.BIntensity;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU44 / Constants.BIntensity + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU44 / Constants.BIntensity + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU44 / Constants.BIntensity + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU44 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					eyes_sadChange += Constants.eyes_sad_MAX_AU44 / Constants.CIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU44 / Constants.CIntensity;
					RBrowUDChange += Constants.RBrowUD_MAX_AU44 / Constants.CIntensity;
					LBrowUDChange += Constants.LBrowUD_MAX_AU44 / Constants.CIntensity;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU44 / Constants.CIntensity + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU44 / Constants.CIntensity + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU44 / Constants.CIntensity + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU44 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					eyes_sadChange += Constants.eyes_sad_MAX_AU44 / Constants.DIntensity;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU44 / Constants.DIntensity;
					RBrowUDChange += Constants.RBrowUD_MAX_AU44 / Constants.DIntensity;
					LBrowUDChange += Constants.LBrowUD_MAX_AU44 / Constants.DIntensity;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU44 / Constants.DIntensity + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU44 / Constants.DIntensity + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU44 / Constants.DIntensity + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU44 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					eyes_sadChange += Constants.eyes_sad_MAX_AU44;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU44;
					RBrowUDChange += Constants.RBrowUD_MAX_AU44;
					LBrowUDChange += Constants.LBrowUD_MAX_AU44;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU44 + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU44 + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU44 + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU44 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= eyes_sad f=0] " +
								@"\SetReg[name= MidBrowUD f=0] " +
								@"\SetReg[name= RBrowUD f=0] " +
								@"\SetReg[name= LBrowUD f=0] ";
					break;
				default:
					eyes_sadChange += Constants.eyes_sad_MAX_AU44 * Convert.ToDouble( intensity ) / 100;
					MidBrowUDChange += Constants.MidBrowUD_MAX_AU44 * Convert.ToDouble( intensity ) / 100;
					RBrowUDChange += Constants.RBrowUD_MAX_AU44 * Convert.ToDouble( intensity ) / 100;
					LBrowUDChange += Constants.LBrowUD_MAX_AU44 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= eyes_sad f=" + Constants.eyes_sad_MAX_AU44 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= MidBrowUD f=" + Constants.MidBrowUD_MAX_AU44 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= RBrowUD f=" + Constants.RBrowUD_MAX_AU44 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= LBrowUD f=" + Constants.LBrowUD_MAX_AU44 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			eyes_sad += eyes_sadChange;
			MidBrowUD += MidBrowUDChange;
			RBrowUD += RBrowUDChange;
			LBrowUD += LBrowUDChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 45 (Blink) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 45 (Blink) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU45( )
		{
			return @"\SetSwitch[switch= blinks state=CloseEye] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 46 (Wink) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="eye"> 
		///     The eye (i.e., side) on which the Action Unit will be activated
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 46 (Wink) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU46( string eye )
		{
			if( eye == "Left" )
				return @"\SetSwitch[switch= blinks state=winkleftfast] ";
			else
				return @"\SetSwitch[switch= blinks state=winkrightfast] ";

		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 51 (Head Turn Left) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 51 (Head Turn Left) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU51( string intensity, string lastIntensity )
		{
			double HeadTwistChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					HeadTwistChange = -Constants.HeadTwist_MAX_AU51 / Constants.AIntensity;
					break;
				case ( "B" ):
					HeadTwistChange = -Constants.HeadTwist_MAX_AU51 / Constants.BIntensity;
					break;
				case ( "C" ):
					HeadTwistChange = -Constants.HeadTwist_MAX_AU51 / Constants.CIntensity;
					break;
				case ( "D" ):
					HeadTwistChange = -Constants.HeadTwist_MAX_AU51 / Constants.DIntensity;
					break;
				case ( "E" ):
					HeadTwistChange = -Constants.HeadTwist_MAX_AU51;
					break;
				case ( "0" ):
					break;
				default:
					HeadTwistChange = -Constants.HeadTwist_MAX_AU51 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					HeadTwistChange += Constants.HeadTwist_MAX_AU51 / Constants.AIntensity;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU51 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					HeadTwistChange += Constants.HeadTwist_MAX_AU51 / Constants.BIntensity;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU51 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					HeadTwistChange += Constants.HeadTwist_MAX_AU51 / Constants.CIntensity;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU51 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					HeadTwistChange += Constants.HeadTwist_MAX_AU51 / Constants.DIntensity;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU51 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					HeadTwistChange += Constants.HeadTwist_MAX_AU51;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU51 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= HeadTwist f=0] ";
					break;
				default:
					HeadTwistChange += Constants.HeadTwist_MAX_AU51 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU51 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			//AU61(intensity, lastIntensity);

			HeadTwist += HeadTwistChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 52 (Head Turn Right) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 52 (Head Turn Right) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU52( string intensity, string lastIntensity )
		{
			double HeadTwistChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					HeadTwistChange = -Constants.HeadTwist_MAX_AU52 / Constants.AIntensity;
					break;
				case ( "B" ):
					HeadTwistChange = -Constants.HeadTwist_MAX_AU52 / Constants.BIntensity;
					break;
				case ( "C" ):
					HeadTwistChange = -Constants.HeadTwist_MAX_AU52 / Constants.CIntensity;
					break;
				case ( "D" ):
					HeadTwistChange = -Constants.HeadTwist_MAX_AU52 / Constants.DIntensity;
					break;
				case ( "E" ):
					HeadTwistChange = -Constants.HeadTwist_MAX_AU52;
					break;
				case ( "0" ):
					break;
				default:
					HeadTwistChange = -Constants.HeadTwist_MAX_AU52 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					HeadTwistChange += Constants.HeadTwist_MAX_AU52 / Constants.AIntensity;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU52 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					HeadTwistChange += Constants.HeadTwist_MAX_AU52 / Constants.BIntensity;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU52 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					HeadTwistChange += Constants.HeadTwist_MAX_AU52 / Constants.CIntensity;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU52 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					HeadTwistChange += Constants.HeadTwist_MAX_AU52 / Constants.DIntensity;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU52 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					HeadTwistChange += Constants.HeadTwist_MAX_AU52;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU52 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= HeadTwist f=0] ";
					break;
				default:
					HeadTwistChange += Constants.HeadTwist_MAX_AU52 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= HeadTwist f=" + Constants.HeadTwist_MAX_AU52 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			HeadTwist += HeadTwistChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 53 (Head Up) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 53 (Head Up) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU53( string intensity, string lastIntensity )
		{
			double HeadForwardChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU53 / Constants.AIntensity;
					break;
				case ( "B" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU53 / Constants.BIntensity;
					break;
				case ( "C" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU53 / Constants.CIntensity;
					break;
				case ( "D" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU53 / Constants.DIntensity;
					break;
				case ( "E" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU53;
					break;
				case ( "0" ):
					break;
				default:
					HeadForwardChange = -Constants.HeadForward_MAX_AU53 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU53 / Constants.AIntensity;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU53 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU53 / Constants.BIntensity;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU53 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU53 / Constants.CIntensity;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU53 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU53 / Constants.DIntensity;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU53 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU53;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU53 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= HeadForward f=0] ";
					break;
				default:
					HeadForwardChange += Constants.HeadForward_MAX_AU53 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU53 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			HeadForward += HeadForwardChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 54 (Head Down) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 54 (Head Down) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU54( string intensity, string lastIntensity )
		{
			double HeadForwardChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU54 / Constants.AIntensity;
					break;
				case ( "B" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU54 / Constants.BIntensity;
					break;
				case ( "C" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU54 / Constants.CIntensity;
					break;
				case ( "D" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU54 / Constants.DIntensity;
					break;
				case ( "E" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU54;
					break;
				case ( "0" ):
					break;
				default:
					HeadForwardChange = -Constants.HeadForward_MAX_AU54 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU54 / Constants.AIntensity;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU54 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU54 / Constants.BIntensity;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU54 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU54 / Constants.CIntensity;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU54 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU54 / Constants.DIntensity;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU54 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU54;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU54 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= HeadForward f=0] ";
					break;
				default:
					HeadForwardChange += Constants.HeadForward_MAX_AU54 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU54 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			HeadForward += HeadForwardChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 55 (Head Tilt Left) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 55 (Head Tilt Left) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU55( string intensity, string lastIntensity )
		{
			double HeadSideBendChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU55 / Constants.AIntensity;
					break;
				case ( "B" ):
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU55 / Constants.BIntensity;
					break;
				case ( "C" ):
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU55 / Constants.CIntensity;
					break;
				case ( "D" ):
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU55 / Constants.DIntensity;
					break;
				case ( "E" ):
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU55;
					break;
				case ( "0" ):
					break;
				default:
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU55 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU55 / Constants.AIntensity;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU55 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU55 / Constants.BIntensity;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU55 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU55 / Constants.CIntensity;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU55 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU55 / Constants.DIntensity;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU55 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU55;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU55 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= HeadSideBend f=0] ";
					break;
				default:
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU55 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU55 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			HeadSideBend += HeadSideBendChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 56 (Head Tilt Right) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 56 (Head Tilt Right) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU56( string intensity, string lastIntensity )
		{
			double HeadSideBendChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU56 / Constants.AIntensity;
					break;
				case ( "B" ):
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU56 / Constants.BIntensity;
					break;
				case ( "C" ):
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU56 / Constants.CIntensity;
					break;
				case ( "D" ):
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU56 / Constants.DIntensity;
					break;
				case ( "E" ):
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU56;
					break;
				case ( "0" ):
					break;
				default:
					HeadSideBendChange = -Constants.HeadSideBend_MAX_AU56 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU56 / Constants.AIntensity;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU56 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU56 / Constants.BIntensity;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU56 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU56 / Constants.CIntensity;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU56 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU56 / Constants.DIntensity;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU56 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU56;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU56 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= HeadSideBend f=0] ";
					break;
				default:
					HeadSideBendChange += Constants.HeadSideBend_MAX_AU56 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= HeadSideBend f=" + Constants.HeadSideBend_MAX_AU56 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			HeadSideBend += HeadSideBendChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 57 (Head Forward) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 57 (Head Forward) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU57( string intensity, string lastIntensity )
		{
			// To DO: intensities D and E are expressed the same.

			double HeadForwardChange = 0;
			double NeckForwardChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU57 / Constants.AIntensity;
					NeckForwardChange = -Constants.NeckForward_MAX_AU57 / Constants.AIntensity;
					break;
				case ( "B" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU57 / Constants.BIntensity;
					NeckForwardChange = -Constants.NeckForward_MAX_AU57 / Constants.BIntensity;
					break;
				case ( "C" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU57 / Constants.CIntensity;
					NeckForwardChange = -Constants.NeckForward_MAX_AU57 / Constants.CIntensity;
					break;
				case ( "D" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU57 / Constants.DIntensity;
					NeckForwardChange = -Constants.NeckForward_MAX_AU57 / Constants.DIntensity;
					break;
				case ( "E" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU57;
					NeckForwardChange = -Constants.NeckForward_MAX_AU57;
					break;
				case ( "0" ):
					break;
				default:
					HeadForwardChange = -Constants.HeadForward_MAX_AU57 * Convert.ToDouble( lastIntensity ) / 100;
					NeckForwardChange = -Constants.NeckForward_MAX_AU57 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU57 / Constants.AIntensity;
					NeckForwardChange += Constants.NeckForward_MAX_AU57 / Constants.AIntensity;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU57 / Constants.AIntensity + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU57 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU57 / Constants.BIntensity;
					NeckForwardChange += Constants.NeckForward_MAX_AU57 / Constants.BIntensity;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU57 / Constants.BIntensity + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU57 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU57 / Constants.CIntensity;
					NeckForwardChange += Constants.NeckForward_MAX_AU57 / Constants.CIntensity;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU57 / Constants.CIntensity + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU57 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU57 / Constants.DIntensity;
					NeckForwardChange += Constants.NeckForward_MAX_AU57 / Constants.DIntensity;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU57 / Constants.DIntensity + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU57 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU57;
					NeckForwardChange += Constants.NeckForward_MAX_AU57;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU57 + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU57 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= NeckForward f=0] " + @"\SetReg[name= HeadForward f=0] ";
					break;
				default:
					HeadForwardChange += Constants.HeadForward_MAX_AU57 * Convert.ToDouble( intensity ) / 100;
					NeckForwardChange += Constants.NeckForward_MAX_AU57 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU57 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU57 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			HeadForward += HeadForwardChange;
			NeckForward += NeckForwardChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 58 (Head Back) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <param name="lastIntensity"> 
		///     Previous intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 58 (Head Back) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU58( string intensity, string lastIntensity )
		{
			// To DO: intensities D and E are expressed the same.

			double HeadForwardChange = 0;
			double NeckForwardChange = 0;
			string hyperText = "";
			switch( lastIntensity )
			{
				case ( "A" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU58 / Constants.AIntensity;
					NeckForwardChange = -Constants.NeckForward_MAX_AU58 / Constants.AIntensity;
					break;
				case ( "B" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU58 / Constants.BIntensity;
					NeckForwardChange = -Constants.NeckForward_MAX_AU58 / Constants.BIntensity;
					break;
				case ( "C" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU58 / Constants.CIntensity;
					NeckForwardChange = -Constants.NeckForward_MAX_AU58 / Constants.CIntensity;
					break;
				case ( "D" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU58 / Constants.DIntensity;
					NeckForwardChange = -Constants.NeckForward_MAX_AU58 / Constants.DIntensity;
					break;
				case ( "E" ):
					HeadForwardChange = -Constants.HeadForward_MAX_AU58;
					NeckForwardChange = -Constants.NeckForward_MAX_AU58;
					break;
				case ( "0" ):
					break;
				default:
					HeadForwardChange = -Constants.HeadForward_MAX_AU58 * Convert.ToDouble( lastIntensity ) / 100;
					NeckForwardChange = -Constants.NeckForward_MAX_AU58 * Convert.ToDouble( lastIntensity ) / 100;
					break;
			}
			switch( intensity )
			{
				case ( "A" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU58 / Constants.AIntensity;
					NeckForwardChange += Constants.NeckForward_MAX_AU58 / Constants.AIntensity;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU58 / Constants.AIntensity + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU58 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU58 / Constants.BIntensity;
					NeckForwardChange += Constants.NeckForward_MAX_AU58 / Constants.BIntensity;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU58 / Constants.BIntensity + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU58 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU58 / Constants.CIntensity;
					NeckForwardChange += Constants.NeckForward_MAX_AU58 / Constants.CIntensity;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU58 / Constants.CIntensity + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU58 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU58 / Constants.DIntensity;
					NeckForwardChange += Constants.NeckForward_MAX_AU58 / Constants.DIntensity;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU58 / Constants.DIntensity + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU58 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					HeadForwardChange += Constants.HeadForward_MAX_AU58;
					NeckForwardChange += Constants.NeckForward_MAX_AU58;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU58 + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU58 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= NeckForward f=0] " + @"\SetReg[name= HeadForward f=0] ";
					break;
				default:
					HeadForwardChange += Constants.HeadForward_MAX_AU58 * Convert.ToDouble( intensity ) / 100;
					NeckForwardChange += Constants.NeckForward_MAX_AU58 * Convert.ToDouble( intensity ) / 100;
					hyperText = @"\SetReg[name= NeckForward f=" + Constants.NeckForward_MAX_AU58 * Convert.ToDouble( intensity ) / 100 + "] " +
								@"\SetReg[name= HeadForward f=" + Constants.HeadForward_MAX_AU58 * Convert.ToDouble( intensity ) / 100 + "] ";
					break;
			}

			HeadForward += HeadForwardChange;
			NeckForward += NeckForwardChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit M59 (Head Nod) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit M59 (Head Nod) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AUM59( )
		{
			return @"\SetSwitch[switch= stop state=gestures_on] " + @"\SetSwitch[switch= gestures state=nod] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit M60 (Head Shake) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="intensity"> 
		///     Intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit M60 (Head Shake) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AUM60( )
		{

			return @"\SetSwitch[switch= stop state=gestures_on] " + @"\SetSwitch[switch= gestures state=shake] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 61 (Eyes Turn Left) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 61 (Eyes Turn Left) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU61( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double LEyeBallLRChange = 0;
			double REyeBallLRChange = 0;
			string hyperText = "";

			switch( lastLeftIntensity )
			{
				case ( "A" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU61 / Constants.AIntensity;
					break;
				case ( "B" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU61 / Constants.BIntensity;
					break;
				case ( "C" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU61 / Constants.CIntensity;
					break;
				case ( "D" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU61 / Constants.DIntensity;
					break;
				case ( "E" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU61;
					break;
				case ( "0" ):
					break;
				default:
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU61 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU61 / Constants.AIntensity;
					break;
				case ( "B" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU61 / Constants.BIntensity;
					break;
				case ( "C" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU61 / Constants.CIntensity;
					break;
				case ( "D" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU61 / Constants.DIntensity;
					break;
				case ( "E" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU61;
					break;
				case ( "0" ):
					break;
				default:
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU61 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU61 / Constants.AIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU61 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU61 / Constants.BIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU61 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU61 / Constants.CIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU61 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU61 / Constants.DIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU61 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU61;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU61 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= LEyeBallLR f= 0] ";
					break;
				default:
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU61 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU61 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU61 / Constants.AIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU61 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU61 / Constants.BIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU61 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU61 / Constants.CIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU61 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU61 / Constants.DIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU61 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU61;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU61 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= REyeBallLR f= 0] ";
					break;
				default:
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU61 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU61 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			LEyeBallLR += LEyeBallLRChange;
			REyeBallLR += REyeBallLRChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 62 (Eyes Turn Right) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 62 (Eyes Turn Right) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU62( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double LEyeBallLRChange = 0;
			double REyeBallLRChange = 0;
			string hyperText = "";

			switch( lastLeftIntensity )
			{
				case ( "A" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU62 / Constants.AIntensity;
					break;
				case ( "B" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU62 / Constants.BIntensity;
					break;
				case ( "C" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU62 / Constants.CIntensity;
					break;
				case ( "D" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU62 / Constants.DIntensity;
					break;
				case ( "E" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU62;
					break;
				case ( "0" ):
					break;
				default:
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU62 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU62 / Constants.AIntensity;
					break;
				case ( "B" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU62 / Constants.BIntensity;
					break;
				case ( "C" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU62 / Constants.CIntensity;
					break;
				case ( "D" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU62 / Constants.DIntensity;
					break;
				case ( "E" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU62;
					break;
				case ( "0" ):
					break;
				default:
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU62 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU62 / Constants.AIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU62 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU62 / Constants.BIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU62 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU62 / Constants.CIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU62 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU62 / Constants.DIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU62 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU62;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU62 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= LEyeBallLR f= 0] ";
					break;
				default:
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU62 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU62 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU62 / Constants.AIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU62 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU62 / Constants.BIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU62 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU62 / Constants.CIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU62 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU62 / Constants.DIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU62 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU62;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU62 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= REyeBallLR f= 0] ";
					break;
				default:
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU62 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU62 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			LEyeBallLR += LEyeBallLRChange;
			REyeBallLR += REyeBallLRChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 63 (Eyes Up) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 63 (Eyes Up) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU63( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double LEyeBallUDChange = 0;
			double REyeBallUDChange = 0;
			string hyperText = "";

			switch( lastLeftIntensity )
			{
				case ( "A" ):
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU63 / Constants.AIntensity;
					break;
				case ( "B" ):
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU63 / Constants.BIntensity;
					break;
				case ( "C" ):
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU63 / Constants.CIntensity;
					break;
				case ( "D" ):
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU63 / Constants.DIntensity;
					break;
				case ( "E" ):
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU63;
					break;
				case ( "0" ):
					break;
				default:
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU63 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU63 / Constants.AIntensity;
					break;
				case ( "B" ):
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU63 / Constants.BIntensity;
					break;
				case ( "C" ):
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU63 / Constants.CIntensity;
					break;
				case ( "D" ):
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU63 / Constants.DIntensity;
					break;
				case ( "E" ):
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU63;
					break;
				case ( "0" ):
					break;
				default:
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU63 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU63 / Constants.AIntensity;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU63 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU63 / Constants.BIntensity;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU63 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU63 / Constants.CIntensity;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU63 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU63 / Constants.DIntensity;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU63 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU63;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU63 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= LEyeBallUD f= 0] ";
					break;
				default:
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU63 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU63 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU63 / Constants.AIntensity;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU63 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU63 / Constants.BIntensity;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU63 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU63 / Constants.CIntensity;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU63 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU63 / Constants.DIntensity;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU63 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU63;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU63 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= REyeBallUD f= 0] ";
					break;
				default:
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU63 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU63 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			LEyeBallUD += LEyeBallUDChange;
			REyeBallUD += REyeBallUDChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 64 (Eyes Down) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 64 (Eyes Down) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU64( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double LEyeBallUDChange = 0;
			double REyeBallUDChange = 0;
			string hyperText = "";

			switch( lastLeftIntensity )
			{
				case ( "A" ):
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU64 / Constants.AIntensity;
					break;
				case ( "B" ):
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU64 / Constants.BIntensity;
					break;
				case ( "C" ):
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU64 / Constants.CIntensity;
					break;
				case ( "D" ):
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU64 / Constants.DIntensity;
					break;
				case ( "E" ):
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU64;
					break;
				case ( "0" ):
					break;
				default:
					LEyeBallUDChange = -Constants.LEyeBallUD_MAX_AU64 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU64 / Constants.AIntensity;
					break;
				case ( "B" ):
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU64 / Constants.BIntensity;
					break;
				case ( "C" ):
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU64 / Constants.CIntensity;
					break;
				case ( "D" ):
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU64 / Constants.DIntensity;
					break;
				case ( "E" ):
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU64;
					break;
				case ( "0" ):
					break;
				default:
					REyeBallUDChange = -Constants.REyeBallUD_MAX_AU64 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU64 / Constants.AIntensity;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU64 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU64 / Constants.BIntensity;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU64 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU64 / Constants.CIntensity;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU64 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU64 / Constants.DIntensity;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU64 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU64;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU64 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= LEyeBallUD f= 0] ";
					break;
				default:
					LEyeBallUDChange += Constants.LEyeBallUD_MAX_AU64 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= LEyeBallUD f=" + Constants.LEyeBallUD_MAX_AU64 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU64 / Constants.AIntensity;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU64 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU64 / Constants.BIntensity;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU64 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU64 / Constants.CIntensity;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU64 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU64 / Constants.DIntensity;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU64 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU64;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU64 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= REyeBallUD f= 0] ";
					break;
				default:
					REyeBallUDChange += Constants.REyeBallUD_MAX_AU64 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= REyeBallUD f=" + Constants.REyeBallUD_MAX_AU64 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			LEyeBallUD += LEyeBallUDChange;
			REyeBallUD += REyeBallUDChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets Action Unit 64 (Eyes Down) on the Haptek face.
		/// </summary>
		/// <returns>
		///     Hypertext to resets Action Unit 64 (Eyes Down) on the Haptek face.
		/// </returns>
		/// <remarks></remarks>
		public string resetAU64( )
		{
			LEyeBallUD = 0;
			REyeBallUD = 0;

			return @"\SetReg[name= REyeBallUD f= 0] " + @"\SetReg[name= LEyeBallUD f= 0] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 65 (Walleye) on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 65 (Walleye) on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU65( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double LEyeBallLRChange = 0;
			double REyeBallLRChange = 0;
			string hyperText = "";

			switch( lastLeftIntensity )
			{
				case ( "A" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU65 / Constants.AIntensity;
					break;
				case ( "B" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU65 / Constants.BIntensity;
					break;
				case ( "C" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU65 / Constants.CIntensity;
					break;
				case ( "D" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU65 / Constants.DIntensity;
					break;
				case ( "E" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU65;
					break;
				case ( "0" ):
					break;
				default:
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU65 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU65 / Constants.AIntensity;
					break;
				case ( "B" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU65 / Constants.BIntensity;
					break;
				case ( "C" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU65 / Constants.CIntensity;
					break;
				case ( "D" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU65 / Constants.DIntensity;
					break;
				case ( "E" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU65;
					break;
				case ( "0" ):
					break;
				default:
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU65 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU65 / Constants.AIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU65 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU65 / Constants.BIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU65 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU65 / Constants.CIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU65 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU65 / Constants.DIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU65 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU65;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU65 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= LEyeBallLR f=0] ";
					break;
				default:
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU65 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU65 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU65 / Constants.AIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU65 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU65 / Constants.BIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU65 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU65 / Constants.CIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU65 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU65 / Constants.DIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU65 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU65;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU65 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= REyeBallLR f=0] ";
					break;
				default:
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU65 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU65 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			LEyeBallLR += LEyeBallLRChange;
			REyeBallLR += REyeBallLRChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maps Action Unit 66 on the Haptek face with the specified intensity.
		/// </summary>
		/// <param name="leftIntensity"> 
		///     Left intensity of Action Unit activation
		/// </param>
		/// <param name="rightIntensity"> 
		///     Right intensity of Action Unit activation
		/// </param>
		/// <param name="lastLeftIntensity"> 
		///     Previous left intensity of Action Unit activation
		/// </param>
		/// <param name="lastRightIntensity"> 
		///     Previous right intensity of Action Unit activation
		/// </param>
		/// <returns>
		///     Hypertext to map Action Unit 66 on the Haptek face
		/// </returns>
		/// <remarks></remarks>
		public string AU66( string leftIntensity, string rightIntensity, string lastLeftIntensity, string lastRightIntensity )
		{
			double LEyeBallLRChange = 0;
			double REyeBallLRChange = 0;
			string hyperText = "";

			switch( lastLeftIntensity )
			{
				case ( "A" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU66 / Constants.AIntensity;
					break;
				case ( "B" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU66 / Constants.BIntensity;
					break;
				case ( "C" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU66 / Constants.CIntensity;
					break;
				case ( "D" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU66 / Constants.DIntensity;
					break;
				case ( "E" ):
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU66;
					break;
				case ( "0" ):
					break;
				default:
					LEyeBallLRChange = -Constants.LEyeBallLR_MAX_AU66 * Convert.ToDouble( lastLeftIntensity ) / 100;
					break;
			}

			switch( lastRightIntensity )
			{
				case ( "A" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU66 / Constants.AIntensity;
					break;
				case ( "B" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU66 / Constants.BIntensity;
					break;
				case ( "C" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU66 / Constants.CIntensity;
					break;
				case ( "D" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU66 / Constants.DIntensity;
					break;
				case ( "E" ):
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU66;
					break;
				case ( "0" ):
					break;
				default:
					REyeBallLRChange = -Constants.REyeBallLR_MAX_AU66 * Convert.ToDouble( lastRightIntensity ) / 100;
					break;
			}

			switch( leftIntensity )
			{
				case ( "A" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU66 / Constants.AIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU66 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU66 / Constants.BIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU66 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU66 / Constants.CIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU66 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU66 / Constants.DIntensity;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU66 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU66;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU66 + "] ";
					break;
				case ( "0" ):
					hyperText = @"\SetReg[name= LEyeBallLR f=0] ";
					break;
				default:
					LEyeBallLRChange += Constants.LEyeBallLR_MAX_AU66 * Convert.ToDouble( leftIntensity ) / 100;
					hyperText = @"\SetReg[name= LEyeBallLR f=" + Constants.LEyeBallLR_MAX_AU66 * Convert.ToDouble( leftIntensity ) / 100 + "] ";
					break;
			}

			switch( rightIntensity )
			{
				case ( "A" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU66 / Constants.AIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU66 / Constants.AIntensity + "] ";
					break;
				case ( "B" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU66 / Constants.BIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU66 / Constants.BIntensity + "] ";
					break;
				case ( "C" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU66 / Constants.CIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU66 / Constants.CIntensity + "] ";
					break;
				case ( "D" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU66 / Constants.DIntensity;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU66 / Constants.DIntensity + "] ";
					break;
				case ( "E" ):
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU66;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU66 + "] ";
					break;
				case ( "0" ):
					hyperText += @"\SetReg[name= REyeBallLR f=0] ";
					break;
				default:
					REyeBallLRChange += Constants.REyeBallLR_MAX_AU66 * Convert.ToDouble( rightIntensity ) / 100;
					hyperText += @"\SetReg[name= REyeBallLR f=" + Constants.REyeBallLR_MAX_AU66 * Convert.ToDouble( rightIntensity ) / 100 + "] ";
					break;
			}

			LEyeBallLR += LEyeBallLRChange;
			REyeBallLR += REyeBallLRChange;
			return hyperText;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets all the upper-face Action Units on the Haptek face.
		/// </summary>
		/// <returns>
		///     Hypertext to reset all the upper-face Action Units on the Haptek face.
		/// </returns>
		/// <remarks></remarks>
		public string resetUpperAUs( )
		{
			MidBrowUD = 0;
			LBrowUD = 0;
			RBrowUD = 0;
			eyes_sad = 0;
			blink = 0;
			trust = 0;
			distrust = 0;

			return @"\SetReg[name= MidBrowUD f= " + 0 + "] " +
				   @"\SetReg[name= LBrowUD f= " + 0 + "] " +
				   @"\SetReg[name= RBrowUD f= " + 0 + "] " +
				   @"\SetReg[name= eyes_sad f= " + 0 + "] " +
				   @"\SetReg[name= trust f= " + 0 + "] " +
				   @"\SetReg[name= distrust f= " + 0 + "] " +
				   @"\SetReg[name= blink f= " + 0 + "] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets all the lower-face Action Units on the Haptek face.
		/// </summary>
		/// <returns>
		///     Hypertext to reset all the lower-face Action Units on the Haptek face.
		/// </returns>
		/// <remarks></remarks>
		public string resetLowerAUs( )
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
			f = 0;
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

			return @"\SetReg[name= lipcornerL3ty f= " + 0 + "] " +
				   @"\SetReg[name= lipcornerR3ty f= " + 0 + "] " +
				   @"\SetReg[name= smile3 f= " + 0 + "] " +
				   @"\SetReg[name= kiss f= " + 0 + "] " +
				   @"\SetReg[name= nostrilL3ty f= " + 0 + "] " +
				   @"\SetReg[name= nostrilR3ty f= " + 0 + "] " +
				   @"\SetReg[name= nostrilL3tx f= " + 0 + "] " +
				   @"\SetReg[name= nostrilR3tx f= " + 0 + "] " +
				   @"\SetReg[name= uh f= " + 0 + "] " +
				   @"\SetReg[name= ow f= " + 0 + "] " +
				   @"\SetReg[name= d f= " + 0 + "] " +
				   @"\SetReg[name= iy f= " + 0 + "] " +
				   @"\SetReg[name= uw f= " + 0 + "] " +
				   @"\SetReg[name= smirk f= " + 0 + "] " +
				   @"\SetReg[name= smirkL f= " + 0 + "] " +
				   @"\SetReg[name= mouth2ty f= " + 0 + "] " +
				   @"\SetReg[name= th f= " + 0 + "] " +
				   @"\SetReg[name= aa f= " + 0 + "] " +
				   @"\SetReg[name= b f= " + 0 + "] " +
				   @"\SetReg[name= ch f= " + 0 + "] " +
				   @"\SetReg[name= ey f= " + 0 + "] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets all the head and eye Action Units on the Haptek face.
		/// </summary>
		/// <returns>
		///     Hypertext to reset all the head and eye Action Units on the Haptek face.
		/// </returns>
		/// <remarks></remarks>
		public string resetHeadAndEyeAUs( )
		{
			LEyeBallLR = 0;
			REyeBallLR = 0;
			LEyeBallUD = 0;
			REyeBallUD = 0;
			HeadTwist = 0;
			HeadForward = 0;
			HeadSideBend = 0;
			NeckForward = 0;

			return @"\SetReg[name= HeadTwist f= " + 0 + "] " +
				   @"\SetReg[name= HeadForward f= " + 0 + "] " +
				   @"\SetReg[name= HeadSideBend f= " + 0 + "] " +
				   @"\SetReg[name= NeckForward f= " + 0 + "] " +
				   @"\SetReg[name= LEyeBallLR f= " + 0 + "] " +
				   @"\SetReg[name= REyeBallLR f= " + 0 + "] " +
				   @"\SetReg[name= LEyeBallUD f= " + 0 + "] " +
				   @"\SetReg[name= REyeBallUD f= " + 0 + "] ";
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Resets all the Action Units on the Haptek face.
		/// </summary>
		/// <returns>
		///     Hypertext to reset all the Action Units on the Haptek face.
		/// </returns>
		/// <remarks></remarks>
		public string resetAllAUs( )
		{
			return resetHeadAndEyeAUs( ) + resetLowerAUs( ) + resetUpperAUs( );
		}

		#endregion

		#region Method to generate the hyper-text for actvating the AUs 
		// Generates the hyper-text for the current values of the AUs

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Generates the hyper-text for the current values of the AUs
		/// </summary>
		/// <returns>
		///     Hypertext for the current values of the AUs
		/// </returns>
		/// <remarks></remarks>
		public string generateFace( )
		{
			return @"\SetReg[name= MidBrowUD f= " + MidBrowUD + "] " +
				   @"\SetReg[name= LBrowUD f= " + LBrowUD + "] " +
				   @"\SetReg[name= RBrowUD f= " + RBrowUD + "] " +
				   @"\SetReg[name= eyes_sad f= " + eyes_sad + "] " +
				   @"\SetReg[name= lipcornerL3ty f= " + lipcornerL3ty + "] " +
				   @"\SetReg[name= lipcornerR3ty f= " + lipcornerR3ty + "] " +
				   @"\SetReg[name= trust f= " + trust + "] " +
				   @"\SetReg[name= distrust f= " + distrust + "] " +
				   @"\SetReg[name= smile3 f= " + smile3 + "] " +
				   @"\SetReg[name= kiss f= " + kiss + "] " +
				   @"\SetReg[name= nostrilL3ty f= " + nostrilL3ty + "] " +
				   @"\SetReg[name= nostrilR3ty f= " + nostrilR3ty + "] " +
				   @"\SetReg[name= nostrilL3tx f= " + nostrilL3tx + "] " +
				   @"\SetReg[name= nostrilR3tx f= " + nostrilR3tx + "] " +
				   @"\SetReg[name= uh f= " + uh + "] " +
				   @"\SetReg[name= ow f= " + ow + "] " +
				   @"\SetReg[name= d f= " + d + "] " +
				   @"\SetReg[name= f f= " + f + "] " +
				   @"\SetReg[name= iy f= " + iy + "] " +
				   @"\SetReg[name= uw f= " + uw + "] " +
				   @"\SetReg[name= smirk f= " + smirk + "] " +
				   @"\SetReg[name= smirkL f= " + smirkL + "] " +
				   @"\SetReg[name= mouth2ty f= " + mouth2ty + "] " +
				   @"\SetReg[name= th f= " + th + "] " +
				   @"\SetReg[name= aa f= " + aa + "] " +
				   @"\SetReg[name= b f= " + b + "] " +
				   @"\SetReg[name= ch f= " + ch + "] " +
				   @"\SetReg[name= ey f= " + ey + "] " +
				   @"\SetReg[name= blink f= " + blink + "] " +
				   @"\SetReg[name= HeadTwist f= " + HeadTwist + "] " +
				   @"\SetReg[name= HeadForward f= " + HeadForward + "] " +
				   @"\SetReg[name= HeadSideBend f= " + HeadSideBend + "] " +
				   @"\SetReg[name= NeckForward f= " + NeckForward + "] " +
				   @"\SetReg[name= LEyeBallLR f= " + LEyeBallLR + "] " +
				   @"\SetReg[name= REyeBallLR f= " + REyeBallLR + "] " +
				   @"\SetReg[name= LEyeBallUD f= " + LEyeBallUD + "] " +
				   @"\SetReg[name= REyeBallUD f= " + REyeBallUD + "] ";

		}

		#endregion
	}
}
