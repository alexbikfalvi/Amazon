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
	/// A class representing an AWS country atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomAtsCountry : Atom
	{
		internal const string xmlPrefix = "aws";
		internal const string xmlName = "Country";

		private AtomAtsRank rank;
		private AtomAtsReach reach;
		private AtomAtsPageViews pageViews;

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomAtsCountry(XElement element)
		{
			// Check the XML element name.
			if (!element.HasName(AtomAtsCountry.xmlPrefix, AtomAtsCountry.xmlName)) throw new AtomException("XML element name mismatch.", element);

			// Parse the XML element members.
			this.rank = AtomAtsRank.Parse(element.Element(AtomAtsRank.xmlPrefix, AtomAtsRank.xmlName));
			this.reach = AtomAtsReach.Parse(element.Element(AtomAtsReach.xmlPrefix, AtomAtsReach.xmlName));
			this.pageViews = AtomAtsPageViews.Parse(element.Element(AtomAtsPageViews.xmlPrefix, AtomAtsPageViews.xmlName));
		}

		// Public properties.

		/// <summary>
		/// Gets the rank property.
		/// </summary>
		public AtomAtsRank Rank { get { return this.rank; } }
		/// <summary>
		/// Gets the reach property.
		/// </summary>
		public AtomAtsReach Reach { get { return this.reach; } }
		/// <summary>
		/// Gets the page views property.
		/// </summary>
		public AtomAtsPageViews PageViews { get { return this.pageViews; } }

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsCountry Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomAtsCountry(element);
		}
	}
}
