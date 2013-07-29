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
using System.Linq;
using System.Xml.Linq;
using DotNetApi.Xml;

namespace AwsApi.Atom.Alexa
{
	/// <summary>
	/// A class representing an AWS top sites response atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomAtsTopSitesResponse : Atom
	{
		internal const string xmlPrefix = "aws";
		internal const string xmlName = "TopSitesResponse";

		private AtomAtsResponse response;

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomAtsTopSitesResponse(XElement element)
		{
			// Check the XML element name.
			if (!element.HasName(AtomAtsTopSitesResponse.xmlPrefix, AtomAtsTopSitesResponse.xmlName)) throw new AtomException("XML element name mismatch.", element);

			// Parse the XML element members.
			XElement el = element.Element(AtomAtsResponse.xmlPrefix, AtomAtsResponse.xmlName);
			this.response = AtomAtsResponse.Parse(el);
		}

		// Public properties.

		/// <summary>
		/// Gets the response property.
		/// </summary>
		public AtomAtsResponse Response { get { return this.response; } }

		// Public methods.


		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsTopSitesResponse Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomAtsTopSitesResponse(element);
		}
	}
}
