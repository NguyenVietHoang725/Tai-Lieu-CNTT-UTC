using Bai4.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai4.Services
{
    internal class BaiTapDienTuService
    {
        public static List<BaiTapDienTu> LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Không tìm thấy file bài tập: " + filePath);

            var lines = File.ReadAllLines(filePath);
            var list = new List<BaiTapDienTu>();

            bool readingDeBai = false;
            bool readingDapAn = false;
            string currentDeBai = "";
            var currentDapAnLines = new List<string>();

            foreach (var raw in lines)
            {
                var line = raw.Trim();

                if (line.Equals("#BEGIN", StringComparison.OrdinalIgnoreCase))
                {
                    currentDeBai = "";
                    currentDapAnLines.Clear();
                    continue;
                }

                if (line.StartsWith("DEBAI:", StringComparison.OrdinalIgnoreCase))
                {
                    readingDeBai = true;
                    readingDapAn = false;
                    continue;
                }

                if (line.StartsWith("DAPAN:", StringComparison.OrdinalIgnoreCase))
                {
                    readingDeBai = false;
                    readingDapAn = true;
                    continue;
                }

                if (line.Equals("#END", StringComparison.OrdinalIgnoreCase))
                {
                    var combined = string.Join(" ", currentDapAnLines);
                    var dapAn = combined.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(x => x.Trim())
                                        .ToList();

                    list.Add(new BaiTapDienTu(currentDeBai.Trim(), dapAn));
                    readingDeBai = false;
                    readingDapAn = false;
                    continue;
                }

                if (readingDeBai) currentDeBai += raw + Environment.NewLine;
                else if (readingDapAn) currentDapAnLines.Add(line);
            }

            return list;
        }
    }
}
