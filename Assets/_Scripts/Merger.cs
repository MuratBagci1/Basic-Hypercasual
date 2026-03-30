using System.Collections.Generic;
using System.Diagnostics;

public static class Merger
{
    static Merger()
    {
        ActionManager.OnMerge += Merge;
    }

    public static void Initialize()
    {
        Debug.WriteLine("Merger çalýþtý");
    }

    public static void Merge(List<Muzzle> blocks)
    {
        if (blocks == null || blocks.Count < 2)
        {
            return;
        }

        bool merged;
        do
        {

            merged = false;
            for (int i = 0; i < blocks.Count; i++)
            {
                Muzzle match = blocks.Find(x => x.Value == blocks[i].Value);
                if(match != blocks[i] && match.Value < 2048)
                {
                    int newValue = blocks[i].Value * 2;

                    blocks[i].ChangeValueAndText(newValue);
                    match.MuzzleReturnPool();

                    ActionManager.OnBlockRemoved?.Invoke(match);
                    ActionManager.OnMergeWobble?.Invoke();

                    //ActionManager.OnBlockValueChanged?.Invoke(blocks[i], newValue);

                    merged = true;
                    i--;
                }
            }

        } while (merged);
    }
}