﻿#pragma checksum "..\..\Idioma.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CC607751EF797C5ACBAB21DACBAB00E84EC252774DE30F26A4FBF95DA13D3B53"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using LoterestTcs;
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


namespace LoterestTcs {
    
    
    /// <summary>
    /// Idioma
    /// </summary>
    public partial class Idioma : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\Idioma.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageDesarrollador;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Idioma.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton EspañolRadioButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Idioma.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton InglesRadioButton;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Idioma.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AceptarButton;
        
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
            System.Uri resourceLocater = new System.Uri("/LoterestTcs;component/idioma.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Idioma.xaml"
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
            this.imageDesarrollador = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.EspañolRadioButton = ((System.Windows.Controls.RadioButton)(target));
            
            #line 17 "..\..\Idioma.xaml"
            this.EspañolRadioButton.Checked += new System.Windows.RoutedEventHandler(this.EspañolRadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.InglesRadioButton = ((System.Windows.Controls.RadioButton)(target));
            
            #line 18 "..\..\Idioma.xaml"
            this.InglesRadioButton.Checked += new System.Windows.RoutedEventHandler(this.InglesRadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AceptarButton = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\Idioma.xaml"
            this.AceptarButton.Click += new System.Windows.RoutedEventHandler(this.AceptarButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
