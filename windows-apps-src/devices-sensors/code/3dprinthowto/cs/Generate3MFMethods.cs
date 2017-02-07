using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Graphics.Printing3D;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace _3DPrintHowTo {


    /// <summary>
    /// This Class is not currently referenced in the executable code. It holds several methods that can be 
    /// used to generate a 3MF Pacakge. Its purpose is to provide the code snippets for conceptual doc content.
    /// </summary>
    class Generate3MFMethods {

        private async Task<bool> CreateData() {

            //<SnippetInitClasses>
            var localPackage = new Printing3D3MFPackage();
            var model = new Printing3DModel();
            // specify scaling units for model data
            model.Unit = Printing3DModelUnit.Millimeter;

            //</SnippetInitClasses>



            // create new mesh on model
            var mesh = new Printing3DMesh();

            // create vertices on the mesh
            await this.GetVerticesAsync(mesh);

            // create triangles on the mesh
            await SetTriangleIndicesAsync(mesh);

            //<SnippetMeshAdd>
            // add the mesh to the model
            model.Meshes.Add(mesh);
            //</SnippetMeshAdd>


            // create material indices
            await SetMaterialIndicesAsync(mesh);

            //<SnippetBaseMaterialGroup>
            // add material group
            // all material indices need to start from 1: 0 is a reserved id
            // create new base materialgroup with id = 1
            var baseMaterialGroup = new Printing3DBaseMaterialGroup(1);

            // create color objects
            // 'A' should be 255 if alpha = 100%
            var darkBlue = Windows.UI.Color.FromArgb(255, 20, 20, 90);
            var orange = Windows.UI.Color.FromArgb(255, 250, 120, 45);
            var teal = Windows.UI.Color.FromArgb(255, 1, 250, 200);

            // create new ColorMaterials, assigning color objects
            var colrMat = new Printing3DColorMaterial();
            colrMat.Color = darkBlue;

            var colrMat2 = new Printing3DColorMaterial();
            colrMat2.Color = orange;

            var colrMat3 = new Printing3DColorMaterial();
            colrMat3.Color = teal;

            // setup new materials using the ColorMaterial objects
            // set desired material type in the Name property
            var baseMaterial = new Printing3DBaseMaterial {
                Name = Printing3DBaseMaterial.Pla,
                Color = colrMat
            };

            var baseMaterial2 = new Printing3DBaseMaterial {
                Name = Printing3DBaseMaterial.Abs,
                Color = colrMat2
            };

            // add base materials to the basematerialgroup

            // material group index 0
            baseMaterialGroup.Bases.Add(baseMaterial);
            // material group index 1
            baseMaterialGroup.Bases.Add(baseMaterial2);

            // add material group to the basegroups property of the model
            model.Material.BaseGroups.Add(baseMaterialGroup);
            //</SnippetBaseMaterialGroup>


            //<SnippetColorMaterialGroup>
            // add ColorMaterials to the Color Material Group (with id 2)
            var colorGroup = new Printing3DColorMaterialGroup(2);

            // add the previous ColorMaterial objects to this ColorMaterialGroup
            colorGroup.Colors.Add(colrMat);
            colorGroup.Colors.Add(colrMat2);
            colorGroup.Colors.Add(colrMat3);

            // add colorGroup to the ColorGroups property on the model
            model.Material.ColorGroups.Add(colorGroup);
            //</SnippetColorMaterialGroup>

            //<SnippetMetadata>
            model.Metadata.Add("Title", "Cube");
            model.Metadata.Add("Designer", "John Smith");
            model.Metadata.Add("CreationDate", "1/1/2016");
            //</SnippetMetadata>




            //<SnippetCompositeMaterialGroup>
            // CompositeGroups
            // create new composite material group with id = 3
            var compositeGroup = new Printing3DCompositeMaterialGroup(3);

            // indices point to base materials in BaseMaterialGroup with id =1
            compositeGroup.MaterialIndices.Add(0);
            compositeGroup.MaterialIndices.Add(1);

            // create new composite materials
            var compMat = new Printing3DCompositeMaterial();
            // fraction adds to 1.0
            compMat.Values.Add(0.2); // .2 of first base material in BaseMaterialGroup 1
            compMat.Values.Add(0.8); // .8 of second base material in BaseMaterialGroup 1

            var compMat2 = new Printing3DCompositeMaterial();
            // fraction adds to 1.0
            compMat2.Values.Add(0.5);
            compMat2.Values.Add(0.5);

            var compMat3 = new Printing3DCompositeMaterial();
            // fraction adds to 1.0
            compMat3.Values.Add(0.8);
            compMat3.Values.Add(0.2);

            var compMat4 = new Printing3DCompositeMaterial();
            // fraction adds to 1.0
            compMat4.Values.Add(0.4);
            compMat4.Values.Add(0.6);

            // add composites to group
            compositeGroup.Composites.Add(compMat);
            compositeGroup.Composites.Add(compMat2);
            compositeGroup.Composites.Add(compMat3);
            compositeGroup.Composites.Add(compMat4);

            // add group to model
            model.Material.CompositeGroups.Add(compositeGroup);
            //</SnippetCompositeMaterialGroup>

            //<SnippetTextureResource>
            // texture resource setup
            Printing3DTextureResource texResource = new Printing3DTextureResource();
            // name conveys the path within the 3MF document
            texResource.Name = "/3D/Texture/msLogo.png";
            
            // in this case, we reference texture data in the sample appx, convert it to 
            // an IRandomAccessStream, and assign it as the TextureData
            Uri texUri = new Uri("ms-appx:///Assets/msLogo.png");
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(texUri);
            IRandomAccessStreamWithContentType iRandomAccessStreamWithContentType = await file.OpenReadAsync();
            texResource.TextureData = iRandomAccessStreamWithContentType;
            // add this testure resource to the 3MF Package
            localPackage.Textures.Add(texResource);

            // assign this texture resource to a Printing3DModelTexture
            var modelTexture = new Printing3DModelTexture();
            modelTexture.TextureResource = texResource;
            //</SnippetTextureResource>

            //<SnippetTexture2CoordMaterialGroup>
            // texture2Coord Group
            // create new Texture2CoordMaterialGroup with id = 4
            var tex2CoordGroup = new Printing3DTexture2CoordMaterialGroup(4);

            // create texture materials:
            // set up four tex2coordmaterial objects with four (u,v) pairs, 
            // mapping to each corner of the image:

            var tex2CoordMaterial = new Printing3DTexture2CoordMaterial();
            tex2CoordMaterial.U = 0.0;
            tex2CoordMaterial.V = 1.0;
            tex2CoordGroup.Texture2Coords.Add(tex2CoordMaterial);

            var tex2CoordMaterial2 = new Printing3DTexture2CoordMaterial();
            tex2CoordMaterial2.U = 1.0;
            tex2CoordMaterial2.V = 1.0;
            tex2CoordGroup.Texture2Coords.Add(tex2CoordMaterial2);

            var tex2CoordMaterial3 = new Printing3DTexture2CoordMaterial();
            tex2CoordMaterial3.U = 0.0;
            tex2CoordMaterial3.V = 0.0;
            tex2CoordGroup.Texture2Coords.Add(tex2CoordMaterial3);

            var tex2CoordMaterial4 = new Printing3DTexture2CoordMaterial();
            tex2CoordMaterial4.U = 1.0;
            tex2CoordMaterial4.V = 0.0;
            tex2CoordGroup.Texture2Coords.Add(tex2CoordMaterial4);

            // add our Printing3DModelTexture to the Texture property of the group
            tex2CoordGroup.Texture = modelTexture;

            // add metadata about the texture so that u,v values can be used
            model.Metadata.Add("tex4", "/3D/Texture/msLogo.png");
            // add group to groups on the model's material
            model.Material.Texture2CoordGroups.Add(tex2CoordGroup);
            //</SnippetTexture2CoordMaterialGroup>


            //<SnippetComponents>
            // create new component
            Printing3DComponent component = new Printing3DComponent();

            // assign mesh to the component's mesh
            component.Mesh = mesh;

            // add component to the model's list of all used components
            // a model can have references to multiple components
            model.Components.Add(component);

            // create the transform matrix
            var componentWithMatrix = new Printing3DComponentWithMatrix();
            // assign component to this componentwithmatrix
            componentWithMatrix.Component = component;

            // create an identity matrix
            var identityMatrix = Matrix4x4.Identity;

            // use the identity matrix as the transform matrix (no transformation)
            componentWithMatrix.Matrix = identityMatrix;

            // add component to the build property.
            model.Build.Components.Add(componentWithMatrix);
            //</SnippetComponents>

            
            //<SnippetSavePackage>
            // save the model to the package:
            await localPackage.SaveModelToPackageAsync(model);
            // get the model stream
            var modelStream = localPackage.ModelPart;

            // fix any textures in the model file
            localPackage.ModelPart = await FixTextureContentType(modelStream);
            //</SnippetSavePackage>

            return true;
        }
        


        /// <summary>
        /// Set materialindices on the mesh
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        //<SnippetMaterialIndices>
        private static async Task SetMaterialIndicesAsync(Printing3DMesh mesh) {
            // declare a description of the material indices
            Printing3DBufferDescription description;
            description.Format = Printing3DBufferFormat.Printing3DUInt;
            // 4 indices for material description per triangle
            description.Stride = 4;
            // 12 triangles total
            mesh.IndexCount = 12;
            mesh.TriangleMaterialIndicesDescription = description;

            // create space for storing this data
            mesh.CreateTriangleMaterialIndices(sizeof(UInt32) * 4 * 12);

            {
                // each row is a triangle face (in the order they were created)
                // first column is the id of the material group, last 3 columns show which material id (within that group)
                // maps to each triangle vertex (in the order they were listed when creating triangles)
                UInt32[] indices =
                {
                    // base materials:
                    // in  the BaseMaterialGroup (id=1), the BaseMaterial with id=0 will be applied to these triangle vertices
                    1, 0, 0, 0, 
                    1, 0, 0, 0,
                    // color materials:
                    // in the ColorMaterialGroup (id=2), the ColorMaterials with these ids will be applied to these triangle vertices
                    2, 1, 1, 1,
                    2, 1, 1, 1,
                    2, 0, 0, 0,
                    2, 0, 0, 0,
                    2, 0, 1, 2,
                    2, 1, 0, 2,
                    // composite materials:
                    // in the CompositeMaterialGroup (id=3), the CompositeMaterial with id=0 will be applied to these triangles
                    3,0,0,0,
                    3,0,0,0,
                    // texture materials:
                    // in the Texture2CoordMaterialGroup (id=4), each texture coordinate is mapped to the appropriate vertex on these
                    // two adjacent triangle faces, so that the square face they create displays the original rectangular image
                    4, 0, 3, 1,
                    4, 2, 3, 0,
                };

                // get the current (unassigned) vertex data as a stream and write our new 'indices' data to it.
                var stream = mesh.GetTriangleMaterialIndices().AsStream();
                var vertexData = indices.SelectMany(v => BitConverter.GetBytes(v)).ToArray();
                var len = vertexData.Length;
                await stream.WriteAsync(vertexData, 0, vertexData.Length);
            }
        }
        //</SnippetMaterialIndices>


        /// <summary>
        /// Set the triangle indices on the mesh
        /// </summary>
        /// <param name="mesh">Printing3DMesh</param>
        /// <returns></returns>
        //<SnippetTriangleIndices>
        private static async Task SetTriangleIndicesAsync(Printing3DMesh mesh) {

            Printing3DBufferDescription description;

            description.Format = Printing3DBufferFormat.Printing3DUInt;
            // 3 vertex indices
            description.Stride = 3;
            // 12 triangles in all in the cube
            mesh.IndexCount = 12;

            mesh.TriangleIndicesDescription = description;

            // allocate space for 12 triangles
            mesh.CreateTriangleIndices(sizeof(UInt32) * 3 * 12);

            // get a datastream of the triangle indices (should be blank at this point)
            var stream2 = mesh.GetTriangleIndices().AsStream();
            {
                // define a set of triangle indices: each row is one triangle. The values in each row
                // correspond to the index of the vertex. 
                UInt32[] indices =
                {
                    1, 0, 2,
                    1, 2, 3,
                    0, 1, 5,
                    0, 5, 4,
                    1, 3, 7,
                    1, 7, 5,
                    2, 7, 3,
                    2, 6, 7,
                    0, 6, 2,
                    0, 4, 6,
                    6, 5, 7,
                    4, 5, 6,
                };
                // convert index data to byte array
                var vertexData = indices.SelectMany(v => BitConverter.GetBytes(v)).ToArray();
                var len = vertexData.Length;
                // write index data to the triangle indices stream
                await stream2.WriteAsync(vertexData, 0, vertexData.Length);
            }

        }
        //</SnippetTriangleIndices>

        //<SnippetVertices>
        private async Task GetVerticesAsync(Printing3DMesh mesh) {
            Printing3DBufferDescription description;

            description.Format = Printing3DBufferFormat.Printing3DDouble;

            // have 3 xyz values
            description.Stride = 3;

            // have 8 vertices in all in this mesh
            mesh.CreateVertexPositions(sizeof(double) * 3 * 8);
            mesh.VertexPositionsDescription = description;

            // set the locations (in 3D coordinate space) of each vertex
            using (var stream = mesh.GetVertexPositions().AsStream()) {
                double[] vertices =
                {
                    0, 0, 0,
                    10, 0, 0,
                    0, 10, 0,
                    10, 10, 0,
                    0, 0, 10,
                    10, 0, 10,
                    0, 10, 10,
                    10, 10, 10,
                };

                // convert vertex data to a byte array
                byte[] vertexData = vertices.SelectMany(v => BitConverter.GetBytes(v)).ToArray();

                // write the locations to each vertex
                await stream.WriteAsync(vertexData, 0, vertexData.Length);
            }
            // update vertex count: 8 vertices in the cube
            mesh.VertexCount = 8;
        }
        //</SnippetVertices>

        /// <summary>
        /// Fixes issue in API where textures are not saved correctly
        /// </summary>
        /// <param name="modelStream">3dmodel.model data</param>
        /// <returns></returns>
        private async Task<IRandomAccessStream> FixTextureContentType(IRandomAccessStream modelStream) {
            XDocument xmldoc = XDocument.Load(modelStream.AsStreamForRead());

            var outputStream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
            var writer = new Windows.Storage.Streams.DataWriter(outputStream);
            writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
            writer.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;
            writer.WriteString("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            var text = xmldoc.ToString();
            // ensure that content type is set correctly
            // texture content can be either png or jpg
            var replacedText = text.Replace("contenttype=\"\"", "contenttype=\"image/png\"");
            writer.WriteString(replacedText);

            await writer.StoreAsync();
            await writer.FlushAsync();
            writer.DetachStream();
            return outputStream;
        }

        //<SnippetSaveTo3mf>
        private async void SaveTo3mf(Printing3D3MFPackage localPackage) {

            // prompt the user to choose a location to save the file to
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.DefaultFileExtension = ".3mf";
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("3MF File", new[] { ".3mf" });
            var storageFile = await savePicker.PickSaveFileAsync();
            if (storageFile == null) {
                return;
            }

            // save the 3MF Package to an IRandomAccessStream
            using (var stream = await localPackage.SaveAsync()) {
                // go to the beginning of the stream
                stream.Seek(0);

                // read from the file stream and write to a buffer
                using (var dataReader = new DataReader(stream)) {
                    await dataReader.LoadAsync((uint)stream.Size);
                    var buffer = dataReader.ReadBuffer((uint)stream.Size);

                    // write from the buffer to the storagefile specified
                    await FileIO.WriteBufferAsync(storageFile, buffer);
                }
            }
        }
        //</SnippetSaveTo3mf>
    }
}
