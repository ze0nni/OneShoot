using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelsPanel : MonoBehaviour
{
   public TextMeshProUGUI CurrentLevel;
   public TextMeshProUGUI NextLevel;
   
   [Space]
   public LevelsPanelItem ItemPrefab;
   
   private int _maxLevels;
   private List<LevelsPanelItem> _items = new List<LevelsPanelItem>();
   
   public void SetMaxLevels(int maxLevels)
   {
      _maxLevels = maxLevels;
      
      ItemPrefab.gameObject.SetActive(false);

      while (_items.Count > maxLevels)
      {
         GameObject.Destroy(_items[0].gameObject);
         _items.RemoveAt(0);
      }

      while (_items.Count < maxLevels)
      {
         var item = GameObject.Instantiate(ItemPrefab, ItemPrefab.transform.parent);
         item.gameObject.SetActive(true);
         _items.Add(item);
      }
   }

   public void SetCurrentLevel(int level)
   {
      level = Math.Max(level, _maxLevels);
      
      CurrentLevel.text = level.ToString();
      NextLevel.text = level < _maxLevels ? (level + 1).ToString() : "Fin";

      for (var i = 0; i < _items.Count; i++)
      {
         var item = _items[i];
         if (i + 1 == level)
         {
            item.Face.color = item.CurrentColor;
         } else if (i + 1 < level)
         {
            item.Face.color = item.CompletedColor;
         }
         else
         {
            item.Face.color = item.NextColor;
         }
      }
   }
}
