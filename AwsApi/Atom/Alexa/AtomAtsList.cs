/* 
 * Copyright (C) 2013 Alex Bikfalvi
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or (at
 * your option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
 */

using System;
using System.Xml.Linq;
using DotNetApi.Xml;

namespace AwsApi.Atom.Alexa
{
	/// <summary>
	/// A class representing an AWS list atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomAtsList : Atom
	{
		internal const string xmlPrefix = "aws";
		internal const string xmlName = "List";

		private AtomAtsName countryName;
		private AtomAtsCountryCode countryCode;
		private AtomAtsTotalSites totalSites;
		private AtomAtsSites sites;

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomAtsList(XElement element)
		{
			// Check the XML element name.
			if (!element.HasName(AtomAtsList.xmlPrefix, AtomAtsList.xmlName)) throw new AtomException("XML element name mismatch.", element);

			// Parse the XML element members.
			this.countryName = AtomAtsName.Parse(element.Element(AtomAtsName.xmlPrefix, AtomAtsName.xmlName));
			this.countryCode = AtomAtsCountryCode.Parse(element.Element(AtomAtsCountryCode.xmlPrefix, AtomAtsCountryCode.xmlName));
			this.totalSites = AtomAtsTotalSites.Parse(element.Element(AtomAtsTotalSites.xmlPrefix, AtomAtsTotalSites.xmlName));
			this.sites = AtomAtsSites.Parse(element.Element(AtomAtsSites.xmlPrefix, AtomAtsSites.xmlName));
		}

		// Public properties.

		/// <summary>
		/// Gets the country name property.
		/// </summary>
		public AtomAtsName CountryName { get { return this.countryName; } }
		/// <summary>
		/// Gets the country code property.
		/// </summary>
		public AtomAtsCountryCode CountryCode { get { return this.countryCode; } }
		/// <summary>
		/// Gets the total sites property.
		/// </summary>
		public AtomAtsTotalSites TotalSites { get { return this.totalSites; } }
		/// <summary>
		/// Gets the sites property.
		/// </summary>
		public AtomAtsSites Sites { get { return this.sites; } }

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsList Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomAtsList(element);
		}
	}
}
