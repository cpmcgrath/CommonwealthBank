﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using CMcG.CommonwealthBank.Logic;

namespace CMcG.CommonwealthBank.Data
{
    [Table]
    public class ScreenSecurity : NotifyBase
    {
        int         m_id;
        string      m_typeFullName;
        AccessLevel m_accessLevel;
        Type        m_viewModelType;

        [Column(AutoSync=AutoSync.OnInsert, IsPrimaryKey=true, IsDbGenerated=true)]
        public int Id
        {
            get { return m_id; }
            set
            {
                if (m_id == value)
                    return;

                SendPropertyChanging();
                m_id = value;
                SendPropertyChanged("Id");
            }
        }

        [Column(CanBeNull=false)]
        public string TypeFullName
        {
            get { return m_typeFullName; }
            set
            {
                if (m_typeFullName == value)
                    return;

                SendPropertyChanging();
                m_typeFullName = value;
                SendPropertyChanged("TypeFullName");
            }
        }

        [Column]
        public AccessLevel AccessLevel
        {
            get { return m_accessLevel; }
            set
            {
                if (m_accessLevel == value)
                    return;

                SendPropertyChanging();
                m_accessLevel = value;
                SendPropertyChanged("AccessLevel");
            }
        }

        public Type ViewModelType
        {
            get
            {
                if (m_viewModelType == null)
                    m_viewModelType = Type.GetType(TypeFullName);
                return m_viewModelType;
            }
            set
            {
                m_viewModelType = value;
                TypeFullName = value.FullName + ", " + value.Assembly.GetName().Name;
            }
        }

        public string Name
        {
            get
            {
                var name = ViewModelType.GetFriendlyName();
                name = name.Replace(" View Model", "");

                if (name.EndsWith(" Edit"))
                    return name.Substring(0, name.Length - 5);
                else
                    return name;
            }
        }

        public string Group
        {
            get
            {
                var index = ViewModelType.Namespace.IndexOf("ViewModels.") + "ViewModels.".Length;
                return ViewModelType.Namespace.Substring(index).Replace(".", ": ");
            }
        }

        public ScreenSecurity Load(Type vm, AccessLevel level)
        {
            ViewModelType = vm;
            AccessLevel = level;
            return this;
        }

        public AccessLevel[] AccessLevels
        {
            get
            {
                return Enum.GetValues(typeof(AccessLevel))
                            .Cast<AccessLevel>()
                            .Except(new[] { AccessLevel.CreateLogin }).ToArray();
            }
        }
    }
}
