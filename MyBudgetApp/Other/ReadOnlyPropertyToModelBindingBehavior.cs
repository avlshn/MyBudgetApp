using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyBudgetApp.Other;

public class ReadOnlyPropertyToModelBindingBehavior : Behavior<UIElement>
{
    public object ReadOnlyDependencyProperty
    {
        get { return (object)GetValue(ReadOnlyDependencyPropertyProperty); }
        set { SetValue(ReadOnlyDependencyPropertyProperty, value); }
    }

    public static readonly DependencyProperty ReadOnlyDependencyPropertyProperty =
        DependencyProperty.Register("ReadOnlyDependencyProperty", typeof(object), typeof(ReadOnlyPropertyToModelBindingBehavior),
            new PropertyMetadata(null, OnReadOnlyDependencyPropertyPropertyChanged));

    public object ModelProperty
    {
        get { return (object)GetValue(ModelPropertyProperty); }
        set { SetValue(ModelPropertyProperty, value); }
    }

    public static readonly DependencyProperty ModelPropertyProperty =
        DependencyProperty.Register("ModelProperty", typeof(object), typeof(ReadOnlyPropertyToModelBindingBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    private static void OnReadOnlyDependencyPropertyPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var b = obj as ReadOnlyPropertyToModelBindingBehavior;
        b.ModelProperty = e.NewValue;
    }
}