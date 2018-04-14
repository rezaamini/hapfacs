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
	///     Animation object simulates an animation involving different sectors, each of which is an AU activation with a given intensity from a given starting time to a given ending time.
	/// </summary>
	/// <remarks></remarks>
	class Animation
	{
		#region Local varibale
		// 


		// Array of Animation Sectors creating the final animation
		private ArrayList animation = new ArrayList();

		#endregion

		#region Constructors
		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Creates an Animation object using an arraylist of AnimationSectors
		/// </summary>
		/// <param name="animation"> 
		///     ArrayList of animation sectors
		/// </param>
		/// <remarks></remarks>
		public Animation(ArrayList animation )
		{
			this.animation = animation;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Creates an Animation object. 
		/// </summary>
		/// <remarks></remarks>
		public Animation( )
		{

		}

		#endregion

		#region Methods

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Adds a sector to the animation
		/// </summary>
		/// <param name="sector"> 
		///     AnimationSector to be added to the animation
		/// </param>
		/// <remarks></remarks>
		public void addToAnimation( AnimationSector sector )
		{
			animation.Add( sector );
		}

		// Returns the animation

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Returns the animation object including all the animation sectors
		/// </summary>
		/// <returns>
		///     Animation ArrayList which contains all the animation sectors
		/// </returns>
		/// <remarks></remarks>
		public ArrayList getAnimation ()
		{
			return animation;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the length of the animation excluding the stop times between the animation sectors
		/// </summary>
		/// <returns>
		///     Length of the animation excluding the stop times between the animation sectors
		/// </returns>
		/// <remarks></remarks>
		public double animationLengthExcludingStops( )
		{
			double length = 0;
			for( int i = 0; i < animation.Count; i++ )
			{
				String au = ((AnimationSector)animation[i]).getAU();
				if( au == "AUM59" || au == "AUM60" )
					length += 2; // Add 2 seconds for head shake and head nod
				else if( au == "AU45" || au == "AU46" )
					length += 1; // Add 1 second for blink
				else
					length += ( ( AnimationSector )animation [ i ] ).getEndTime() - ( ( AnimationSector )animation [ i ] ).getStartTime( );
			}

			return length;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the length of the animation including the stop times between the animation sectors
		/// </summary>
		/// <returns>
		///     Length of the animation including the stop times between the animation sectors
		/// </returns>
		/// <remarks></remarks>
		public double animationLengthIncludingStops( )
		{
			double maxEnd = Int16.MinValue;
			for( int i = 0; i < animation.Count; i++ )
			{
				if( ( ( AnimationSector )animation [ i ] ).getEndTime() >= maxEnd )
					maxEnd = ( ( AnimationSector )animation [ i ] ).getEndTime();
			}

			AnimationSector lastSector = (AnimationSector)animation[animation.Count - 1];
			if( lastSector.getAU( ) == "AUM59" || lastSector.getAU( ) == "AUM60" )
				maxEnd += 2;
			else if( lastSector.getAU( ) == "AU45" || lastSector.getAU( ) == "AU46" )
				maxEnd += 1;
			return maxEnd;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the starting time of the animation
		/// </summary>
		/// <returns>
		///     Starting time of the animation
		/// </returns>
		/// <remarks></remarks>
		public double animationStartTime( )
		{
			double minStart = Int16.MaxValue;
			for( int i = 0; i < animation.Count; i++ )
			{
				if( ( ( AnimationSector )animation [ i ] ).getStartTime( ) <= minStart )
					minStart = ( ( AnimationSector )animation [ i ] ).getStartTime( );
			}

			return minStart;
		}

		/// <author>
		///     Reza Amini (ramin001@fiu.edu) 
		///     Affective Social Computing lab at Florida International University
		/// </author>
		/// <summary>
		///     Gets the stop time of the animation
		/// </summary>
		/// <returns>
		///     Stop time of the animation
		/// </returns>
		/// <remarks></remarks>
		public double animationStopTime( )
		{
			return animationLengthIncludingStops( ) - animationLengthExcludingStops( );
		}

		#endregion
	}
}
