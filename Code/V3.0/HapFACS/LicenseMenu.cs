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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace HapFACS
{
	partial class LicenseMenu : Form
	{
		public LicenseMenu( )
		{
			InitializeComponent( );
			this.textBoxDescription.Text = "Copyright 2014 Florida International University Board of Trustees" + Environment.NewLine + Environment.NewLine +
				"All HapFACS users per the software license must cite the following article in any work derived from HapFACS (available at http://ascl.cs.fiu.edu/publications.html):" + Environment.NewLine + Environment.NewLine +
				"R. Amini, and C. Lisetti, (2013). HapFACS: an Open Source API/Software to Generate FACS-Based Expressions for ECAs Animation and for Corpus Generation. In Proceedings of the Affective Computing and Intelligent Interactions, Geneva, 2013." + Environment.NewLine + Environment.NewLine +
 				"Based on a work at haptek.com.";
		}

		private void okButton_Click( object sender, EventArgs e )
		{
			this.Close( );
		}

		private void LicenseMenu_Load( object sender, EventArgs e )
		{

		}
		
	}
}
