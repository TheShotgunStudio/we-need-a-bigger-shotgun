Closes #<issue-number>
### Summary


## Checklist
### 1. Merging
- [] Did you select the dev branch?
### 2. File Structure
- [] Are all scripts placed within Assets/Scripts and organized into logical categories using PascalCase (e.g., Core, Player, Level)?
- [] Are assets like materials, textures and sound files named using lower_snake_case (e.g., diamond_pickaxe.png)?
- [] Did the changes avoid unnecessary scene modifications? If new changes were needed for testing, were they done in Scenes/Dev/<Specification> and then discarded?
### 3. Code
- [] Do private fields start with an underscore and use camelCase (e.g., _privateVariable)?
- [] Are static readonly variables named using UPPER_SNAKE_CASE?
- [] Are enum types also in UPPER_SNAKE_CASE?
- [] Are function and variable names clear, concise, and descriptive without using unnecessary abbreviations?
- [] Are all public functions and fields documented with clear summaries where needed
- [] Does the code maintain a strict hierarchy, ensuring objects do not fetch references themselves or their parents in the hierarchy?
- [] Are references passed down through dependency injection as needed?Does each component only depend on its parent in the hierarchy?
- [] Are there any unnecessary Debug.Logs?
### 4. Logging
- [] If possible, did you make a video and did you send it/a link to it in the feature-videos channel?


