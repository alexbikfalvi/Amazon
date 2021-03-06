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

namespace AwsApi.Atom
{
	/// <summary>
	/// The base class of an atom object.
	/// </summary>
	[Serializable]
	public abstract class Atom
	{
		/// <summary>
		/// Creates a new atom instance from the specified XML element with the given prefix and name.
		/// </summary>
		/// <param name="element">The XML element.</param>
		/// <param name="xmlPrefix">The element prefix.</param>
		/// <param name="xmlName">The element name.</param>
		protected Atom(XElement element, string xmlPrefix, string xmlName)
		{
			// Check the XML element name.
			if (!element.HasName(xmlPrefix, xmlName)) throw new AtomException("XML element name mismatch.", element);
		}
	}
}
