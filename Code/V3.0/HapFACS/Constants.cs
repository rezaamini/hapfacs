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
	///     Includes all the constant variables used thoughout the HapFACS
	/// </summary>
	/// <remarks></remarks>
	public static class Constants
	{
		#region Constants showing the maximum possible intensity of each register for each individual AU
		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     The number by which the intensity of the AU will be divided when it is activated by intensity "A"
		/// </summary>
		/// <remarks></remarks>
		public static double AIntensity = 6.66; // 15% intensity

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     The number by which the intensity of the AU will be divided when it is activated by intensity "B"
		/// </summary>
		/// <remarks></remarks>
		public static double BIntensity = 3.33; // 30% intensity

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     The number by which the intensity of the AU will be divided when it is activated by intensity "C"
		/// </summary>
		/// <remarks></remarks>
		public static double CIntensity = 1.81; // 55% intensity

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     The number by which the intensity of the AU will be divided when it is activated by intensity "D"
		/// </summary>
		/// <remarks></remarks>
		public static double DIntensity = 1.17; // 85% intensity

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of MidBrowUD register in AU1
		/// </summary>
		/// <remarks></remarks>
		public static double MidBrowUD_MAX_AU1 = -2.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of LBrowUD register in AU2
		/// </summary>
		/// <remarks></remarks>
		public static double LBrowUD_MAX_AU2 = -1.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of RBrowUD register in AU2
		/// </summary>
		/// <remarks></remarks>
		public static double RBrowUD_MAX_AU2 = -1.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of MidBrowUD register in AU4
		/// </summary>
		/// <remarks></remarks>
		public static double MidBrowUD_MAX_AU4 = 3.00;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of LBrowUD register in AU4
		/// </summary>
		/// <remarks></remarks>
		public static double LBrowUD_MAX_AU4 = 0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of RBrowUD register in AU4
		/// </summary>
		/// <remarks></remarks>
		public static double RBrowUD_MAX_AU4 = 0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of eyes_sad register in AU4
		/// </summary>
		/// <remarks></remarks>
		public static double eyes_sad_MAX_AU4 = 1.25;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of trust register in AU5
		/// </summary>
		/// <remarks></remarks>
		public static double trust_MAX_AU5 = -0.85;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of lipcornerL3ty register in AU6
		/// </summary>
		/// <remarks></remarks>
		public static double lipcornerL3ty_MAX_AU6 = -0.70;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of lipcornerR3ty register in AU6
		/// </summary>
		/// <remarks></remarks>
		public static double lipcornerR3ty_MAX_AU6 = -0.70;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of smile3 register in AU6
		/// </summary>
		/// <remarks></remarks>
		public static double smile3_MAX_AU6 = 0.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of kiss register in AU6
		/// </summary>
		/// <remarks></remarks>
		public static double kiss_MAX_AU6 = 0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of distrust register in AU7
		/// </summary>
		/// <remarks></remarks>
		public static double distrust_MAX_AU7 = 1.20;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of b register in AU8
		/// </summary>
		/// <remarks></remarks>
		public static double b_MAX_AU8 = 0.75;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3ty register in AU9
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3ty_MAX_AU9 = 0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3ty register in AU9
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3ty_MAX_AU9 = 0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3tx register in AU9
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3tx_MAX_AU9 = 0.10;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3tx register in AU9
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3tx_MAX_AU9 = -0.10;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of MidBrowUD register in AU9
		/// </summary>
		/// <remarks></remarks>
		public static double MidBrowUD_MAX_AU9 = 1.00;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of uh register in AU10
		/// </summary>
		/// <remarks></remarks>
		public static double uh_MAX_AU10 = -1.70; // Previous value: -1.10;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of ow register in AU10
		/// </summary>
		/// <remarks></remarks>
		public static double ow_MAX_AU10 = 0.61;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of d register in AU10
		/// </summary>
		/// <remarks></remarks>
		public static double d_MAX_AU10 = 2.76; // Previous value: 1.40;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of iy register in AU10
		/// </summary>
		/// <remarks></remarks>
		public static double iy_MAX_AU10 = -0.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3ty register in AU10
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3ty_MAX_AU10 = 0.45;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3ty register in AU10
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3ty_MAX_AU10 = 0.45;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3tx register in AU10
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3tx_MAX_AU10 = 0.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3ty register in AU10
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3tx_MAX_AU10 = -0.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3tx register in AU11
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3tx_MAX_AU11 = 0.20;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3tx register in AU11
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3tx_MAX_AU11 = -0.20;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of uh register in AU11
		/// </summary>
		/// <remarks></remarks>
		public static double uh_MAX_AU11 = -0.40;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of d register in AU11
		/// </summary>
		/// <remarks></remarks>
		public static double d_MAX_AU11 = 0.31;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of smile3 register in AU12
		/// </summary>
		/// <remarks></remarks>
		public static double smile3_MAX_AU12 = 0.50;
		//public static double ey_MAX_AU12 = -0.80;
		//public static double iy_MAX_AU12 = 1.70;
		//public static double lipcornerL3ty_MAX_AU12 = 0.80;
		//public static double lipcornerR3ty_MAX_AU12 = 0.80;
		//public static double nostrilL3tx_MAX_AU12 = -0.25;
		//public static double nostrilR3tx_MAX_AU12 = 0.25;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of lipcornerL3ty register in AU13
		/// </summary>
		/// <remarks></remarks>
		public static double lipcornerL3ty_MAX_AU13 = 1.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of lipcornerR3ty register in AU13
		/// </summary>
		/// <remarks></remarks>
		public static double lipcornerR3ty_MAX_AU13 = 1.30;
		//public static double uw_MAX_AU13 = -0.25;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of smirk register in AU14
		/// </summary>
		/// <remarks></remarks>
		public static double smirk_MAX_AU14 = 0.45;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of smirkL register in AU14
		/// </summary>
		/// <remarks></remarks>
		public static double smirkL_MAX_AU14 = 0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of lipcornerL3ty register in AU15
		/// </summary>
		/// <remarks></remarks>
		public static double lipcornerL3ty_MAX_AU15 = -1.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of lipcornerR3ty register in AU15
		/// </summary>
		/// <remarks></remarks>
		public static double lipcornerR3ty_MAX_AU15 = -1.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of th register in AU16
		/// </summary>
		/// <remarks></remarks>
		public static double th_MAX_AU16 = 0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of lipcornerL3ty register in AU17
		/// </summary>
		/// <remarks></remarks>
		public static double lipcornerL3ty_MAX_AU17 = -0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of lipcornerR3ty register in AU17
		/// </summary>
		/// <remarks></remarks>
		public static double lipcornerR3ty_MAX_AU17 = -0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of aa register in AU17
		/// </summary>
		/// <remarks></remarks>
		public static double aa_MAX_AU17 = -0.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of ow register in AU17
		/// </summary>
		/// <remarks></remarks>
		public static double ow_MAX_AU17 = -0.40;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of mouth2ty register in AU17
		/// </summary>
		/// <remarks></remarks>
		public static double mouth2ty_MAX_AU17 = 0.20;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of kiss register in AU18
		/// </summary>
		/// <remarks></remarks>
		public static double kiss_MAX_AU18 = 1.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of lipcornerL3ty register in AU20
		/// </summary>
		/// <remarks></remarks>
		public static double lipcornerL3ty_MAX_AU20 = -0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of lipcornerR3ty register in AU20
		/// </summary>
		/// <remarks></remarks>
		public static double lipcornerR3ty_MAX_AU20 = -0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of b register in AU20
		/// </summary>
		/// <remarks></remarks>
		public static double b_MAX_AU20 = 0.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of ow register in AU20
		/// </summary>
		/// <remarks></remarks>
		public static double ow_MAX_AU20 = -1.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of mouth2ty register in AU20
		/// </summary>
		/// <remarks></remarks>
		public static double mouth2ty_MAX_AU20 = -0.40;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3tx register in AU20
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3tx_MAX_AU20 = -0.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3tx register in AU20
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3tx_MAX_AU20 = 0.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of th register in AU20
		/// </summary>
		/// <remarks></remarks>
		public static double th_MAX_AU20 = -0.40;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of ch register in AU22
		/// </summary>
		/// <remarks></remarks>
		public static double ch_MAX_AU22 = 1.00;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of uw register in AU22
		/// </summary>
		/// <remarks></remarks>
		public static double uw_MAX_AU22 = 0.15;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of b register in AU23
		/// </summary>
		/// <remarks></remarks>
		public static double b_MAX_AU23 = 1.40;
		//public static double kiss_MAX_AU23 = 0.65;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of b register in AU24
		/// </summary>
		/// <remarks></remarks>
		public static double b_MAX_AU24 = 0.90;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of kiss register in AU24
		/// </summary>
		/// <remarks></remarks>
		public static double kiss_MAX_AU24 = 0.25;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of ch register in AU25
		/// </summary>
		/// <remarks></remarks>
		public static double ch_MAX_AU25 = 0.80;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3ty register in AU25
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3ty_MAX_AU25 = 0.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3ty register in AU25
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3ty_MAX_AU25 = 0.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of aa register in AU26
		/// </summary>
		/// <remarks></remarks>
		public static double aa_MAX_AU26 = 1.10;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of aa register in AU27
		/// </summary>
		/// <remarks></remarks>
		public static double aa_MAX_AU27 = 1.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of ey register in AU27
		/// </summary>
		/// <remarks></remarks>
		public static double ey_MAX_AU27 = 1.20;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of b register in AU28
		/// </summary>
		/// <remarks></remarks>
		public static double b_MAX_AU28 = 1.9;// Previous amount 1.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of d register in AU28
		/// </summary>
		/// <remarks></remarks>
		public static double d_MAX_AU28 = -0.7;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of f register in AU28
		/// </summary>
		/// <remarks></remarks>
		public static double f_MAX_AU28 = 0.28;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3ty register in AU38
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3ty_MAX_AU38 = 0.30;
		
		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3ty register in AU38
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3ty_MAX_AU38 = 0.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3tx register in AU38
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3tx_MAX_AU38 = 0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3tx register in AU38
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3tx_MAX_AU38 = -0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3ty register in AU39
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3ty_MAX_AU39 = -0.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3ty register in AU39
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3ty_MAX_AU39 = -0.30;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilL3tx register in AU39
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilL3tx_MAX_AU39 = -0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of nostrilR3tx register in AU39
		/// </summary>
		/// <remarks></remarks>
		public static double nostrilR3tx_MAX_AU39 = 0.60;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of trust register in AU41
		/// </summary>
		/// <remarks></remarks>
		public static double trust_MAX_AU41 = 1.00;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "MidBrowUD" register in AU42
		/// </summary>
		/// <remarks></remarks>
		public static double MidBrowUD_MAX_AU42 = 1.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "blink" register in AU43
		/// </summary>
		/// <remarks></remarks>
		public static double blink_MAX_AU43 = 1.40;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "MidBrowUD" register in AU44
		/// </summary>
		/// <remarks></remarks>
		public static double MidBrowUD_MAX_AU44 = 1.00;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "LBrowUD" register in AU44
		/// </summary>
		/// <remarks></remarks>
		public static double LBrowUD_MAX_AU44 = -0.80;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "RBrowUD" register in AU44
		/// </summary>
		/// <remarks></remarks>
		public static double RBrowUD_MAX_AU44 = -0.90;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "eyes_sad" register in AU44
		/// </summary>
		/// <remarks></remarks>
		public static double eyes_sad_MAX_AU44 = 1.40;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "HeadTwist" register in AU51
		/// </summary>
		/// <remarks></remarks>
		public static double HeadTwist_MAX_AU51 = -0.62;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "HeadTwist" register in AU52
		/// </summary>
		/// <remarks></remarks>
		public static double HeadTwist_MAX_AU52 = 0.62;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "HeadForward" register in AU53
		/// </summary>
		/// <remarks></remarks>
		public static double HeadForward_MAX_AU53 = -0.36;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "HeadForward" register in AU54
		/// </summary>
		/// <remarks></remarks>
		public static double HeadForward_MAX_AU54 = 0.33;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "HeadSideBend" register in AU55
		/// </summary>
		/// <remarks></remarks>
		public static double HeadSideBend_MAX_AU55 = 0.35;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "HeadSideBend" register in AU56
		/// </summary>
		/// <remarks></remarks>
		public static double HeadSideBend_MAX_AU56 = -0.35;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "HeadForward" register in AU57
		/// </summary>
		/// <remarks></remarks>
		public static double HeadForward_MAX_AU57 = -0.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "NeckForward" register in AU57
		/// </summary>
		/// <remarks></remarks>
		public static double NeckForward_MAX_AU57 = 0.40;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "HeadForward" register in AU58
		/// </summary>
		/// <remarks></remarks>
		public static double HeadForward_MAX_AU58 = 0.50;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "NeckForward" register in AU58
		/// </summary>
		/// <remarks></remarks>
		public static double NeckForward_MAX_AU58 = -0.35;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "LEyeBallLR" register in AU61
		/// </summary>
		/// <remarks></remarks>
		public static double LEyeBallLR_MAX_AU61 = -0.45;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "REyeBallLR" register in AU61
		/// </summary>
		/// <remarks></remarks>
		public static double REyeBallLR_MAX_AU61 = -0.45;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "LEyeBallLR" register in AU62
		/// </summary>
		/// <remarks></remarks>
		public static double LEyeBallLR_MAX_AU62 = 0.45;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "REyeBallLR" register in AU62
		/// </summary>
		/// <remarks></remarks>
		public static double REyeBallLR_MAX_AU62 = 0.45;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "LEyeBallUD" register in AU63
		/// </summary>
		/// <remarks></remarks>
		public static double LEyeBallUD_MAX_AU63 = -0.18;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "REyeBallUD" register in AU63
		/// </summary>
		/// <remarks></remarks>
		public static double REyeBallUD_MAX_AU63 = -0.18;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "LEyeBallUD" register in AU64
		/// </summary>
		/// <remarks></remarks>
		public static double LEyeBallUD_MAX_AU64 = 0.18;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "REyeBallUD" register in AU64
		/// </summary>
		/// <remarks></remarks>
		public static double REyeBallUD_MAX_AU64 = 0.18;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "LEyeBallLR" register in AU65
		/// </summary>
		/// <remarks></remarks>
		public static double LEyeBallLR_MAX_AU65 = 0.38;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "REyeBallLR" register in AU65
		/// </summary>
		/// <remarks></remarks>
		public static double REyeBallLR_MAX_AU65 = -0.38;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "LEyeBallLR" register in AU66
		/// </summary>
		/// <remarks></remarks>
		public static double LEyeBallLR_MAX_AU66 = -0.45;

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Maximum intensity of "REyeBallLR" register in AU66
		/// </summary>
		/// <remarks></remarks>
		public static double REyeBallLR_MAX_AU66 = 0.45;

		#endregion
	}
}



