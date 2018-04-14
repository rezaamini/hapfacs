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
using System.Windows.Forms;

namespace HapFACS
{
	/// <author>
	///     Reza Amini (ramin001@fiu.edu) 
	///     Affective Social Computing lab at Florida International University
	/// </author>
	/// <summary>
	///     AnimationSector object simulates an animation sector which includes all the information needed to animate an AU
	/// </summary>
	/// <remarks></remarks>
	class AnimationSector
	{
		#region Local variables
		private string AU; // animation AU
		private string Side; // AU side
		private string StartIntensity; // AU starting intensity
		private string EndIntensity; // AU ending intensity
		private double StartTime; // start time of animating AU
		private double EndTime; // end time of animating AU
		private string textToSpeak; // Text to be spoken 

		#endregion

		#region Constructors
		
		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Creates an AnimationSector for the unilateral AUs which need the side of the AU as a parameter as well as other parameters
		/// </summary>
		/// <param name="AU"> 
		///     The AU to be animated
		/// </param>
		/// <param name="side"> 
		///     Side of the AU to be animated
		/// </param>
		/// <param name="startIntensity"> 
		///     Start intensity of the AU to be animated
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of the AU to be animated
		/// </param>
		/// <param name="startTime"> 
		///     Time when the AU activation starts
		/// </param>
		/// <param name="endTime"> 
		///     Time when the AU activation ends
		/// </param>
		/// <remarks></remarks>
		public AnimationSector( string AU, string side, string startIntensity, string endIntensity, string startTime, string endTime )
		{
			this.AU = AU;
			this.Side = side;
			this.StartIntensity = startIntensity;
			this.EndIntensity = endIntensity;

			if( !double.TryParse( startTime, out this.StartTime ) )
				MessageBox.Show( "The start time of the Action Units should be a double number" );
			if( !double.TryParse( startTime, out this.EndTime ) )
				MessageBox.Show( "The end time of the Action Units should be a double number" );

			this.setStartTime(Convert.ToDouble( startTime ));
			if( AU == "Speak" )
				this.textToSpeak = endTime; // Here, the text inside the endTime TextBox plays the role of the "text to be spoken".
			else if( AU == "AUM59" || AU == "AUM60" )
				this.setEndTime(this.getStartTime( ) + 2);
			else if( AU == "AU45" || AU == "AU46" )
				this.setEndTime(this.getStartTime( ) + 1);
			else
				this.setEndTime(Convert.ToDouble( endTime ));
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Creates an AnimationSector for the bilateral AUs which activate both sides of the AU
		/// </summary>
		/// <param name="AU"> 
		///     The AU to be animated
		/// </param>
		/// <param name="startIntensity"> 
		///     Start intensity of the AU to be animated
		/// </param>
		/// <param name="endIntensity"> 
		///     End intensity of the AU to be animated
		/// </param>
		/// <param name="startTime"> 
		///     Time when the AU activation starts
		/// </param>
		/// <param name="endTime"> 
		///     Time when the AU activation ends
		/// </param>
		/// <remarks></remarks>
		public AnimationSector( string AU, string startIntensity, string endIntensity, string startTime, string endTime )
		{
			this.AU = AU;
			this.StartIntensity = startIntensity;
			this.EndIntensity = endIntensity;

			if( !double.TryParse( startTime, out this.StartTime ) )
				MessageBox.Show( "The start time of the Action Units should be a double number" );
			if( !double.TryParse( startTime, out this.EndTime ) )
				MessageBox.Show( "The end time of the Action Units should be a double number" );

			this.setStartTime(Convert.ToDouble( startTime ));
			if( AU == "Speak" )
				this.textToSpeak = endTime; // Here, the text inside the endTime TextBox plays the role of the "text to be spoken".
			else if( AU == "AUM59" || AU == "AUM60" )
				this.setEndTime(this.getStartTime( ) + 2);
			else if( AU == "AU45" || AU == "AU46" )
				this.setEndTime(this.getStartTime( ) + 1);
			else
				this.setEndTime(Convert.ToDouble( endTime ));
		}

		#endregion

		#region Methods
		// get and set method for AU

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the AU name to be animated
		/// </summary>
		/// <returns>
		///     Name of the AU to be animated
		/// </returns>
		/// <remarks></remarks>
		public string getAU( )
		{
			return AU;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Sets the name of the AU to be animated
		/// </summary>
		/// <param name="au"> 
		///     Name of the AU to be animated
		/// </param>
		/// <remarks></remarks>
		public void setAU( string au )
		{
			AU = au;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the time (in seconds) when the AU activation starts
		/// </summary>
		/// <returns>
		///     The time (in seconds) when the AU activation starts
		/// </returns>
		/// <remarks></remarks>
		public double getStartTime( )
		{
			return StartTime;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Sets the time (in seconds) when the AU activation starts
		/// </summary>
		/// <param name="startTime"> 
		///     The time (in seconds) when the AU activation starts
		/// </param>
		/// <remarks></remarks>
		public void setStartTime( double startTime )
		{
			StartTime = startTime;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the time (in seconds) when the AU activation ends
		/// </summary>
		/// <returns>
		///     The time (in seconds) when the AU activation ends
		/// </returns>
		/// <remarks></remarks>
		public double getEndTime( )
		{
			return EndTime;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Sets the time (in seconds) when the AU activation ends
		/// </summary>
		/// <param name="endTime"> 
		///     The time (in seconds) when the AU activation ends
		/// </param>
		/// <remarks></remarks>
		public void setEndTime( double endTime )
		{
			EndTime = endTime;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the starting intensity of the AU activation
		/// </summary>
		/// <returns>
		///     The starting intensity of the AU activation
		/// </returns>
		/// <remarks></remarks>
		public string getStartIntensity()
		{
			return StartIntensity;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Sets the starting intensity of the AU activation
		/// </summary>
		/// <param name="startIntensity"> 
		///     The starting intensity of the AU activation
		/// </param>
		/// <remarks></remarks>
		public void setStartIntensity(string startIntensity)
		{
			StartIntensity = startIntensity;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the ending intensity of the AU activation
		/// </summary>
		/// <returns>
		///     The ending intensity of the AU activation
		/// </returns>
		/// <remarks></remarks>
		public string getEndIntensity( )
		{
			return EndIntensity;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Sets the ending intensity of the AU activation
		/// </summary>
		/// <param name="endIntensity"> 
		///     The ending intensity of the AU activation
		/// </param>
		/// <remarks></remarks>
		public void setEndIntensity( string endIntensity )
		{
			EndIntensity = endIntensity;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the side of the AU to be activated
		/// </summary>
		/// <returns>
		///     The side of the AU to be activated
		/// </returns>
		/// <remarks></remarks>
		public string getSide( )
		{
			return Side;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Sets the side of the AU to be activated
		/// </summary>
		/// <param name="side"> 
		///     The side of the AU to be activated
		/// </param>
		/// <remarks></remarks>
		public void setSide( string side )
		{
			Side = side;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the animation sector length in terms of seconds
		/// </summary>
		/// <returns>
		///     The animation sector length in terms of seconds
		/// </returns>
		/// <remarks></remarks>
		public double sectorLength( )
		{
			return ( getEndTime() - getStartTime( ) );
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the text to be spoken in the animation sector
		/// </summary>
		/// <returns>
		///     The text to be spoken in the animation sector
		/// </returns>
		/// <remarks></remarks>
		public string getTextToSpeak( )
		{
			return textToSpeak;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Sets he text to be spoken in the animation sector
		/// </summary>
		/// <param name="text"> 
		///     The text to be spoken in the animation sector
		/// </param>
		/// <remarks></remarks>
		public void setTextToSpeak( string text )
		{
			textToSpeak = text;
		}

		#endregion
	}
}
