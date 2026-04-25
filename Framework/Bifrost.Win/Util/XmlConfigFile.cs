using System;
using System.Xml;
using System.IO;
using System.Collections;

namespace Bifrost.Win.Util
{
	/// <summary>
	/// Creates a basic XML config file designed to take a 
	/// Section, Key and Value
	/// It will then allow you to recall those values from
	/// the file or edit the config file.
	/// 
	/// Provides two static functions for accessing the XML
	/// Config files.
	/// </summary>
	public class XmlConfigFile
	{
		/// <summary>
		/// Internal Class for creating a single XML Configuration File
		/// </summary>
		public class clsXmlCfgFile 
		{
			// Filename
			private String strFilename;
			private Boolean boolDoesExist;
			private XmlDocument xmlConfigDoc;

			/// <summary>
			///  An internal class for modifying a single XML Configuration
			/// </summary>
			/// <param name="sFilename">The Filename for the XML document</param>
			public clsXmlCfgFile ( String sFilename ) 
			{				
				// Sets the filename
				strFilename = sFilename;
				xmlConfigDoc = new XmlDocument();

				try 
				{
					// Loads the XML Document
					xmlConfigDoc.Load( sFilename );

					// Verifies the existence of the XML document
					boolDoesExist = true;
				}
				catch
				{
					// This is not the best way to handle file checking, but OK
					xmlConfigDoc.LoadXml ( "<configuration>" + "</configuration>" );
					xmlConfigDoc.Save ( sFilename );
				}
			}

			/// <summary>
			///	GetConfigInfo
			/// 
			/// Gets the requested information out of the XML configuration file
			/// Will return an array of Strings if the keys are found. 
			/// Returns an empty array if the keys are missing.
			/// </summary>
			/// <param name="sSection">Desired Section</param>
			/// <param name="sKey">Desired Key</param>
			/// <param name="sDefaultValue">Return the default value if the key is missing</param>
			/// <returns></returns>
			public ArrayList GetConfigInfo( String sSection, String sKey, String sDefaultValue )
			{
				ArrayList alConfigList;

				if ( boolDoesExist == false ) 
				{
					alConfigList = new ArrayList();
					alConfigList.Add( getKeyValue ( sSection, sKey, sDefaultValue ) );
				}
				else if ( sSection == "" ) 
				{
					alConfigList = getChildren("");
				}
				else if ( sKey == "" )
				{
					alConfigList = getChildren (sSection);
				}
				else 
				{
					alConfigList = new ArrayList();
					alConfigList.Add( getKeyValue ( sSection, sKey, sDefaultValue ) );
				}

				return alConfigList;
			}


			/// <summary>
			/// WriteConfigInfo
			/// Writes the configuration information INI style into
			/// the configuration file. 
			/// IMPORTANT-- Using an empty value for sSection or sKey will
			/// result in the erasure of the entire Config file or ConfigSection.
			/// </summary>
			/// <param name="sSection">Section to use</param>
			/// <param name="sKey">Chosen key</param>
			/// <param name="sValue">Write value</param>
			/// <returns></returns>
			public Boolean WriteConfigInfo ( String sSection, String sKey, String sValue ) 
			{
				
				XmlNode node1, node2;

				// If there's no key, empty the section
				if ( sKey == "" ) 
				{
					// find the section, remove all its keys and remove the section
					node1 = (xmlConfigDoc.DocumentElement).SelectSingleNode( "/configuration/" );

					// if no such section return true
					if ( node1 == null ) 
					{
						return true;
					}
					else 
					{
						// remove all its children
						node1.RemoveAll();

						// Select its parent "Configuration"
						node2 = (xmlConfigDoc.DocumentElement).SelectSingleNode( "configuration" );

						// Remove the section
						node2.RemoveChild(node1);
					}
				}
				else if ( sValue == "" ) 
				{
					node1 = (xmlConfigDoc.DocumentElement).SelectSingleNode("/configuration/" + sSection);

					// Return if the section is empty
					if ( node1 == null )
					{
						return true;
					}
					else 
					{
						node2 = (xmlConfigDoc.DocumentElement).SelectSingleNode("/configuration/" + sSection + "/" + sKey);
						
						if ( node2 == null )		
						{
							return true;
						}
						else 
						{
							if (node1.RemoveChild( node2 ) == null )
							{
								return false;
							}
						}
					}
				}
				else 
				{
					// If both Value and Key are filled
					node1 = (xmlConfigDoc.DocumentElement).SelectSingleNode("/configuration/" + sSection + "/" + sKey);
					if ( node1 == null ) 
					{
						// The key doesn't exist -- find the section
						node2 = (xmlConfigDoc.DocumentElement).SelectSingleNode("/configuration/" + sSection);
						
						if ( node2 == null ) 
						{	
							// Create the section first
							XmlElement e = xmlConfigDoc.CreateElement ( sSection );

							// Add the new node at the end of the children of "configuration"
							node2 = xmlConfigDoc.DocumentElement.AppendChild ( e );

							// Return false if it fails
							if ( node2 == null ) 
							{
								return false;
							}
							else 
							{
								// Create the key
								e = xmlConfigDoc.CreateElement( sKey );
								e.InnerText = sValue;

								if ( node2.AppendChild( e ) == null ) 
								{
									return false;
								}
							}
						}
						else 
						{
							XmlElement e = xmlConfigDoc.CreateElement ( sKey );
							e.InnerText = sValue;
							node2.AppendChild( e );
						} 

					}
					else 
					{
						// Create the key and put the value
						node1.InnerText = sValue;	
					}
				}
				try 
				{
					// If its succeeded, save the file.
					xmlConfigDoc.Save( strFilename );
				
					return true;
				}
				catch ( Exception e)
				{
					System.Console.WriteLine ( e );
					return false;
				}
			}

			/// <summary>
			/// getKeyValue
			/// </summary>
			/// <param name="sSection">Section of the Config File</param>
			/// <param name="sKey">Desired key of the section</param>
			/// <param name="sDefaultValue">Default return value if the key is missing</param>
			/// <returns>the string value of the selected node.</returns>
			public String getKeyValue ( String sSection, String sKey, String sDefaultValue ) 
			{
				XmlNode node;

				node = (xmlConfigDoc.DocumentElement).SelectSingleNode("/configuration/" + sSection + "/" + sKey );
				if ( node == null ) 
				{
					return sDefaultValue;
				}
				else 
				{
					return node.InnerText;
				}
			}
			/**
			 * getChildren returns an ArrayList of the nodes under the node
			 * given to the function
			 * 
			 * @param sNodeName - the string nodename
			 * @return - An Arraylist of Strings containing the node names
			 */
			public ArrayList getChildren ( String sNodeName ) 
			{
				ArrayList alChildren = new ArrayList();
				XmlNode node;

				try 
				{
					if ( sNodeName == "" )
					{
						node = xmlConfigDoc.DocumentElement;
					}
					else 
					{
						node = xmlConfigDoc.SelectSingleNode( sNodeName );
					}
				}
				catch {
					return alChildren;
				}

				if ( node == null ) 
				{
					// Returning the empty collection
				}
				else if ( node.HasChildNodes == false )
				{
					// We're still returning an empty arraylist
				}
				else 
				{
					// Return the children of the current node
					XmlNodeList nodeList = node.ChildNodes;

					for( int i = 0; i < nodeList.Count; i++ )
					{
						// Add each child to the list
						alChildren.Add( nodeList.Item( i ).Name );
					}
				}
				return alChildren;
			}
		}



		/// <summary>
		///  Parses a String for XML saving
		///  (Removes lt and gt)
		/// </summary>
		/// <param name="strParseMe">The String to be Parsed</param>
		/// <returns></returns>
		public static String ParseForXML ( String strParseMe ) 
		{
			return strParseMe.Replace ( "<", "&#lt" ).Replace( ">", "&#gt");
		}

		/// <summary>
		/// Obtains the Configuration Information 
		/// From the specified file
		/// </summary>
		/// <param name="sSection">Section Name</param>
		/// <param name="sKey">Desired Key</param>
		/// <param name="sDefaultValue">Default Value of Key</param>
		/// <param name="sFilename">Filename of XML config file</param>
		/// <returns>return a list of values in the section</returns>		
		public static ArrayList GetConfigInfo (String sSection, String sKey, String sDefaultValue, String sFilename )
		{
			ArrayList alConfigList;

			if ( sFilename == "" ) 
			{
				alConfigList = new ArrayList();
			}
			else 
			{
				clsXmlCfgFile xmlFile = new clsXmlCfgFile( sFilename );
				alConfigList = xmlFile.GetConfigInfo( sSection, sKey, sDefaultValue );
			}

			return alConfigList;
		}

		/// <summary>
		/// Writes the specified information to the configuration file.
		/// 
		/// Warning!! 
		/// 
		/// Leaving the Key or Section value as "" will result in
		/// the erasure of the entire tree.
		/// 
		/// </summary>
		/// <param name="sSection">Section to be written to</param>
		/// <param name="sKey">Key to Write</param>
		/// <param name="sValue">Value to Write</param>
		/// <param name="sFilename">Filename to write</param>
		/// <returns>return true if the writing succeeds.</returns>
		public static Boolean WriteConfigInfo (String sSection, String sKey, String sValue, String sFilename )
		{
			if ( sSection == "" || sKey == "" ) 
			{
				return false;
			}
			else 
			{
				clsXmlCfgFile xmlFile = new clsXmlCfgFile ( sFilename );
				return xmlFile.WriteConfigInfo( sSection, sKey, sValue );
			}
		}
		/// <summary>
		/// This class should never be instanced
		/// </summary>
		private XmlConfigFile () 
		{
		}
	}
}
