/* 
 * Copyright (C) 2012-2013 Alex Bikfalvi
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
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Linq;
using DotNetApi.Xml;

namespace AwsApi.Atom
{
	/// <summary>
	/// A class representing an atom exception.
	/// </summary>
	[Serializable]
	public sealed class AtomException : Exception, ISerializable
	{
		private string xml;

		/// <summary>
		/// Creates a new atom exception instance.
		/// </summary>
		/// <param name="message">The exception message.</param>
		public AtomException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Creates a new atom exception instance.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="xml">The XML element that cannot be parsed.</param>
		public AtomException(string message, XElement xml)
			: base(message)
		{
			this.xml = xml.ToString(SaveOptions.None);
		}

		/// <summary>
		/// Creates a new atom exception instance.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="xml">The XML element that cannot be parsed.</param>
		/// <param name="innerException">The inner exception.</param>
		public AtomException(string message, XElement xml, Exception innerException)
			: base(message, innerException)
		{
			this.xml = xml.ToString(SaveOptions.None);
		}

		/// <summary>
		/// Creates a new atom exception instance during the deserialization.
		/// </summary>
		/// <param name="info">The serialization info.</param>
		/// <param name="ctx">The streaming context.</param>
		private AtomException(SerializationInfo info, StreamingContext ctx)
			: base(info, ctx)
		{
			this.xml = info.GetValue("xml", typeof(string)) as string;
		}

		/// <summary>
		/// Returns the object data during serialization.
		/// </summary>
		/// <param name="info">The serialization info.</param>
		/// <param name="context">The streaming context.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("xml", this.xml);
			base.GetObjectData(info, context);
		}

		/// <summary>
		/// Gets the XML string that generated the exception.
		/// </summary>
		public string Xml { get { return this.xml; } }
	}
}
