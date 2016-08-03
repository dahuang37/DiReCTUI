using Microsoft.Maps.MapControl.WPF;
using System.Windows.Input;

namespace DiReCTUI.Map
{
    public class DraggablePin : Pushpin
    {
        private Microsoft.Maps.MapControl.WPF.Map map;
        private bool isDragging = false;
        Location center;

        public DraggablePin(Microsoft.Maps.MapControl.WPF.Map map)
        {
            this.map = map;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (map != null)
            {
                center = this.map.Center;

                map.ViewChangeOnFrame += Map_ViewChangeOnFrame;
                map.MouseUp += ParentMap_MouseLeftButtonUp;
                map.MouseMove += ParentMap_MouseMove;
                map.TouchMove += Map_TouchMove;
                
            }
            // Enable Dragging
            this.isDragging = true;

            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if(map != null)
            {
                center = map.Center;

                map.ViewChangeOnFrame -= Map_ViewChangeOnFrame;
                map.MouseUp -= ParentMap_MouseLeftButtonUp;
                map.MouseMove -= ParentMap_MouseMove;
                map.TouchMove -= Map_TouchMove;
            }

            isDragging = false;
            base.OnMouseLeftButtonUp(e);
        }
        
        void Map_TouchMove(object sender, TouchEventArgs e)
        {
            var map = sender as Microsoft.Maps.MapControl.WPF.Map;
            // Check if the user is currently dragging the Pushpin
            if (isDragging)
            {
                // If so, the Move the Pushpin to where the Mouse is.
                var mouseMapPosition = e.GetTouchPoint(map);
                var mouseGeocode = map.ViewportPointToLocation(mouseMapPosition.Position);
                Location = mouseGeocode;
            }
        }

        void Map_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            if (isDragging)
            {
                map.Center = center;
            }
        }

        #region "Mouse Event Handler Methods"

        void ParentMap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Left Mouse Button released, stop dragging the Pushpin
            if (map != null)
            {
                map.ViewChangeOnFrame -= Map_ViewChangeOnFrame;
                map.MouseUp -= ParentMap_MouseLeftButtonUp;
                map.MouseMove -= ParentMap_MouseMove;
                map.TouchMove -= Map_TouchMove;
            }
            this.isDragging = false;
            
        }

        void ParentMap_MouseMove(object sender, MouseEventArgs e)
        {
            var map = sender as Microsoft.Maps.MapControl.WPF.Map;
            // Check if the user is currently dragging the Pushpin
            if (this.isDragging)
            {
                // If so, the Move the Pushpin to where the Mouse is.
                var mouseMapPosition = e.GetPosition(map);
                var mouseGeocode = map.ViewportPointToLocation(mouseMapPosition);
                this.Location = mouseGeocode;
            }
        }
        #endregion
    }
}

