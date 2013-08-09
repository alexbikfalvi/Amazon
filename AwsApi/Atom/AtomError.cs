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

namespace AwsApi.Atom
{
	/// <summary>
	/// A class representing an AWS error atom object.
	/// </summary>
	[Serializable]
	public sealed class AtomError : Atom
	{
		internal static readonly string xmlPrefix = null;
		internal const string xmlName = "Error";

		/// <summary>
		/// Creates a new atom instance from the specified XML element.
		/// </summary>
		/// <param name="element">The XML element.</param>
		private AtomError(XElement element)
			: base(element, AtomError.xmlPrefix, AtomError.xmlName)
		{
			// Parse the XML element members.
			this.Code = AtomCode.ParseChild(element);
			this.Message = AtomMessage.ParseChild(element);
		}

		// Public properties.

		/// <summary>
		/// Gets the code property.
		/// </summary>
		public AtomCode Code { get; private set; }
		/// <summary>
		/// Gets the country property.
		/// </summary>
		public AtomMessage Message { get; private set; }

		// Public methods.

		/// <summary>
		/// Parses the XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <returns>The parsed atom object or null if the XML element is null.</returns>
		public static AtomError Parse(XElement element)
		{
			// If the XML element is null, return null.
			if (null == element) return null;
			// Else, return a new atom object.
			return new AtomError(element);
		}

		/// <summary>
		/// Parses the first child XML element into the corresponding atom object.
		/// </summary>
		/// <param name="element">The parent XML element.</param>
		/// <returns>The parsed atom object or null if no child is found.</returns>
		public static AtomError ParseChild(XElement element)
		{
			// If the XML element is null, throw an exception.
			if (null == element) throw new AtomException("Parent element cannot be null.");
			// Parse the first child element.
			return AtomError.Parse(element.Element(AtomError.xmlPrefix, AtomError.xmlName));
		}
	}
}
