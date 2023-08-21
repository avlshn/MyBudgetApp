using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Other;

public static class Constants
{
    #region GUI CONSTANTS
    //General duration of animation in seconds
    public const double ANIMATION_DURATION = 0.5;

    //Side panel animation
    public const double SIDE_PANEL_WIDTH = 200;
    public const double SIDE_PANEL_MINIMIZED_WIDTH = 10;

    //Plot animation
    public const double FIRST_COLOMN_WIDTH_PERCENTAGE = 40;
    public const double SECOND_ROW_HEIGHT_PERCANTAGE = 50;

    #endregion

    #region PLOT CONSTANTS

    //Donut plot resolution
    public const int DONUT_PLOT_WIDTH = 600;
    public const int DONUT_PLOT_HEIGHT = 400;

    //Donut plot settings
    public const double DONUT_PLOT_IMAGE_SCALE = 1;

    //Stacked bar plot resolution
    public const int STACKED_BAR_PLOT_WIDTH = 800;
    public const int STACKED_BAR_PLOT_HEIGHT = 400;

    
    //Stacked bar plot settings
    public const double BARS_POSITION_DELTA = 10;
    public const double BAR_WIDTH = 8;
    public const double MAX_Y_AXIS_SCALE = 1.05;
    public const double STACKED_BAR_PLOT_IMAGE_SCALE = 2;
    #endregion
}
