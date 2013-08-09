﻿/* 
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
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using DotNetApi.Xml;

namespace AwsApi.Atom.Alexa
{
	/// <summary>
	/// A class representing an AWS cities atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomAtsCities : Atom, IEnumerable<AtomAtsCity>
	{
		internal const string xmlPrefix = "aws";
		internal const string xmlName = "Cities";

		private List<AtomAtsCity> sites = new List<AtomAtsCity>();

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomAtsCities(XElement element)
			: base(element, AtomAtsCities.xmlPrefix, AtomAtsCities.xmlName)
		{
			// Parse the XML element members.
			foreach (XElement el in element.Elements(AtomAtsCity.xmlPrefix, AtomAtsCity.xmlName))
			{
				// Add a new site atom object to the list.
				this.sites.Add(AtomAtsCity.Parse(el));
			}
		}

		// Public properties.

		/// <summary>
		/// Gets the atom city at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The atom site.</returns>
		public AtomAtsCity this[int index]
		{
			get { return this.sites[index]; }
		}

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsCities Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomAtsCities(element);
		}

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsCities ParseChild(XElement element)
		{
			// If the XML element is null, throw an exception.
			if (null == element) throw new AtomException("Parent element cannot be null.");
			// Else, return a new atom object.
			return AtomAtsCities.Parse(element.Element(AtomAtsCities.xmlPrefix, AtomAtsCities.xmlName));
		}

		/// <summary>
		/// Returns the enumerator for the list of categories.
		/// </summary>
		/// <returns>The enumerator.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Returns the enumerator for the list of categories.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<AtomAtsCity> GetEnumerator()
		{
			return this.sites.GetEnumerator();
		}
	}
}
