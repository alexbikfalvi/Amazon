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
	/// A class representing an AWS per million atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomAtsPerMillion : Atom
	{
		internal const string xmlPrefix = "aws";
		internal const string xmlName = "PerMillion";

		private decimal value;

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <param name="top">The top XML namespace.</param>
		private AtomAtsPerMillion(XElement element)
		{
			// Check the XML element name.
			if (!element.HasName(AtomAtsPerMillion.xmlPrefix, AtomAtsPerMillion.xmlName)) throw new AtomException("XML element name mismatch.", element);

			// Parse the XML element value.
			this.value = decimal.Parse(element.Value);
		}

		// Public properties.

		/// <summary>
		/// Gets the value of the current atom element.
		/// </summary>
		public decimal Value { get { return this.value; } }

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsPerMillion Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomAtsPerMillion(element);
		}
	}
}
