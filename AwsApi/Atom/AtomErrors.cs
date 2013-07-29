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
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using DotNetApi.Xml;

namespace AwsApi.Atom
{
	/// <summary>
	/// A class representing an AWS errors atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomErrors : Atom, IEnumerable<AtomError>
	{
		internal static readonly string xmlPrefix = null;
		internal const string xmlName = "Errors";

		private List<AtomError> errors = new List<AtomError>();

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomErrors(XElement element)
		{
			// Check the XML element name.
			if (!element.HasName(AtomErrors.xmlPrefix, AtomErrors.xmlName)) throw new AtomException("XML element name mismatch.", element);

			// Parse the XML element members.
			foreach (XElement el in element.Elements(AtomError.xmlPrefix, AtomError.xmlName))
			{
				// Add a new site atom object to the list.
				this.errors.Add(AtomError.Parse(el));
			}
		}

		// Public properties.

		/// <summary>
		/// Gets the atom error at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The atom error.</returns>
		public AtomError this[int index]
		{
			get { return this.errors[index]; }
		}

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomErrors Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomErrors(element);
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
		public IEnumerator<AtomError> GetEnumerator()
		{
			return this.errors.GetEnumerator();
		}
	}
}
