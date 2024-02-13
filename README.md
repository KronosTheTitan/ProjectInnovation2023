# Project Innovation 2023
Unity version = 2022.3.19f1
## Style Guidelines Art
These guidelines are in relation to naming structure, file organization and related tasks. It does not affect the art style in any way. If additional file types need to be covered these can be updated.
- All models are OBJ unless otherwise needed.
- In the event OBJ does not provide all the needed features FBX will be the next best file type.
- All models follow the naming rule M_MeshName.fileExtension.
- All textures follow the naming rule T_MeshName_TextureTypeLetter.png.
  - A = albedo
  - N = normal
  - M = metallic
  - E = emissive
## Style Guidelines Code
- Everything is private unless otherwise needed.
- Nesting should be kept to a minimum.
- All files should be PascalCased.
- Guidelines for naming of code elements:
  - [SerializeField] private int exampleName;
  - public int exampleName;
  - public int ExampleName(int exampleParameter);
  - public class ExampleClass