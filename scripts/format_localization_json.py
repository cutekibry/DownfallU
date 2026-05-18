from __future__ import annotations

import argparse
import json
from pathlib import Path
from typing import Any


ROOT = Path(__file__).resolve().parents[1]
LOCALIZATION_DIR = ROOT / "DownfallU" / "localization"


def formatted_json(data: Any) -> str:
    return json.dumps(data, ensure_ascii=False, indent=2, sort_keys=True) + "\n"


def format_file(path: Path, *, check: bool) -> bool:
    with path.open("r", encoding="utf-8-sig") as file:
        data = json.load(file)

    old_text = path.read_text(encoding="utf-8-sig")
    new_text = formatted_json(data)
    if old_text == new_text:
        return False

    if check:
        print(f"would format: {path}")
    else:
        path.write_text(new_text, encoding="utf-8")
        print(f"formatted: {path}")
    return True


def json_files(root: Path) -> list[Path]:
    return sorted(root.rglob("*.json"))


def main() -> None:
    parser = argparse.ArgumentParser(
        description="Format DownfallU localization JSON files."
    )
    parser.add_argument(
        "--localization-dir",
        type=Path,
        default=LOCALIZATION_DIR,
        help=f"localization directory (default: {LOCALIZATION_DIR})",
    )
    parser.add_argument(
        "--check",
        action="store_true",
        help="report files that would be formatted without writing changes",
    )
    args = parser.parse_args()

    localization_dir = args.localization_dir.resolve()
    if not localization_dir.is_dir():
        raise SystemExit(f"localization directory not found: {localization_dir}")

    files = json_files(localization_dir)
    changed = sum(format_file(path, check=args.check) for path in files)

    action = "would format" if args.check else "formatted"
    print(f"{action} {changed} of {len(files)} JSON files")

    if args.check and changed:
        raise SystemExit(1)


if __name__ == "__main__":
    main()
