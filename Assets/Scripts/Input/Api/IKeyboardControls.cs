using UnityEngine;

namespace Assets.Scripts.Input.Api
{
    public interface IKeyboardControls
    {
        KeyCode QuickSlot1 { get; }

        KeyCode QuickSlot2 { get; }

        KeyCode QuickSlot3 { get; }
        
        KeyCode QuickSlot4 { get; }
        
        KeyCode QuickSlot5 { get; }
        
        KeyCode QuickSlot6 { get; }
        
        KeyCode QuickSlot7 { get; }
        
        KeyCode QuickSlot8 { get; }
        
        KeyCode QuickSlot9 { get; }
        
        KeyCode QuickSlot10 { get; }

        KeyCode MoveLeft { get; }

        KeyCode MoveRight { get; }

        KeyCode MoveUp { get; }

        KeyCode MoveDown { get; }

        KeyCode Interact { get; }

        KeyCode ToggleCrafting { get; }

        KeyCode ToggleInventory { get; }

        KeyCode ToggleCharacterSheet { get; }

        KeyCode GuiClose { get; }
    }
}