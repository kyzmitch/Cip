using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable 1591

namespace Cip
{
    //Copyright (C) Microsoft Corporation.  All rights reserved.

    // The AuthorAttribute class is a user-defined attribute class.
    // It can be applied to classes and struct declarations only.
    // It takes one unnamed string argument (the author's name).
    // It has one optional named argument Version, which is of type int.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class AuthorAttribute : Attribute
    {
        // This constructor specifies the unnamed arguments to the attribute class.
        public AuthorAttribute(string name)
        {
            this.name = name;
            this.version = 0;
        }

        // This property is readonly (it has no set accessor)
        // so it cannot be used as a named argument to this attribute.
        public string Name
        {
            get
            {
                return name;
            }
        }

        // This property is read-write (it has a set accessor)
        // so it can be used as a named argument when using this
        // class as an attribute class.
        public int Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        public override string ToString()
        {
            string value = "Author : " + Name;
            if (version != 0)
            {
                value += " Version : " + Version.ToString();
            }
            return value;
        }

        private string name;
        private int version;
    }

}
