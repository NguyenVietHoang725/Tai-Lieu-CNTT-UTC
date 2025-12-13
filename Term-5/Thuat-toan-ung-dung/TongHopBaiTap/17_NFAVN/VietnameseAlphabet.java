/**
 * Lớp {@code VietnameseAlphabet} cung cấp một kiểu dữ liệu cho bảng chữ cái tiếng Việt,
 * kế thừa từ lớp {@code Alphabet}.
 * Các ký tự chữ cái có dấu được sắp xếp theo quy tắc bảng chữ cái tiếng Việt.
 * Các ký tự còn lại (whitespace, dấu câu, chữ số, ký hiệu) được sắp xếp theo thứ tự mã ASCII
 * (cho ký tự ASCII) và mã Unicode (cho ký tự ngoài ASCII).
 */
public class VietnameseAlphabet extends Alphabet {

    public static final VietnameseAlphabet VIETNAMESE_ALPHABET = new VietnameseAlphabet();

    public VietnameseAlphabet() {
        super(
            // ===== Whitespace (ASCII và NBSP) =====
            "\t\n\r \u00A0" + // tab (0x09), newline (0x0A), carriage return (0x0D), space (0x20), NBSP (0x00A0)

            // ===== Dấu câu và ký hiệu ASCII (theo thứ tự mã ASCII) =====
            "!\"#$%&'()*+,-./:;<=>?@[]^_`{|}~\\" + // từ 0x21 đến 0x7E, thêm /

            // ===== Chữ số (ASCII) =====
            "0123456789" + // từ 0x30 đến 0x39

            // ===== Ký hiệu Unicode (sắp xếp theo mã Unicode tăng dần) =====
            "\u00AB\u00BB" + // « » (guillemets, 0x00AB, 0x00BB)
            "\u2039\u203A" + // ‹ › (single guillemets, 0x2039, 0x203A)
            "\u2013\u2014\u2015" + // en dash (0x2013), em dash (0x2014), horizontal bar (0x2015)
            "\u2018\u2019\u201A" + // ‘ ’ ‚ (single quotes, 0x2018, 0x2019, 0x201A)
            "\u201C\u201D\u201E" + // “ ” „ (double quotes, 0x201C, 0x201D, 0x201E)
            "\u2026" + // … (ellipsis, 0x2026)
            "\u22EF" + // ⋯ (midline ellipsis, 0x22EF)
            "\u00A9\u00AE\u00B0" + // © (0x00A9), ® (0x00AE), ° (0x00B0)
            "\u00A3\u00A5\u20AC\u20AB" + // £ (0x00A3), ¥ (0x00A5), € (0x20AC), ₫ (0x20AB)
            "\u00B1\u00D7\u00F7\u2030" + // ± (0x00B1), × (0x00D7), ÷ (0x00F7), ‰ (0x2030)
            "\u00A7" + // § (0x00A7)

            // ===== Dấu thanh rời (combining diacritics) =====
            "\u0300\u0301\u0303\u0309\u0323" + // huyền, sắc, ngã, hỏi, nặng

            // ===== Chữ cái tiếng Việt (theo quy tắc bảng chữ cái tiếng Việt) =====
            "AÀÁẢÃẠĂẰẮẲẴẶÂẦẤẨẪẬ" + "aàáảãạăằắẳẵặâầấẩẫậ" +
            "B" + "b" +
            "C" + "c" +
            "DĐ" + "dđ" +
            "EÈÉẺẼẸÊỀẾỂỄỆ" + "eèéẻẽẹêềếểễệ" +
            "G" + "g" +
            "H" + "h" +
            "IÌÍỈĨỊ" + "iìíỉĩị" +
            "K" + "k" +
            "L" + "l" +
            "M" + "m" +
            "N" + "n" +
            "OÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢ" + "oòóỏõọôồốổỗộơờớởỡợ" +
            "P" + "p" +
            "Q" + "q" +
            "R" + "r" +
            "S" + "s" +
            "T" + "t" +
            "UÙÚỦŨỤƯỪỨỬỮỰ" + "uùúủũụưừứửữự" +
            "V" + "v" +
            "X" + "x" +
            "YỲÝỶỸỴ" + "yỳýỷỹỵ" +
            // Chữ cái Latin ngoài bảng TV, để phòng tên riêng
            "FfJjWwZz"
        );
    }
}