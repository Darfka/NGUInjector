﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NGUInjector.AllocationProfiles;

namespace NGUInjector
{
    public partial class SettingsForm : Form
    {
        internal static readonly Dictionary<int, string> TitanList = new Dictionary<int, string>();
        internal static readonly Dictionary<int, string> ZoneList = new Dictionary<int, string>();
        internal static readonly Dictionary<int, string> CombatModeList = new Dictionary<int, string>();
        internal static readonly Dictionary<int, string> CubePriorityList = new Dictionary<int, string>();
        private bool _initializing = true;
        public SettingsForm()
        {
            InitializeComponent();

            // Populate our data sources
            TitanList.Add(0, "None");
            TitanList.Add(1, "GRB");
            TitanList.Add(2, "GCT");
            TitanList.Add(3, "Jake");
            TitanList.Add(4, "UUG");
            TitanList.Add(5, "Walderp");
            TitanList.Add(6, "Beast");
            TitanList.Add(7, "Greasy Nerd");
            TitanList.Add(8, "Godmother");
            TitanList.Add(9, "Exile");
            TitanList.Add(10, "IT HUNGERS");
            TitanList.Add(11, "Rock Lobster");
            TitanList.Add(12, "Amalgamate");

            ZoneList.Add(-1, "Safe Zone: Awakening Site");
            ZoneList.Add(0, "Tutorial Zone");
            ZoneList.Add(1, "Sewers");
            ZoneList.Add(2, "Forest");
            ZoneList.Add(3, "Cave of Many Things");
            ZoneList.Add(4, "The Sky");
            ZoneList.Add(5, "High Security Base");
            ZoneList.Add(6, "Gordon Ramsay Bolton");
            ZoneList.Add(7, "Clock Dimension");
            ZoneList.Add(8, "Grand Corrupted Tree");
            ZoneList.Add(9, "The 2D Universe");
            ZoneList.Add(10, "Ancient Battlefield");
            ZoneList.Add(11, "Jake From Accounting");
            ZoneList.Add(12, "A Very Strange Place");
            ZoneList.Add(13, "Mega Lands");
            ZoneList.Add(14, "UUG THE UNMENTIONABLE");
            ZoneList.Add(15, "The Beardverse");
            ZoneList.Add(16, "WALDERP");
            ZoneList.Add(17, "Badly Drawn World");
            ZoneList.Add(18, "Boring-Ass Earth");
            ZoneList.Add(19, "THE BEAST");
            ZoneList.Add(20, "Chocolate World");
            ZoneList.Add(21, "The Evilverse");
            ZoneList.Add(22, "Pretty Pink Princess Land");
            ZoneList.Add(23, "GREASY NERD");
            ZoneList.Add(24, "Meta Land");
            ZoneList.Add(25, "Interdimensional Party");
            ZoneList.Add(26, "THE GODMOTHER");
            ZoneList.Add(27, "Typo Zonw");
            ZoneList.Add(28, "The Fad-Lands");
            ZoneList.Add(29, "JRPGVille");
            ZoneList.Add(30, "THE EXILE");
            ZoneList.Add(31, "The Rad-lands");
            ZoneList.Add(32, "Back To School");
            ZoneList.Add(33, "The West World");
            ZoneList.Add(34, "IT HUNGERS");
            ZoneList.Add(35, "The Breadverse");
            ZoneList.Add(36, "That 70's Zone");
            ZoneList.Add(37, "The Halloweenies");
            ZoneList.Add(38, "ROCK LOBSTER");
            ZoneList.Add(39, "Construction Zone");
            ZoneList.Add(40, "DUCK DUCK ZONE");
            ZoneList.Add(41, "The Nether Regions");
            ZoneList.Add(42, "AMALGAMATE");
            ZoneList.Add(1000, "ITOPOD");

            CombatModeList.Add(0, "Manual");
            CombatModeList.Add(1, "Idle");

            CubePriorityList.Add(0, "None");
            CubePriorityList.Add(1, "Balanced");
            CubePriorityList.Add(2, "Power");
            CubePriorityList.Add(3, "Toughness");

            CubePriority.DataSource = new BindingSource(CubePriorityList, null);
            CubePriority.ValueMember = "Key";
            CubePriority.DisplayMember = "Value";

            HighestTitanDropdown.DataSource = new BindingSource(TitanList, null);
            HighestTitanDropdown.ValueMember = "Key";
            HighestTitanDropdown.DisplayMember = "Value";

            CombatMode.DataSource = new BindingSource(CombatModeList, null);
            CombatMode.ValueMember = "Key";
            CombatMode.DisplayMember = "Value";

            QuestCombatMode.DataSource = new BindingSource(CombatModeList, null);
            QuestCombatMode.ValueMember = "Key";
            QuestCombatMode.DisplayMember = "Value";

            CombatTargetZone.DataSource = new BindingSource(ZoneList, null);
            CombatTargetZone.ValueMember = "Key";
            CombatTargetZone.DisplayMember = "Value";

            //Remove ITOPOD for non combat zones
            ZoneList.Remove(1000);

            GoldLoadoutZone.DataSource = new BindingSource(ZoneList, null);
            GoldLoadoutZone.ValueMember = "Key";
            GoldLoadoutZone.DisplayMember = "Value";

            InitialGoldTarget.DataSource = new BindingSource(ZoneList, null);
            InitialGoldTarget.ValueMember = "Key";
            InitialGoldTarget.DisplayMember = "Value";

            blacklistLabel.Text = "";
            yggItemLabel.Text = "";
            priorityBoostLabel.Text = "";
            titanLabel.Text = "";
            GoldItemLabel.Text = "";

            priorityBoostItemAdd.TextChanged += priorityBoostItemAdd_TextChanged;
            blacklistAddItem.TextChanged += blacklistAddItem_TextChanged;
            yggLoadoutItem.TextChanged += yggLoadoutItem_TextChanged;
            titanAddItem.TextChanged += titanAddItem_TextChanged;
            GoldItemBox.TextChanged += GoldItemBox_TextChanged;
            WishAddInput.TextChanged += WishAddInput_TextChanged;

            prioUpButton.Text = char.ConvertFromUtf32(8593);
            prioDownButton.Text = char.ConvertFromUtf32(8595);

            WishUpButton.Text = char.ConvertFromUtf32(8593);
            WishDownButton.Text = char.ConvertFromUtf32(8595);

            VersionLabel.Text = $"Version: {Main.Version}";
        }

        internal void SetSnipeZone(ComboBox control, int setting)
        {
            control.SelectedIndex = setting >= 1000 ? 44 : setting + 1;
        }

        internal void SetOtherZone(ComboBox control, int setting)
        {
            control.SelectedIndex = setting + 1;
        }
        
        internal void UpdateFromSettings(SavedSettings newSettings)
        {
            _initializing = true;
            AutoDailySpin.Checked = newSettings.AutoSpin;
            AutoFightBosses.Checked = newSettings.AutoFight;
            AutoITOPOD.Checked = newSettings.AutoQuestITOPOD;
            AutoMoneyPit.Checked = newSettings.AutoMoneyPit;
            MoneyPitThreshold.Text = $"{newSettings.MoneyPitThreshold:#.##E+00}"; 
            ManageEnergy.Checked = newSettings.ManageEnergy;
            ManageMagic.Checked = newSettings.ManageMagic;
            ManageGear.Checked = newSettings.ManageGear;
            ManageDiggers.Checked = newSettings.ManageDiggers;
            ManageWandoos.Checked = newSettings.ManageWandoos;
            AutoRebirth.Checked = newSettings.AutoRebirth;
            ManageYggdrasil.Checked = newSettings.ManageYggdrasil;
            YggdrasilSwap.Checked = newSettings.SwapYggdrasilLoadouts;
            ManageInventory.Checked = newSettings.ManageInventory;
            ManageBoostConvert.Checked = newSettings.AutoConvertBoosts;
            SwapTitanLoadout.Checked = newSettings.SwapTitanLoadouts;
            HighestTitanDropdown.SelectedIndex = newSettings.HighestAKZone;
            BossesOnly.Checked = newSettings.SnipeBossOnly;
            PrecastBuffs.Checked = newSettings.PrecastBuffs;
            RecoverHealth.Checked = newSettings.RecoverHealth;
            FastCombat.Checked = newSettings.FastCombat;
            CombatMode.SelectedIndex = newSettings.CombatMode;
            SetSnipeZone(CombatTargetZone, newSettings.SnipeZone);
            AllowFallthrough.Checked = newSettings.AllowZoneFallback;
            SetOtherZone(GoldLoadoutZone, newSettings.GoldZone);
            SetOtherZone(InitialGoldTarget, newSettings.InitialGoldZone);
            QuestCombatMode.SelectedIndex = newSettings.QuestCombatMode;
            ManageQuests.Checked = newSettings.AutoQuest;
            AllowMajor.Checked = newSettings.AllowMajorQuests;
            AbandonMinors.Checked = newSettings.AbandonMinors;
            AbandonMinorThreshold.Value = newSettings.MinorAbandonThreshold;
            QuestFastCombat.Checked = newSettings.QuestFastCombat;
            UseGoldLoadout.Checked = newSettings.NextGoldSwap;
            AutoSpellSwap.Checked = newSettings.AutoSpellSwap;
            SpaghettiCap.Value = newSettings.SpaghettiThreshold;
            CounterfeitCap.Value = newSettings.CounterfeitThreshold;
            AutoBuyEM.Checked = newSettings.AutoBuyEM;
            MasterEnable.Checked = newSettings.GlobalEnabled;
            CombatActive.Checked = newSettings.CombatEnabled;
            ManualMinor.Checked = newSettings.ManualMinors;
            ButterMajors.Checked = newSettings.UseButterMajor;
            ManageR3.Checked = newSettings.ManageR3;
            ButterMinors.Checked = newSettings.UseButterMinor;
            ActivateFruits.Checked = newSettings.ActivateFruits;
            BeastMode.Checked = newSettings.BeastMode;
            CubePriority.SelectedIndex = newSettings.CubePriority;
            BloodNumberThreshold.Text = $"{newSettings.BloodNumberThreshold:#.##E+00}";

            
            var temp = newSettings.YggdrasilLoadout.ToDictionary(x => x, x => Main.Character.itemInfo.itemName[x]);
            if (temp.Count > 0)
            {
                yggdrasilLoadoutBox.DataSource = null;
                yggdrasilLoadoutBox.DataSource = new BindingSource(temp, null);
                yggdrasilLoadoutBox.ValueMember = "Key";
                yggdrasilLoadoutBox.DisplayMember = "Value";
            }
            else
            {
                yggdrasilLoadoutBox.Items.Clear();
            }
            

            temp = newSettings.PriorityBoosts.ToDictionary(x => x, x => Main.Character.itemInfo.itemName[x]);
            if (temp.Count > 0)
            {
                priorityBoostBox.DataSource = null;
                priorityBoostBox.DataSource = new BindingSource(temp, null);
                priorityBoostBox.ValueMember = "Key";
                priorityBoostBox.DisplayMember = "Value";
            }
            else
            {
                priorityBoostBox.Items.Clear();
            }
            
            temp = newSettings.BoostBlacklist.ToDictionary(x => x, x => Main.Character.itemInfo.itemName[x]);
            if (temp.Count > 0)
            {
                blacklistBox.DataSource = null;
                blacklistBox.DataSource = new BindingSource(temp, null);
                blacklistBox.ValueMember = "Key";
                blacklistBox.DisplayMember = "Value";
            }
            else
            {
                blacklistBox.Items.Clear();
            }
            

            
            temp = newSettings.TitanLoadout.ToDictionary(x => x, x => Main.Character.itemInfo.itemName[x]);
            if (temp.Count > 0)
            {
                titanLoadout.DataSource = null;
                titanLoadout.DataSource = new BindingSource(temp, null);
                titanLoadout.ValueMember = "Key";
                titanLoadout.DisplayMember = "Value";
            }
            else
            {
                titanLoadout.Items.Clear();
            }
            

            
            temp = newSettings.GoldDropLoadout.ToDictionary(x => x, x => Main.Character.itemInfo.itemName[x]);
            if (temp.Count > 0)
            {
                GoldLoadout.DataSource = null;
                GoldLoadout.DataSource = new BindingSource(temp, null);
                GoldLoadout.ValueMember = "Key";
                GoldLoadout.DisplayMember = "Value";
            }
            else
            {
                GoldLoadout.Items.Clear();
            }
            
            
            temp = newSettings.WishPriorities.ToDictionary(x => x, x => Main.Character.wishesController.properties[x].wishName);
            if (temp.Count > 0)
            {
                WishPriority.DataSource = null;
                WishPriority.DataSource = new BindingSource(temp, null);
                WishPriority.ValueMember = "Key";
                WishPriority.DisplayMember = "Value";
            }
            else
            {
                WishPriority.Items.Clear();
            }
            
            Refresh();
            _initializing = false;
        }

        internal void UpdateProgressBar(int progress)
        {
            if (progress < 0)
                return;
            progressBar1.Value = progress;
        }

        internal void UpdateGoldLoadout(bool active)
        {
            _initializing = true;
            UseGoldLoadout.Checked = active;
            Refresh();
            _initializing = false;
        }

        internal void UpdateActive(bool active)
        {
            _initializing = true;
            MasterEnable.Checked = active;
            Refresh();
            _initializing = false;
        }

        internal void UpdateITOPOD(bool active)
        {
            _initializing = true;
            AutoITOPOD.Checked = active;
            Refresh();
            _initializing = false;
        }

        private void MasterEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.GlobalEnabled = MasterEnable.Checked;
        }

        private void AutoDailySpin_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoSpin = AutoDailySpin.Checked;
        }

        private void AutoMoneyPit_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoMoneyPit = AutoMoneyPit.Checked;
        }

        private void AutoITOPOD_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoQuestITOPOD = AutoITOPOD.Checked;
        }

        private void AutoFightBosses_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoFight = AutoFightBosses.Checked;
        }

        private void MoneyPitThresholdSave_Click(object sender, EventArgs e)
        {
            var newVal = MoneyPitThreshold.Text;
            if (double.TryParse(newVal, out var saved))
            {
                if (saved < 0)
                {
                    moneyPitError.SetError(MoneyPitThreshold, "Not a valid value");
                    return;
                }
                Main.Settings.MoneyPitThreshold = saved;
            }
            else
            {
                moneyPitError.SetError(MoneyPitThreshold, "Not a valid value");
            }
        }

        private void MoneyPitThreshold_TextChanged_1(object sender, EventArgs e)
        {
            moneyPitError.SetError(MoneyPitThreshold, "");
        }

        private void ManageEnergy_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageEnergy = ManageEnergy.Checked;
        }

        private void ManageMagic_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageMagic = ManageMagic.Checked;
        }

        private void ManageGear_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageGear = ManageGear.Checked;
        }

        private void ManageDiggers_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageDiggers = ManageDiggers.Checked;
        }

        private void ManageWandoos_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageWandoos = ManageWandoos.Checked;
        }

        private void AutoRebirth_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoRebirth = AutoRebirth.Checked;
        }

        private void ManageYggdrasil_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageYggdrasil = ManageYggdrasil.Checked;
        }

        private void YggdrasilSwap_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.SwapYggdrasilLoadouts = YggdrasilSwap.Checked;
        }

        private void yggLoadoutItem_TextChanged(object sender, EventArgs e)
        {
            yggErrorProvider.SetError(yggLoadoutItem, "");
            var val = decimal.ToInt32(yggLoadoutItem.Value);
            if (val < 40 || val > 505)
                return;
            var itemName = Main.Character.itemInfo.itemName[val];
            yggItemLabel.Text = itemName;
        }

        private void yggAddButton_Click(object sender, EventArgs e)
        {
            yggErrorProvider.SetError(yggLoadoutItem, "");
            var val = decimal.ToInt32(yggLoadoutItem.Value);
            if (val < 40 || val > 505)
            {
                yggErrorProvider.SetError(yggLoadoutItem, "Not a valid item id");
                return;
            }

            if (Main.Settings.YggdrasilLoadout.Contains(val))
                return;
            var temp = Main.Settings.YggdrasilLoadout.ToList();
            temp.Add(val);
            Main.Settings.YggdrasilLoadout = temp.ToArray();
        }

        private void yggRemoveButton_Click(object sender, EventArgs e)
        {
            yggErrorProvider.SetError(yggLoadoutItem, "");
            var item= yggdrasilLoadoutBox.SelectedItem;
            if (item == null)
                return;

            var id = (KeyValuePair<int, string>)item;

            var temp = Main.Settings.YggdrasilLoadout.ToList();
            temp.RemoveAll(x => x == id.Key);
            Main.Settings.YggdrasilLoadout = temp.ToArray();
        }

        private void ManageInventory_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageInventory = ManageInventory.Checked;
        }

        private void ManageBoostConvert_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoConvertBoosts = ManageBoostConvert.Checked;
        }

        private void priorityBoostAdd_Click(object sender, EventArgs e)
        {
            invPrioErrorProvider.SetError(priorityBoostItemAdd, "");
            var val = decimal.ToInt32(priorityBoostItemAdd.Value);
            if (val < 40 || val > 505)
            {
                invPrioErrorProvider.SetError(priorityBoostItemAdd, "Not a valid item id");
                return;
            }

            if (Main.Settings.PriorityBoosts.Contains(val)) return;
            var temp = Main.Settings.PriorityBoosts.ToList();
            temp.Add(val);
            Main.Settings.PriorityBoosts = temp.ToArray();
        }

        private void priorityBoostRemove_Click(object sender, EventArgs e)
        {
            invPrioErrorProvider.SetError(priorityBoostItemAdd, "");
            var item = priorityBoostBox.SelectedItem;
            if (item == null)
                return;

            var id = (KeyValuePair<int, string>)item;

            var temp = Main.Settings.PriorityBoosts.ToList();
            temp.RemoveAll(x => x == id.Key);
            Main.Settings.PriorityBoosts = temp.ToArray();
        }

        private void prioUpButton_Click(object sender, EventArgs e)
        {
            invPrioErrorProvider.SetError(priorityBoostItemAdd, "");
            var index = priorityBoostBox.SelectedIndex;
            if (index == -1 || index == 0)
                return;

            var temp = Main.Settings.PriorityBoosts.ToList();
            var item = temp[index];
            temp.RemoveAt(index);
            temp.Insert(index - 1, item);
            Main.Settings.PriorityBoosts = temp.ToArray();
            priorityBoostBox.SelectedIndex = index - 1;
        }

        private void prioDownButton_Click(object sender, EventArgs e)
        {
            invPrioErrorProvider.SetError(priorityBoostItemAdd, "");
            var index = priorityBoostBox.SelectedIndex;
            if (index == -1)
                return;

            var temp = Main.Settings.PriorityBoosts.ToList();
            if (index == temp.Count - 1)
                return;

            var item = temp[index];
            temp.RemoveAt(index);
            temp.Insert(index + 1, item);
            Main.Settings.PriorityBoosts = temp.ToArray();
            priorityBoostBox.SelectedIndex = index + 1;
        }

        private void blacklistAdd_Click(object sender, EventArgs e)
        {
            invBlacklistErrProvider.SetError(blacklistAddItem, "");
            var val = decimal.ToInt32(blacklistAddItem.Value);
            if (val < 40 || val > 505)
            {
                invBlacklistErrProvider.SetError(blacklistAddItem, "Not a valid item id");
                return;
            }

            if (Main.Settings.BoostBlacklist.Contains(val)) return;
            var temp = Main.Settings.BoostBlacklist.ToList();
            temp.Add(val);
            Main.Settings.BoostBlacklist = temp.ToArray();
        }

        private void blacklistRemove_Click(object sender, EventArgs e)
        {
            invBlacklistErrProvider.SetError(blacklistAddItem, "");
            var item = blacklistBox.SelectedItem;
            if (item == null)
                return;

            var id = (KeyValuePair<int, string>)item;

            var temp = Main.Settings.BoostBlacklist.ToList();
            temp.RemoveAll(x => x == id.Key);
            Main.Settings.BoostBlacklist = temp.ToArray();
        }

        private void SwapTitanLoadout_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.SwapTitanLoadouts = SwapTitanLoadout.Checked;
        }

        private void HighestTitanDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            var selected = HighestTitanDropdown.SelectedIndex;
            Main.Settings.HighestAKZone = selected;
        }

        private void titanAddItem_TextChanged(object sender, EventArgs e)
        {
            titanErrProvider.SetError(titanAddItem, "");
            var val = decimal.ToInt32(titanAddItem.Value);
            if (val < 40 || val > 505)
                return;
            var itemName = Main.Character.itemInfo.itemName[val];
            titanLabel.Text = itemName;
        }

        private void titanAdd_Click(object sender, EventArgs e)
        {
            titanErrProvider.SetError(titanAddItem, "");
            var val = decimal.ToInt32(titanAddItem.Value);
            if (val < 40 || val > 505)
            {
                invBlacklistErrProvider.SetError(titanAddItem, "Not a valid item id");
                return;
            }

            if (Main.Settings.TitanLoadout.Contains(val)) return;
            var temp = Main.Settings.TitanLoadout.ToList();
            temp.Add(val);
            Main.Settings.TitanLoadout = temp.ToArray();
        }

        private void titanRemove_Click(object sender, EventArgs e)
        {
            titanErrProvider.SetError(titanAddItem, "");
            var selectedItem = titanLoadout.SelectedItem;
            if (selectedItem == null)
                return;

            var item = (KeyValuePair<int, string>)selectedItem;

            var temp = Main.Settings.TitanLoadout.ToList();
            temp.RemoveAll(x => x == item.Key);
            Main.Settings.TitanLoadout = temp.ToArray();
        }

        private void CombatActive_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.CombatEnabled = CombatActive.Checked;
        }

        private void BossesOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.SnipeBossOnly = BossesOnly.Checked;
        }

        private void PrecastBuffs_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.PrecastBuffs = PrecastBuffs.Checked;
        }

        private void RecoverHealth_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.RecoverHealth = RecoverHealth.Checked;
        }

        private void FastCombat_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.FastCombat = FastCombat.Checked;
        }

        private void CombatMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            var selected = CombatMode.SelectedIndex;
            Main.Settings.CombatMode = selected;
        }

        private void CombatTargetZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            var selected = CombatTargetZone.SelectedItem;
            var item = (KeyValuePair<int, string>) selected;
            Main.Settings.SnipeZone = item.Key;
        }

        private void AllowFallthrough_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AllowZoneFallback = AllowFallthrough.Checked;
        }

        private void GoldLoadoutZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            var selected = GoldLoadoutZone.SelectedItem;
            var item = (KeyValuePair<int, string>)selected;
            Main.Settings.GoldZone = item.Key;
        }

        private void UseGoldLoadout_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.NextGoldSwap = UseGoldLoadout.Checked;
        }

        private void InitialGoldTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            var selected = InitialGoldTarget.SelectedItem;
            var item = (KeyValuePair<int, string>)selected;
            Main.Settings.InitialGoldZone = item.Key;
        }

        private void GoldItemBox_TextChanged(object sender, EventArgs e)
        {
            goldErrorProvider.SetError(GoldItemBox, "");
            var val = decimal.ToInt32(GoldItemBox.Value);
            if (val < 40 || val > 505)
                return;
            var itemName = Main.Character.itemInfo.itemName[val];
            GoldItemLabel.Text = itemName;
        }

        private void GoldLoadoutAdd_Click(object sender, EventArgs e)
        {
            goldErrorProvider.SetError(GoldItemBox, "");
            var val = decimal.ToInt32(GoldItemBox.Value);
            if (val < 40 || val > 505)
            {
                goldErrorProvider.SetError(GoldItemBox, "Invalid item id");
                return;
            }

            if (Main.Settings.GoldDropLoadout.Contains(val)) return;
            var temp = Main.Settings.GoldDropLoadout.ToList();
            temp.Add(val);
            Main.Settings.GoldDropLoadout = temp.ToArray();
        }

        private void GoldLoadoutRemove_Click(object sender, EventArgs e)
        {
            goldErrorProvider.SetError(GoldItemBox, "");
            var selected = GoldLoadout.SelectedItem;
            if (selected == null)
                return;

            var id = (KeyValuePair<int, string>)selected;

            var temp = Main.Settings.GoldDropLoadout.ToList();
            temp.RemoveAll(x => x == id.Key);
            Main.Settings.GoldDropLoadout = temp.ToArray();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void ManageQuests_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoQuest = ManageQuests.Checked;
        }

        private void AllowMajor_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AllowMajorQuests = AllowMajor.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AbandonMinors = AbandonMinors.Checked;
        }

        private void AbandonMinorThreshold_ValueChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.MinorAbandonThreshold = decimal.ToInt32(AbandonMinorThreshold.Value);
        }

        private void QuestFastCombat_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.QuestFastCombat = QuestFastCombat.Checked;
        }

        private void priorityBoostItemAdd_TextChanged(object sender, EventArgs e)
        {
            invPrioErrorProvider.SetError(priorityBoostItemAdd, "");
            var val = decimal.ToInt32(priorityBoostItemAdd.Value);
            if (val < 40 || val > 505)
                return;
            var itemName = Main.Character.itemInfo.itemName[val];
            priorityBoostLabel.Text = itemName;
        }

        private void priorityBoostItemAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                invPrioErrorProvider.SetError(priorityBoostItemAdd, "");
                var val = decimal.ToInt32(priorityBoostItemAdd.Value);
                if (val < 40 || val > 505)
                {
                    invPrioErrorProvider.SetError(priorityBoostItemAdd, "Not a valid item id");
                    return;
                }

                ActiveControl = priorityBoostLabel;

                if (Main.Settings.PriorityBoosts.Contains(val))
                    return;
                var temp = Main.Settings.PriorityBoosts.ToList();
                temp.Add(val);
                Main.Settings.PriorityBoosts = temp.ToArray();
            }
        }

        private void blacklistAddItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                invBlacklistErrProvider.SetError(blacklistAddItem, "");
                var val = decimal.ToInt32(blacklistAddItem.Value);
                if (val < 40 || val > 505)
                {
                    invBlacklistErrProvider.SetError(blacklistAddItem, "Not a valid item id");
                    return;
                }

                ActiveControl = blacklistLabel;

                if (Main.Settings.BoostBlacklist.Contains(val))
                    return;
                var temp = Main.Settings.BoostBlacklist.ToList();
                temp.Add(val);
                Main.Settings.BoostBlacklist = temp.ToArray();
            }
        }

        private void blacklistAddItem_TextChanged(object sender, EventArgs e)
        {
            invBlacklistErrProvider.SetError(blacklistAddItem, "");
            var val = decimal.ToInt32(blacklistAddItem.Value);
            if (val < 40 || val > 505)
                return;
            var itemName = Main.Character.itemInfo.itemName[val];
            blacklistLabel.Text = itemName;
        }

        private void yggLoadoutItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                yggErrorProvider.SetError(yggLoadoutItem, "");
                var val = decimal.ToInt32(yggLoadoutItem.Value);
                if (val < 40 || val > 505)
                {
                    yggErrorProvider.SetError(yggLoadoutItem, "Not a valid item id");
                    return;
                }

                ActiveControl = yggItemLabel;

                if (Main.Settings.YggdrasilLoadout.Contains(val))
                    return;
                var temp = Main.Settings.YggdrasilLoadout.ToList();
                temp.Add(val);
                Main.Settings.YggdrasilLoadout = temp.ToArray();
            }
            
        }

        private void titanAddItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                titanErrProvider.SetError(titanAddItem, "");
                var val = decimal.ToInt32(titanAddItem.Value);
                if (val < 40 || val > 505)
                {
                    invBlacklistErrProvider.SetError(titanAddItem, "Not a valid item id");
                    return;
                }

                ActiveControl = titanLabel;

                if (Main.Settings.TitanLoadout.Contains(val))
                    return;
                var temp = Main.Settings.TitanLoadout.ToList();
                temp.Add(val);
                Main.Settings.TitanLoadout = temp.ToArray();
            }
        }

        private void GoldItemBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                goldErrorProvider.SetError(GoldItemBox, "");
                var val = decimal.ToInt32(GoldItemBox.Value);
                if (val < 40 || val > 505)
                {
                    goldErrorProvider.SetError(GoldItemBox, "Invalid item id");
                    return;
                }

                ActiveControl = GoldItemLabel;

                if (Main.Settings.GoldDropLoadout.Contains(val))
                    return;
                var temp = Main.Settings.GoldDropLoadout.ToList();
                temp.Add(val);
                Main.Settings.GoldDropLoadout = temp.ToArray();
            }
        }

        private void AutoSpellSwap_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoSpellSwap = AutoSpellSwap.Checked;
        }

        private void SaveSpellCapButton_Click(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.SpaghettiThreshold = decimal.ToInt32(SpaghettiCap.Value);
            Main.Settings.CounterfeitThreshold = decimal.ToInt32(CounterfeitCap.Value);

            var newVal = BloodNumberThreshold.Text;
            if (double.TryParse(newVal, out var saved))
            {
                if (saved < 0)
                {
                    numberErrProvider.SetError(BloodNumberThreshold, "Not a valid value");
                    return;
                }
                Main.Settings.BloodNumberThreshold = saved;
            }
            else
            {
                numberErrProvider.SetError(BloodNumberThreshold, "Not a valid value");
            }
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            //var c = Main.Character;
            //for (var i = 0; i <= 13; i++)
            //{
            //    CustomAllocation.
            //}
        }

        private void AutoBuyEM_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoBuyEM = AutoBuyEM.Checked;
        }

        private void IdleMinor_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManualMinors = ManualMinor.Checked;
        }

        private void UseButter_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.UseButterMajor = ButterMajors.Checked;
        }

        private void ManageR3_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageR3 = ManageR3.Checked;
        }

        private void ButterMinors_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.UseButterMinor = ButterMinors.Checked;
        }

        private void ActivateFruits_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ActivateFruits = ActivateFruits.Checked;
        }

        private void WishUpButton_Click(object sender, EventArgs e)
        {
            wishErrorProvider.SetError(WishAddInput, "");
            var index = WishPriority.SelectedIndex;
            if (index == -1 || index == 0)
                return;

            var temp = Main.Settings.WishPriorities.ToList();
            var item = temp[index];
            temp.RemoveAt(index);
            temp.Insert(index -1, item);
            Main.Settings.WishPriorities = temp.ToArray();
            WishPriority.SelectedIndex = index - 1;

            Main.Character.removeAllRes3();
            Main.Character.removeMostEnergy();
            Main.Character.removeMostMagic();
        }

        private void WishDownButton_Click(object sender, EventArgs e)
        {
            wishErrorProvider.SetError(WishAddInput, "");
            var index = WishPriority.SelectedIndex;
            if (index == -1)
                return;

            var temp = Main.Settings.WishPriorities.ToList();

            if (index == temp.Count - 1)
                return;
            var item = temp[index];
            temp.RemoveAt(index);
            temp.Insert(index + 1, item);
            Main.Settings.WishPriorities = temp.ToArray();
            WishPriority.SelectedIndex = index + 1;
            Main.Character.removeAllRes3();
            Main.Character.removeMostEnergy();
            Main.Character.removeMostMagic();
        }

        private void AddWishButton_Click(object sender, EventArgs e)
        {
            wishErrorProvider.SetError(WishAddInput, "");
            var val = decimal.ToInt32(WishAddInput.Value);
            if (val < 0 || val > 224)
            {
                wishErrorProvider.SetError(WishAddInput, "Not a valid Wish ID");
                return;
            }

            if (Main.Settings.WishPriorities.Contains(val)) return;
            var temp = Main.Settings.WishPriorities.ToList();
            temp.Add(val);
            Main.Settings.WishPriorities = temp.ToArray();
            Main.Character.removeAllRes3();
            Main.Character.removeMostEnergy();
            Main.Character.removeMostMagic();
        }

        private void RemoveWishButton_Click(object sender, EventArgs e)
        {
            wishErrorProvider.SetError(WishAddInput, "");

            var item = WishPriority.SelectedItem;
            if (item == null)
                return;

            var id = (KeyValuePair<int, string>)item;

            var temp = Main.Settings.WishPriorities.ToList();
            temp.RemoveAll(x => x == id.Key);
            Main.Settings.WishPriorities = temp.ToArray();
            Main.Character.removeAllRes3();
            Main.Character.removeMostEnergy();
            Main.Character.removeMostMagic();
        }

        private void WishAddInput_TextChanged(object sender, EventArgs e)
        {
            wishErrorProvider.SetError(WishAddInput, "");
            var val = decimal.ToInt32(WishAddInput.Value);
            if (val < 0 || val > 224)
                return;
            var wishName = Main.Character.wishesController.properties[val].wishName;
            AddWishLabel.Text = wishName;
        }

        private void WishAddInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                wishErrorProvider.SetError(WishAddInput, "");
                var val = decimal.ToInt32(WishAddInput.Value);
                if (val < 0 || val > 224)
                {
                    wishErrorProvider.SetError(WishAddInput, "Not a valid Wish ID");
                    return;
                }

                if (Main.Settings.WishPriorities.Contains(val)) return;
                var temp = Main.Settings.WishPriorities.ToList();
                temp.Add(val);
                Main.Settings.WishPriorities = temp.ToArray();
            }
        }

        private void BeastMode_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.BeastMode = BeastMode.Checked;
        }

        private void CubePriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.CubePriority = CubePriority.SelectedIndex;
        }

		private void LoopMinor_CheckedChanged(object sender, EventArgs e)
		{
            if (_initializing) return;
            Main.Settings.LoopMinorForMinimumDrop = LoopMinor.Checked;
        }
	}
}
