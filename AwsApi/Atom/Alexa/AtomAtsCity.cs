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
	/// A class representing an AWS city atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomAtsCity : Atom
	{
		internal const string xmlPrefix = "aws";
		internal const string xmlName = "City";

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomAtsCity(XElement element)
			: base(element, AtomAtsCity.xmlPrefix, AtomAtsCity.xmlName)
		{
			// Parse the XML element members.
			this.Name = AtomAtsName.ParseChild(element);
			this.Code = AtomAtsCode.ParseChild(element);
			this.TotalSites = AtomAtsTotalSites.ParseChild(element);
			this.PageViews = AtomAtsPageViews.ParseChild(element);
			this.Users = AtomAtsUsers.ParseChild(element);
		}

		// Public properties.

		/// <summary>
		/// Gets the name property.
		/// </summary>
		public AtomAtsName Name { get; private set; }
		/// <summary>
		/// Gets the code property.
		/// </summary>
		public AtomAtsCode Code { get; private set; }
		/// <summary>
		/// Gets the total sites property.
		/// </summary>
		public AtomAtsTotalSites TotalSites { get; private set; }
		/// <summary>
		/// Gets the page views property.
		/// </summary>
		public AtomAtsPageViews PageViews { get; private set; }
		/// <summary>
		/// Gets the users property.
		/// </summary>
		public AtomAtsUsers Users { get; private set; }

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <param name="top">The XML namespace.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsCity Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomAtsCity(element);
		}
	}
}
