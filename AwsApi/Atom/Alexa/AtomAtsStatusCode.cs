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
using System.Xml.Linq;
using DotNetApi.Xml;

namespace AwsApi.Atom.Alexa
{
	/// <summary>
	/// A class representing an AWS status code atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomAtsStatusCode : Atom
	{
		internal const string xmlPrefix = "aws";
		internal const string xmlName = "StatusCode";

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomAtsStatusCode(XElement element)
			: base(element, AtomAtsStatusCode.xmlPrefix, AtomAtsStatusCode.xmlName)
		{
			// Parse the XML element value.
			this.Value = element.Value;
		}

		// Public properties.

		/// <summary>
		/// Gets the value of the current atom element.
		/// </summary>
		public string Value { get; private set; }

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsStatusCode Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomAtsStatusCode(element);
		}

		/// <summary>
		/// Parses the first child XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The parent XML element.</param>
		/// <returns>The parsed atom object or null if no child is found.</returns>
		public static AtomAtsStatusCode ParseChild(XElement element)
		{
			// If the XML element is null, throw an exception.
			if (null == element) throw new AtomException("Parent element cannot be null.");
			// Parse the first child element.
			return AtomAtsStatusCode.Parse(element.Element(AtomAtsStatusCode.xmlPrefix, AtomAtsStatusCode.xmlName));
		}

		/// <summary>
		/// Converts this object into a string.
		/// </summary>
		/// <returns>The string.</returns>
		public override string ToString()
		{
			return this.Value;
		}
	}
}
