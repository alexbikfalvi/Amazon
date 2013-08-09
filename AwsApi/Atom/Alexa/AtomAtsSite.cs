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
	/// A class representing an AWS site atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomAtsSite : Atom
	{
		internal const string xmlPrefix = "aws";
		internal const string xmlName = "Site";

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomAtsSite(XElement element)
			: base(element, AtomAtsSite.xmlPrefix, AtomAtsSite.xmlName)
		{
			// Parse the XML element members.
			this.DataUrl = AtomAtsDataUrl.ParseChild(element);
			this.Country = AtomAtsCountry.ParseChild(element);
			this.Global = AtomAtsGlobal.ParseChild(element);
		}

		// Public properties.

		/// <summary>
		/// Gets the data URL property.
		/// </summary>
		public AtomAtsDataUrl DataUrl { get; private set; }
		/// <summary>
		/// Gets the country property.
		/// </summary>
		public AtomAtsCountry Country { get; private set; }
		/// <summary>
		/// Gets the global property.
		/// </summary>
		public AtomAtsGlobal Global { get; private set; }

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <param name="top">The XML namespace.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsSite Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomAtsSite(element);
		}
	}
}
