// Decompiled with JetBrains decompiler
// Type: CrackMe.Form1
// Assembly: CrackMe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E929FEB7-F3DD-449F-99CD-83285EA760B5
// Assembly location: D:\Programming\Mentoring\11 - Debugging\CrackMe.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace CrackMe
{
    public class Form1 : Form
    {
        private string[] a;
        private IContainer eval_b;
        private Button eval_c;
        private TextBox eval_d;
        private Label eval_e;
        private Label eval_f;
        [NonSerialized]
        string eval_g;

        public Form1()
        {
            this.eval_a();
        }

        private void eval_a(object A_0, EventArgs A_1)
        {
            int num1 = 0;
            int num2;
            while (true)
            {
                switch (num1)
                {
                    case 0:
                    label_1:
                        break;
                    case 1:
                        goto label_10;
                    case 2:
                        num2 = 1;
                        if (num2 == 0)
                            ;
                        num2 = 0;
                        num2 = -26500;
                        int num3 = num2;
                        num2 = -26500;
                        int num4 = num2;
                        switch (num3 == num4 ? 1 : 0)
                        {
                            case 0:
                            case 2:
                                goto label_1;
                            default:
                                goto label_7;
                        }
                }
                if (string.IsNullOrEmpty(this.eval_d.Text))
                {
                    num2 = 2;
                    num1 = num2;
                }
                else
                {
                    this.a = this.eval_d.Text.Split('-');
                    num2 = 1;
                    num1 = num2;
                }
            }
        label_7:
            num2 = 0;
            if (num2 == 0)
                ;
            this.eval_f.Text = "Key cannot be empty";
            return;
        label_10:
            this.eval_f.Text = this.eval_a(this.a) ? "Correct key" : "Wrong key";
            this.eval_f.Visible = true;
        }

        private bool eval_a(string[] A_0)
        {
            byte[] evalAa;
            int num1;
            // ISSUE: variable of a compiler-generated type
            NetworkInterface networkInterface;
            switch (0)
            {
                case 0:
                label_2:
                    // ISSUE: object of a compiler-generated type is created
                    //evalA = new Form1.eval_a();
                    networkInterface = Enumerable.FirstOrDefault<NetworkInterface>((IEnumerable<NetworkInterface>)NetworkInterface.GetAllNetworkInterfaces());
                    num1 = 0;
                    goto default;
                default:
                    while (true)
                    {
                        switch (num1)
                        {
                            case 0:
                                switch (-22053 == -22053 ? 1 : 0)
                                {
                                    case 0:
                                    case 2:
                                        goto label_8;
                                    default:
                                        if (1 == 0)
                                            ;
                                        if (0 == 0)
                                            ;
                                        num1 = networkInterface != null ? 1 : 2;
                                        continue;
                                }
                            case 1:
                                goto label_7;
                            case 2:
                            label_8:
                                num1 = 3;
                                continue;
                            case 3:
                                goto label_9;
                            default:
                                goto label_2;
                        }
                    }
                label_7:
                    byte[] addressBytes = networkInterface.GetPhysicalAddress().GetAddressBytes();
                    // ISSUE: reference to a compiler-generated field
                    evalAa = BitConverter.GetBytes(DateTime.Now.Date.ToBinary());
                    // ISSUE: reference to a compiler-generated method
                    //Func<byte, int, int> selector1 = new Func<byte, int, int>(evalA.eval_a);
                    int[] numArray = Enumerable.ToArray<int>(Enumerable.Select<int, int>(Enumerable.Select<byte, int>((IEnumerable<byte>)addressBytes, x => x), (Func<int, int>)(A_0_2 =>
                    {
                        if (A_0_2 <= 999)
                        {
                            int num2 = 17361;
                            int num3 = num2;
                            num2 = 17361;
                            int num4 = num2;
                            switch (num3 == num4 ? 1 : 0)
                            {
                                case 0:
                                case 2:
                                    break;
                                default:
                                    num2 = 0;
                                    if (num2 == 0)
                                        ;
                                    num2 = 0;
                                    num2 = 1;
                                    if (num2 == 0)
                                        ;
                                    return A_0_2 * 10;
                            }
                        }
                        return A_0_2;
                    })));
                    // ISSUE: reference to a compiler-generated field
                    //evalA.b = Enumerable.ToArray<int>(Enumerable.Select<string, int>((IEnumerable<string>)A_0_1, new Func<string, int>(int.Parse)));
                    // ISSUE: reference to a compiler-generated method
                    //Func<int, int, int> selector2 = new Func<int, int, int>(evalA.eval_a);
                    return Enumerable.All<int>(Enumerable.Select<int, int>((IEnumerable<int>)numArray, x => x), A_0_2 =>
                    {
                        int num2 = 862;
                        int num3 = num2;
                        num2 = 862;
                        int num4 = num2;
                        switch (num3 == num4)
                        {
                            case true:
                                int num5 = 1;
                                if (num5 == 0)
                                    ;
                                num5 = 0;
                                if (num5 == 0)
                                    ;
                                num5 = 0;
                                return A_0_2 == 0;
                            default:
                                throw new Exception();
                        }
                    });
                label_9:
                    return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            int num1 = 2;
            while (true)
            {
                int num2 = 0;
                switch (num1)
                {
                    case 0:
                        num2 = 4;
                        num1 = num2;
                        continue;
                    case 1:
                        goto label_12;
                    case 2:
                        switch (0)
                        {
                            case 0:
                                goto label_3;
                            default:
                                continue;
                        }
                    case 3:
                    label_7:
                        this.eval_b.Dispose();
                        num2 = 1;
                        num1 = num2;
                        continue;
                    case 4:
                        num2 = 1;
                        if (num2 == 0)
                            ;
                        if (this.eval_b != null)
                        {
                            num2 = 3;
                            num1 = num2;
                            continue;
                        }
                        goto label_12;
                    default:
                    label_3:
                        if (disposing)
                        {
                            num2 = 13490;
                            int num3 = num2;
                            num2 = 13490;
                            int num4 = num2;
                            switch (num3 == num4 ? 1 : 0)
                            {
                                case 0:
                                case 2:
                                    goto label_7;
                                default:
                                    num2 = 0;
                                    if (num2 == 0)
                                        ;
                                    num2 = 0;
                                    num1 = num2;
                                    continue;
                            }
                        }
                        else
                            goto label_12;
                }
            }
        label_12:
            base.Dispose(disposing);
        }

        private void eval_a()
        {
            int num1 = 15225;
            int num2 = num1;
            num1 = 15225;
            int num3 = num1;
            switch (num2 == num3)
            {
                case true:
                    int num4 = 0;
                    if (num4 == 0)
                        ;
                    num4 = 1;
                    if (num4 == 0)
                        ;
                    num4 = 0;
                    this.eval_c = new Button();
                    this.eval_d = new TextBox();
                    this.eval_e = new Label();
                    this.eval_f = new Label();
                    this.SuspendLayout();
                    this.eval_c.Location = new Point(268, 51);
                    this.eval_c.Name = "bt_check";
                    this.eval_c.Size = new Size(75, 23);
                    this.eval_c.TabIndex = 0;
                    this.eval_c.Text = "Check";
                    this.eval_c.UseVisualStyleBackColor = true;
                    this.eval_c.Click += new EventHandler(this.eval_a);
                    this.eval_d.Location = new Point(35, 25);
                    this.eval_d.Name = "tb_key";
                    this.eval_d.Size = new Size(308, 20);
                    this.eval_d.TabIndex = 1;
                    this.eval_e.AutoSize = true;
                    this.eval_e.Location = new Point(32, 9);
                    this.eval_e.Name = "label1";
                    this.eval_e.Size = new Size(107, 13);
                    this.eval_e.TabIndex = 2;
                    this.eval_e.Text = "Please, enter the key";
                    this.eval_f.AutoSize = true;
                    this.eval_f.Location = new Point(35, 52);
                    this.eval_f.Name = "lb_status";
                    this.eval_f.Size = new Size(35, 13);
                    this.eval_f.TabIndex = 3;
                    this.eval_f.Text = "label2";
                    this.eval_f.Visible = false;
                    this.AutoScaleDimensions = new SizeF(6f, 13f);
                    this.AutoScaleMode = AutoScaleMode.Font;
                    this.ClientSize = new Size(369, 86);
                    this.Controls.Add((Control)this.eval_f);
                    this.Controls.Add((Control)this.eval_e);
                    this.Controls.Add((Control)this.eval_d);
                    this.Controls.Add((Control)this.eval_c);
                    this.Name = "Form1";
                    this.Text = "Crack me";
                    this.ResumeLayout(false);
                    this.PerformLayout();
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
