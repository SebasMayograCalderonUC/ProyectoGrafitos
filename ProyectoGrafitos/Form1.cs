using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace ProyectoGrafitos
{
    public partial class Form1 : Form
    {
        private GMapOverlay markerOverLay;
        private double latInicial = 9.924264;
        private double longInicial = -84.1909738;
        private int color = 0;
        private string pathToDelete;
        private bool updating = false;
        private bool addMarker = true;
        private int posA, posB;
        GMarkerGoogleType[] colors=new GMarkerGoogleType[]{
            GMarkerGoogleType.blue, GMarkerGoogleType.green, GMarkerGoogleType.lightblue,
            GMarkerGoogleType.orange, GMarkerGoogleType.pink, GMarkerGoogleType.purple, GMarkerGoogleType.red, GMarkerGoogleType.yellow
        };

        private Graph graph;

        private string markerName;
        private int markerPoss = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(this.latInicial, this.longInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;
            this.markerOverLay= new GMapOverlay("Markers");
            gMapControl1.Overlays.Add(this.markerOverLay);
            gMapControl1.Overlays.Add(new GMapOverlay("Routes"));
            graph = new Graph();
            updateMarkers();
           
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
            gMapControl1.Zoom = gMapControl1.Zoom - 1;
        }

        private void updateMarkers()
        {
            
            gMapControl1.Overlays[0].Clear();
            var aux = graph.first;
            while (aux != null)
            {
                var marker = new GMarkerGoogle(aux.getValue().possition, colors[color]);
                changeColors();
                markerOverLay.Markers.Add(marker);
                aux = aux.getNextNode();
            }
        }

        private void updateRoutues()
        {
            gMapControl1.Overlays[1].Routes.Clear();
            gMapControl1.Overlays.Add(graph.setRoutes(gMapControl1.Overlays[1]));
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
            gMapControl1.Zoom = gMapControl1.Zoom - 1;
        }


        private void updateMarker( string markerName, PointLatLng possition)
        {
            graph.searchVertice(markerName).getValue().possition = possition;
            markerOverLay.Markers[markerPoss].Position = possition;
        }

        private void alterMarker(PointLatLng possition)
        {
            var marker = markerOverLay.Markers[markerPoss];
            marker.Position = possition;
        }

        private void changeColors()
        {
            if (color == colors.Length - 1){
                color = 0;
            }else{
                color++;
            }
        }

        private PointLatLng getPosition(double lat, double lng)
        {
            return new PointLatLng(lat,lng);
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
   
            if (addMarker){
                var marker = new GMarkerGoogle(getPosition(lat,lng), colors[color]);
                markerOverLay.Markers.Add(marker);
                addMarker = !addMarker;
                changeColors();
            }else{
                this.alterMarker(new PointLatLng(lat,lng));
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!updating)
            {
                var marker = markerOverLay.Markers[markerPoss];
                marker.ToolTipMode = MarkerTooltipMode.Always;
                marker.ToolTipText = textBox1.Text;
                dataGridView1.Rows.Add(textBox1.Text, marker.Position.Lat, marker.Position.Lng);
                addMarker = true;
                markerPoss++;
                graph.addVertice(new Location(textBox1.Text, marker.Position.Lat, marker.Position.Lng));
                dataGridView2.Rows.Add(textBox1.Text);
                dataGridView3.Rows.Add(textBox1.Text);
                textBox1.Text = "";

            }
          
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updating = true;
            var index = e.RowIndex;
            addMarker = false;
            markerPoss = index;
            markerName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox1.Text = markerName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updating = false;
            graph.searchVertice(markerName).getValue().possition = markerOverLay.Markers[markerPoss].Position;
            markerPoss = markerOverLay.Markers.Count;
            updateRoutues();
            addMarker = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            posB = e.RowIndex;
            gMapControl1.Position = markerOverLay.Markers[posB].Position;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                graph.addPath(dataGridView1.Rows[posA].Cells[0].Value.ToString(), dataGridView3.Rows[posB].Cells[0].Value.ToString(), textBox2.Text);
                updateRoutues();
                gMapControl1.Zoom = gMapControl1.Zoom + 1;
                gMapControl1.Zoom = gMapControl1.Zoom - 1;
                AddPathsToTable();

            }
            catch (Exception exception)
            {
                MessageBox.Show("Error intente denuevo");
            }
           

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            posA = e.RowIndex;
            gMapControl1.Position = markerOverLay.Markers[posA].Position;
        }

        private void Paths_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pathToDelete = Paths.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            graph.removePath(pathToDelete);
            updateRoutues();
        }

        private void AddPathsToTable()
        {
            Paths.Rows.Clear();
            var listOfLists = graph.getAllPaths();
            foreach (var list in listOfLists)
            {
                foreach (var path in list)
                {
                    Paths.Rows.Add(path.origin.getValue().name, path.destination.getValue().name, path.value);
                }
            }
        }
    }
}
