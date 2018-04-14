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
	///     HapFACSEmotion object simulates the Emotional FACS (EmFACS) emotions
	/// </summary>
	/// <remarks></remarks>
    class HapFACSEmotion
    {
		HapFACS hapFACS = new HapFACS( );

		#region Methods which similate different emotions on the character's face using combinations of AUs

		/// <author>
        ///     Reza Amini (ramin001@fiu.edu) 
        ///     Affective Social Computing lab at Florida International University
        /// </author>
        /// <summary>
		///     Maps EmFACS happiness on the Haptek face with the specified intensity. Highest level of happiness is the combination of AU6B, AU12E, and AU25B.
        /// </summary>
        /// <param name="intensity"> 
        ///     Intensity of happiness
        /// </param>
        /// <param name="lastIntensity"> 
        ///     Previous intensity of happiness
        /// </param>
        /// <returns>
        ///     Hypertext to map happiness on the Haptek face
        /// </returns>
        /// <remarks></remarks>
        public string Happiness(string intensity, string lastIntensity)
        {
            string hyperText = "";
			
			// Activate AU6 with maximum intensity of B
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU6( "A", lastIntensity );
			else if( intensity == "C" || intensity == "B" )
				hyperText += hapFACS.AU6( "A", lastIntensity );
			else if (intensity == "A" || intensity == "0")
				hyperText += hapFACS.AU6( "0", lastIntensity );
			else // Here, we scale the AU6 maximum to the B instead of E
				hyperText += hapFACS.AU6((Convert.ToDouble(intensity) / Constants.BIntensity).ToString(), lastIntensity);
			
			// Activate AU12
            hyperText += hapFACS.AU12(intensity, lastIntensity);

			// Activate AU25 with maximum intensity of B
			if( intensity == "E" || intensity == "D")
				hyperText += hapFACS.AU25("C", lastIntensity);
			else if (intensity == "C" || intensity == "B")
				hyperText += hapFACS.AU25( "B", lastIntensity );
			else if( intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU25( intensity, lastIntensity );
			else // Here, we scale the AU25 maximum to the B instead of E
				hyperText += hapFACS.AU25( ( Convert.ToDouble( intensity ) / Constants.CIntensity ).ToString( ), lastIntensity );

			hapFACS.resetLowerAUs( );
			hapFACS.resetUpperAUs( );
			hapFACS.resetHeadAndEyeAUs( );
			return hyperText;
        }

        /// <author>
        ///     Reza Amini (ramin001@fiu.edu) 
        ///     Affective Social Computing lab at Florida International University
        /// </author>
        /// <summary>
		///     Maps EmFACS sadness on the Haptek face with the specified intensity. Highest level of sadness is the combination of AU1E, AU4E, and AU15E.
        /// </summary>
        /// <param name="intensity"> 
        ///     Intensity of sadness
        /// </param>
        /// <param name="lastIntensity"> 
        ///     Previous intensity of sadness
        /// </param>
        /// <returns>
        ///     Hypertext to map sadness on the Haptek face
        /// </returns>
        /// <remarks></remarks>
        public string Sadness(string intensity, string lastIntensity)
        {
			string hyperText = "";
			
			// AUs 1 and 4 have common Haptek registers. So, we use GenerateFace() method to resolve the problem.
			hapFACS.AU1(intensity, lastIntensity);
			hapFACS.AU4(intensity, lastIntensity);
			hyperText += hapFACS.generateFace( );
			
            hyperText += hapFACS.AU15(intensity, intensity, lastIntensity, lastIntensity); // AU15 is a bilateral AU

			hapFACS.resetAU15( );
			return hyperText;
        }

        /// <author>
        ///     Reza Amini (ramin001@fiu.edu) 
        ///     Affective Social Computing lab at Florida International University
        /// </author>
        /// <summary>
		///     Maps EmFACS surprise on the Haptek face with the specified intensity. Highest level of surprise is the combination of AU1E, AU2E, AU5B, and AU26E.
        /// </summary>
        /// <param name="intensity"> 
        ///     Intensity of surprise
        /// </param>
        /// <param name="lastIntensity"> 
        ///     Previous intensity of surprise
        /// </param>
        /// <returns>
        ///     Hypertext to map surprise on the Haptek face
        /// </returns>
        /// <remarks></remarks>
        public string Surprise(string intensity, string lastIntensity)
        {
            string hyperText = "";
			
            hyperText += hapFACS.AU1(intensity, lastIntensity);
			hyperText += hapFACS.AU2( intensity, intensity, lastIntensity, lastIntensity ); // AU2 is a bilateral AU
            hyperText += hapFACS.AU26(intensity, lastIntensity);
			
			// Activate AU5 with maximum intensity of B
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU5( "B", lastIntensity );
			else if( intensity == "C" || intensity == "B" )
				hyperText += hapFACS.AU5( "A", lastIntensity );
			else if( intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU5( "0", lastIntensity );
			else // Here, we scale the AU5 maximum to the B instead of E
				hyperText += hapFACS.AU5( ( Convert.ToDouble( intensity ) / Constants.BIntensity ).ToString( ), lastIntensity );

			hapFACS.resetLowerAUs( );
			hapFACS.resetUpperAUs( );
			hapFACS.resetHeadAndEyeAUs( );
			return hyperText;
        }

        /// <author>
        ///     Reza Amini (ramin001@fiu.edu) 
        ///     Affective Social Computing lab at Florida International University
        /// </author>
        /// <summary>
        ///     Maps EmFACS fear on the Haptek face with the specified intensity. Highest level of fear is the combination of AU1E, AU2E, AU4E, AU5B, AU20C, and AU26D.
        /// </summary>
        /// <param name="intensity"> 
        ///     Intensity of fear
        /// </param>
        /// <param name="lastIntensity"> 
        ///     Previous intensity of fear
        /// </param>
        /// <returns>
        ///     Hypertext to map fear on the Haptek face
        /// </returns>
        /// <remarks></remarks>
        public string Fear(string intensity, string lastIntensity)
        {
			string hyperText = "";
			
			// AUs 1, 2, and 4 have common Haptek registers . So, we use generateFace() method to resolve that problem.
			hapFACS.AU1(intensity, lastIntensity);
			hapFACS.AU2( intensity, intensity, lastIntensity, lastIntensity ); // AU2 is a bilateral AU
			hapFACS.AU4( intensity, lastIntensity );

			hyperText += hapFACS.generateFace( );

			// Activate AU5 with maximum intensity of B
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU5( "B", lastIntensity );
			else if( intensity == "C" || intensity == "B" )
				hyperText += hapFACS.AU5( "A", lastIntensity );
			else if( intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU5( "0", lastIntensity );
			else // Here, we scale the AU5 maximum to the B instead of E
				hyperText += hapFACS.AU5( ( Convert.ToDouble( intensity ) / Constants.BIntensity ).ToString( ), lastIntensity );

			// Activate AU20 with maximum intensity of C
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU20( "C", lastIntensity );
			else if ( intensity == "C" || intensity == "B" || intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU20( intensity, lastIntensity );
			else // Here, we scale the AU20 maximum to the C instead of E
				hyperText += hapFACS.AU20( ( Convert.ToDouble( intensity ) / Constants.CIntensity ).ToString( ), lastIntensity );
			
			// Activate AU26 with maximum intensity of D
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU26( "D", lastIntensity );
			else if ( intensity == "C" || intensity == "B" || intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU26( intensity, lastIntensity );
			else // Here, we scale the AU26 maximum to the D instead of E
				hyperText += hapFACS.AU26( ( Convert.ToDouble( intensity ) / Constants.DIntensity ).ToString( ), lastIntensity );

			hapFACS.resetAU5( );
			hapFACS.resetAU20( );
			hapFACS.resetAU26( );
			return hyperText;
        }

        /// <author>
        ///     Reza Amini (ramin001@fiu.edu) 
        ///     Affective Social Computing lab at Florida International University
        /// </author>
        /// <summary>
		///     Maps EmFACS anger on the Haptek face with the specified intensity. Highest level of anger is the combination of AU4D, AU5B, AU7B, AU23A, and AU24B.
        /// </summary>
        /// <param name="intensity"> 
        ///     Intensity of anger
        /// </param>
        /// <param name="lastIntensity"> 
        ///     Previous intensity of anger
        /// </param>
        /// <returns>
        ///     Hypertext to map anger on the Haptek face
        /// </returns>
        /// <remarks></remarks>
        public string Anger(string intensity, string lastIntensity)
        {
            string hyperText = "";
			
			// Anger version 1
			/*
			// Activate AU4 with maximum intensity of D
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU4( "D", lastIntensity );
			else if( intensity == "C" || intensity == "B" || intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU4( intensity, lastIntensity );
			else // Here, we scale the AU4 maximum to the D instead of E
				hyperText += hapFACS.AU4( ( Convert.ToDouble( intensity ) / Constants.DIntensity ).ToString( ), lastIntensity );

			// Activate AU5 with maximum intensity of B
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU5( "B", lastIntensity );
			else if( intensity == "C" || intensity == "B" )
				hyperText += hapFACS.AU5( "A", lastIntensity );
			else if( intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU5( "0", lastIntensity );
			else // Here, we scale the AU5 maximum to the B instead of E
				hyperText += hapFACS.AU5( ( Convert.ToDouble( intensity ) / Constants.BIntensity ).ToString( ), lastIntensity );

			// Activate AU7 with maximum intensity of B
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU7( "B", lastIntensity );
			else if( intensity == "C" || intensity == "B" )
				hyperText += hapFACS.AU7( "A", lastIntensity );
			else if( intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU7( "0", lastIntensity );
			else // Here, we scale the AU7 maximum to the B instead of E
				hyperText += hapFACS.AU7( ( Convert.ToDouble( intensity ) / Constants.BIntensity ).ToString( ), lastIntensity );

			// Activate AU23 with maximum intensity of A
			if( intensity == "E" || intensity == "D" || intensity == "C" || intensity == "B" || intensity == "A" )
				hyperText += hapFACS.AU23("A", lastIntensity);
			else if (intensity == "0")
				hyperText += hapFACS.AU23( "0", lastIntensity );
			else // Here, we scale the AU23 maximum to the A instead of E
				hyperText += hapFACS.AU23( ( Convert.ToDouble( intensity ) / Constants.AIntensity ).ToString( ), lastIntensity );

			// Activate AU24 with maximum intensity of B
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU24( "B", lastIntensity );
			else if( intensity == "C" || intensity == "B" )
				hyperText += hapFACS.AU24( "A", lastIntensity );
			else if( intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU24( "0", lastIntensity );
			else // Here, we scale the AU24 maximum to the B instead of E
				hyperText += hapFACS.AU24( ( Convert.ToDouble( intensity ) / Constants.BIntensity ).ToString( ), lastIntensity );

			*/

			// Anger version 2
			// Activate AUs 5, 7, 9, 10, 15, 42 with maximum intensity of E
			hyperText += hapFACS.AU5( intensity, lastIntensity );
			hyperText += hapFACS.AU7( intensity, lastIntensity );
			hyperText += hapFACS.AU9( intensity, lastIntensity );
			hyperText += hapFACS.AU10( intensity, lastIntensity );
			hyperText += hapFACS.AU15( intensity, intensity, lastIntensity, lastIntensity );
			hyperText += hapFACS.AU42( intensity, lastIntensity );
						
			hapFACS.resetLowerAUs( );
			hapFACS.resetUpperAUs( );
			hapFACS.resetHeadAndEyeAUs( );
			return hyperText;
        }

        /// <author>
        ///     Reza Amini (ramin001@fiu.edu) 
        ///     Affective Social Computing lab at Florida International University
        /// </author>
        /// <summary>
		///     Maps EmFACS disgust on the Haptek face with the specified intensity. Highest level of disgust is the combination of AU9, AU15, and AU16.
        /// </summary>
        /// <param name="intensity"> 
        ///     Intensity of disgust
        /// </param>
        /// <param name="lastIntensity"> 
        ///     Previous intensity of disgust
        /// </param>
        /// <returns>
        ///     Hypertext to map disgust on the Haptek face
        /// </returns>
        /// <remarks></remarks>
        public string Disgust(string intensity, string lastIntensity)
        {
            string hyperText = "";
			
			hyperText += hapFACS.AU9(intensity, lastIntensity);
            hyperText += hapFACS.AU15(intensity, intensity, lastIntensity, lastIntensity); // AU15 is bilateral
            hyperText += hapFACS.AU16(intensity, lastIntensity);

			hapFACS.resetLowerAUs( );
			hapFACS.resetUpperAUs( );
			hapFACS.resetHeadAndEyeAUs( );
            return hyperText;
        }

        /// <author>
        ///     Reza Amini (ramin001@fiu.edu) 
        ///     Affective Social Computing lab at Florida International University
        /// </author>
        /// <summary>
		///     Maps EmFACS contempt on the Haptek face with the specified intensity. Highest level of contempt is the combination of AU12R (right) and AU14R (right).
        /// </summary>
        /// <param name="intensity"> 
        ///     Intensity of contempt
        /// </param>
        /// <param name="lastIntensity"> 
        ///     Previous intensity of contempt
        /// </param>
        /// <returns>
        ///     Hypertext to map contempt on the Haptek face
        /// </returns>
        /// <remarks></remarks>
        public string Contempt(string intensity, string lastIntensity)
        {
            string hyperText = "";
			
			// Activate AU12 with maximum intensity of A
			if( intensity == "E" || intensity == "D" || intensity == "C" )
				hyperText += hapFACS.AU12( "A", lastIntensity );
			else if( intensity == "B" || intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU12( "0", lastIntensity );
			else // Here, we scale the AU12 maximum to the A instead of E
				hyperText += hapFACS.AU12( ( Convert.ToDouble( intensity ) / Constants.AIntensity ).ToString( ), lastIntensity );

			// Activate AU14 on the right side
            hyperText += hapFACS.AU14("0", intensity, lastIntensity, lastIntensity); // AU14 is bilateral

			hapFACS.resetLowerAUs( );
			hapFACS.resetUpperAUs( );
			hapFACS.resetHeadAndEyeAUs( );
			return hyperText;
        }

        /// <author>
        ///     Reza Amini (ramin001@fiu.edu) 
        ///     Affective Social Computing lab at Florida International University
        /// </author>
        /// <summary>
		///     Maps EmFACS embarrassment on the Haptek face with the specified intensity. Highest level of embarrassment is the combination of AU12C, AU54C, AU62B, and AU64C.
        /// </summary>
        /// <param name="intensity"> 
        ///     Intensity of embarrassment
        /// </param>
        /// <param name="lastIntensity"> 
        ///     Previous intensity of embarrassment
        /// </param>
        /// <returns>
        ///     Hypertext to map embarrassment on the Haptek face
        /// </returns>
        /// <remarks></remarks>
        public string Embarrassment(string intensity, string lastIntensity)
        {
            string hyperText = "";
			
			// Activate AU12 with maximum intensity of C
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU12( "C", lastIntensity );
			else if( intensity == "C" || intensity == "B" || intensity == "A" || intensity == "0"  )
				hyperText += hapFACS.AU12( intensity, lastIntensity );
			else // Here, we scale the AU12 maximum to the C instead of E
				hyperText += hapFACS.AU12( ( Convert.ToDouble( intensity ) / Constants.CIntensity ).ToString( ), lastIntensity );

			// Activate AU54 with maximum intensity of C
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU54( "C", lastIntensity );
			else if( intensity == "C" || intensity == "B" || intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU54( intensity, lastIntensity );
			else // Here, we scale the AU54 maximum to the C instead of E
				hyperText += hapFACS.AU54( ( Convert.ToDouble( intensity ) / Constants.CIntensity ).ToString( ), lastIntensity );
            
			// Activate AU62 with maximum intensity of B
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU62( "B", "B", lastIntensity, lastIntensity ); // AU62 is bilateral
			else if( intensity == "C" || intensity == "B" )
				hyperText += hapFACS.AU62( "A", "A", lastIntensity, lastIntensity );
			else if( intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU62( "0", "0", lastIntensity, lastIntensity );
			else // Here, we scale the AU62 maximum to the B instead of E
				hyperText += hapFACS.AU62( ( Convert.ToDouble( intensity ) / Constants.BIntensity ).ToString( ), 
					( Convert.ToDouble( intensity ) / Constants.BIntensity ).ToString( ), lastIntensity, lastIntensity );

			// Activate AU64 with maximum intensity of C
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU64( "C", "C", lastIntensity, lastIntensity ); // AU64 is bilateral
			else if( intensity == "C" || intensity == "B" || intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU64( intensity, intensity, lastIntensity, lastIntensity );
			else // Here, we scale the AU64 maximum to the C instead of E
				hyperText += hapFACS.AU64( ( Convert.ToDouble( intensity ) / Constants.CIntensity ).ToString( ),
					( Convert.ToDouble( intensity ) / Constants.CIntensity ).ToString( ), lastIntensity, lastIntensity );

			hapFACS.resetLowerAUs( );
			hapFACS.resetUpperAUs( );
			hapFACS.resetHeadAndEyeAUs( );
			return hyperText;
        }

        /// <author>
        ///     Reza Amini (ramin001@fiu.edu) 
        ///     Affective Social Computing lab at Florida International University
        /// </author>
        /// <summary>
		///     Maps EmFACS pride on the Haptek face with the specified intensity. Highest level of pride is the combination of AU12B, AU53E, AU58E, and AU64E.
        /// </summary>
        /// <param name="intensity"> 
        ///     Intensity of pride
        /// </param>
        /// <param name="lastIntensity"> 
        ///     Previous intensity of pride
        /// </param>
        /// <returns>
        ///     Hypertext to map pride on the Haptek face
        /// </returns>
        /// <remarks></remarks>
        public string Pride(string intensity, string lastIntensity)
        {
			string hyperText = "";
			
			hapFACS.AU53( intensity, lastIntensity );
			hapFACS.AU58( intensity, lastIntensity );
			hyperText += hapFACS.generateFace( );

			// Activate AU12 with maximum intensity of B
			if( intensity == "E" || intensity == "D" )
				hyperText += hapFACS.AU12( "B", lastIntensity );
			else if( intensity == "C" || intensity == "B" )
				hyperText += hapFACS.AU12( "A", lastIntensity );
			else if( intensity == "A" || intensity == "0" )
				hyperText += hapFACS.AU12( "0", lastIntensity );
			else // Here, we scale the AU12 maximum to the B instead of E
				hyperText += hapFACS.AU12( ( Convert.ToDouble( intensity ) / Constants.BIntensity ).ToString( ), lastIntensity );

			hyperText += hapFACS.AU64(intensity, intensity, lastIntensity, lastIntensity); // AU64 is bilateral

			hapFACS.resetAU12( );
			hapFACS.resetAU64( );
			return hyperText;
        }

        /// <author>
        ///     Reza Amini (ramin001@fiu.edu) 
        ///     Affective Social Computing lab at Florida International University
        /// </author>
        /// <summary>
        ///     Resets the facial emotion to neutral.
        /// </summary>
        /// <returns>
        ///     Hypertext to reset the facial emotion to neutral
        /// </returns>
        /// <remarks></remarks>
        public string Neutral()
        {
            string hyperText = "";
			//HapFACS hapFACS = new HapFACS( );
            hyperText += hapFACS.resetUpperAUs();
            hyperText += hapFACS.resetLowerAUs();
            hyperText += hapFACS.resetHeadAndEyeAUs();

			//hapFACS = null; // This will call the destructor to do garbage collection for hapFACS object which is not used any more.
			return hyperText;
		}
		
		#endregion
	}
}

