﻿#pragma checksum "..\..\..\PersPages\BasketPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "89B019BC6E5B79C6D79EAB41FBBC0BC1E074B5BD6550F5A047B1FC9DDE958A10"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Optika.PersPages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Optika.PersPages {
    
    
    /// <summary>
    /// BasketPage
    /// </summary>
    public partial class BasketPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\..\PersPages\BasketPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Search;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\PersPages\BasketPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Name;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\PersPages\BasketPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Option;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\PersPages\BasketPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Price;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\PersPages\BasketPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listPills;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\PersPages\BasketPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ClosedBorder;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Optika;component/perspages/basketpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PersPages\BasketPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Search = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\PersPages\BasketPage.xaml"
            this.Search.Click += new System.Windows.RoutedEventHandler(this.Search_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Name = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\PersPages\BasketPage.xaml"
            this.Name.Click += new System.Windows.RoutedEventHandler(this.Name_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Option = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\PersPages\BasketPage.xaml"
            this.Option.Click += new System.Windows.RoutedEventHandler(this.Option_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Price = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\PersPages\BasketPage.xaml"
            this.Price.Click += new System.Windows.RoutedEventHandler(this.Price_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.listPills = ((System.Windows.Controls.ListBox)(target));
            
            #line 115 "..\..\..\PersPages\BasketPage.xaml"
            this.listPills.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listPills_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ClosedBorder = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

