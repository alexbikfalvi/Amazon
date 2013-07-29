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
	/// A class representing an AWS top sites result atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomAtsTopSitesResult : Atom
	{
		internal const string xmlPrefix = "aws";
		internal const string xmlName = "TopSitesResult";

		private AtomAtsAlexa alexa;

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomAtsTopSitesResult(XElement element)
		{
			// Check the XML element name.
			if (!element.HasName(AtomAtsTopSitesResult.xmlPrefix, AtomAtsTopSitesResult.xmlName)) throw new AtomException("XML element name mismatch.", element);

			// Parse the XML element members.
			this.alexa = AtomAtsAlexa.Parse(element.Element(AtomAtsAlexa.xmlPrefix, AtomAtsAlexa.xmlName));
		}

		// Public properties.

		/// <summary>
		/// Gets the Alexa top sites property.
		/// </summary>
		public AtomAtsAlexa Alexa { get { return this.alexa; } }

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsTopSitesResult Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomAtsTopSitesResult(element);
		}
	}
}
