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
	/// A class representing an AWS response status atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomAtsResponseStatus : Atom
	{
		internal const string xmlPrefix = "aws";
		internal const string xmlName = "ResponseStatus";

		private AtomAtsStatusCode statusCode;

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomAtsResponseStatus(XElement element)
		{
			// Check the XML element name.
			if (!element.HasName(AtomAtsResponseStatus.xmlPrefix, AtomAtsResponseStatus.xmlName)) throw new AtomException("XML element name mismatch.", element);

			// Parse the XML element members.
			this.statusCode = AtomAtsStatusCode.Parse(element.Element(AtomAtsStatusCode.xmlPrefix, AtomAtsStatusCode.xmlName));
		}

		// Public properties.

		/// <summary>
		/// Gets the status code property.
		/// </summary>
		public AtomAtsStatusCode StatusCode { get { return this.statusCode; } }

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomAtsResponseStatus Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomAtsResponseStatus(element);
		}
	}
}
