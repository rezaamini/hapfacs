﻿/*
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace HapFACS
{
	partial class AboutBox : Form
	{
		public AboutBox( )
		{
			InitializeComponent( );
			//this.Text = String.Format( "About {0}", AssemblyTitle );
			//this.labelProductName.Text = AssemblyProduct;
			//this.labelVersion.Text = String.Format( "Version {0}", AssemblyVersion );
			//this.labelCopyright.Text = AssemblyCopyright;
			//this.labelCompanyName.Text = AssemblyCompany;
			//this.textBoxDescription.Text = AssemblyDescription;
		}

		#region Assembly Attribute Accessors

		private string AssemblyTitle
		{
			get
			{
				object [ ] attributes = Assembly.GetExecutingAssembly( ).GetCustomAttributes( typeof( AssemblyTitleAttribute ), false );
				if( attributes.Length > 0 )
				{
					AssemblyTitleAttribute titleAttribute = ( AssemblyTitleAttribute )attributes [ 0 ];
					if( titleAttribute.Title != "" )
					{
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension( Assembly.GetExecutingAssembly( ).CodeBase );
			}
		}

		private string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly( ).GetName( ).Version.ToString( );
			}
		}

		private string AssemblyDescription
		{
			get
			{
				object [ ] attributes = Assembly.GetExecutingAssembly( ).GetCustomAttributes( typeof( AssemblyDescriptionAttribute ), false );
				if( attributes.Length == 0 )
				{
					return "";
				}
				return ( ( AssemblyDescriptionAttribute )attributes [ 0 ] ).Description;
			}
		}

		private string AssemblyProduct
		{
			get
			{
				object [ ] attributes = Assembly.GetExecutingAssembly( ).GetCustomAttributes( typeof( AssemblyProductAttribute ), false );
				if( attributes.Length == 0 )
				{
					return "";
				}
				return ( ( AssemblyProductAttribute )attributes [ 0 ] ).Product;
			}
		}

		private string AssemblyCopyright
		{
			get
			{
				object [ ] attributes = Assembly.GetExecutingAssembly( ).GetCustomAttributes( typeof( AssemblyCopyrightAttribute ), false );
				if( attributes.Length == 0 )
				{
					return "";
				}
				return ( ( AssemblyCopyrightAttribute )attributes [ 0 ] ).Copyright;
			}
		}

		private string AssemblyCompany
		{
			get
			{
				object [ ] attributes = Assembly.GetExecutingAssembly( ).GetCustomAttributes( typeof( AssemblyCompanyAttribute ), false );
				if( attributes.Length == 0 )
				{
					return "";
				}
				return ( ( AssemblyCompanyAttribute )attributes [ 0 ] ).Company;
			}
		}
		#endregion

		private void okButton_Click( object sender, EventArgs e )
		{
			this.Close( );
		}

		private void AboutBox_Load( object sender, EventArgs e )
		{

		}
	}
}
