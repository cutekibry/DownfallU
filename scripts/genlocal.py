from __future__ import annotations

import argparse
import json
from pathlib import Path
from typing import Any


ROOT = Path(__file__).resolve().parents[1]
LOCALIZATION_DIR = ROOT / "DownfallU" / "localization"
NOTICE_KEY = "!"
NOTICE_VALUE = (
    "This is automatically generated using scripts. "
    "Edit the files in subdirectories instead."
)


class DuplicateKeyError(ValueError):
    pass


def load_json_object(path: Path) -> dict[str, Any]:
    with path.open("r", encoding="utf-8-sig") as file:
        data = json.load(file)

    if not isinstance(data, dict):
        raise ValueError(f"expected JSON object: {path}")
    return data


def write_json_object(path: Path, data: dict[str, Any]) -> None:
    new_text = json.dumps(data, ensure_ascii=False, indent=2) + "\n"
    path.write_text(new_text, encoding="utf-8")


def character_dirs(language_dir: Path) -> list[Path]:
    return sorted(path for path in language_dir.iterdir() if path.is_dir())


def entry_names(characters: list[Path]) -> list[str]:
    names: set[str] = set()
    for character_dir in characters:
        names.update(path.name for path in character_dir.glob("*.json"))
    return sorted(names)


def delete_generated_files(language_dir: Path) -> int:
    deleted = 0
    for path in sorted(language_dir.glob("*.json")):
        path.unlink()
        deleted += 1
        print(f"deleted: {path}")
    return deleted


def add_entries(
    merged: dict[str, Any],
    sources: dict[str, Path],
    data: dict[str, Any],
    source_path: Path,
) -> None:
    for key, value in data.items():
        if key in merged:
            first_source = sources[key]
            raise DuplicateKeyError(
                f"duplicate key {key!r} in {source_path}; first seen in {first_source}"
            )
        merged[key] = value
        sources[key] = source_path


def generated_language_files(language_dir: Path) -> dict[Path, dict[str, Any]]:
    characters = character_dirs(language_dir)
    generated: dict[Path, dict[str, Any]] = {}

    for entry_name in entry_names(characters):
        merged: dict[str, Any] = {NOTICE_KEY: NOTICE_VALUE}
        sources: dict[str, Path] = {NOTICE_KEY: Path("<generated notice>")}
        for character_dir in characters:
            source_path = character_dir / entry_name
            if source_path.is_file():
                add_entries(
                    merged,
                    sources,
                    load_json_object(source_path),
                    source_path,
                )

        generated[language_dir / entry_name] = merged

    return generated


def collect_generated_files(localization_dir: Path) -> dict[Path, dict[str, Any]]:
    generated: dict[Path, dict[str, Any]] = {}

    for language_dir in language_dirs(localization_dir):
        generated.update(generated_language_files(language_dir))

    return generated


def language_dirs(localization_dir: Path) -> list[Path]:
    return sorted(path for path in localization_dir.iterdir() if path.is_dir())


def main() -> None:
    parser = argparse.ArgumentParser(
        description="Generate merged DownfallU localization JSON files."
    )
    parser.add_argument(
        "--localization-dir",
        type=Path,
        default=LOCALIZATION_DIR,
        help=f"localization directory (default: {LOCALIZATION_DIR})",
    )
    args = parser.parse_args()

    localization_dir = args.localization_dir.resolve()
    if not localization_dir.is_dir():
        raise SystemExit(f"localization directory not found: {localization_dir}")

    try:
        generated = collect_generated_files(localization_dir)
    except DuplicateKeyError as error:
        raise SystemExit(str(error)) from error

    deleted = sum(
        delete_generated_files(language_dir) for language_dir in language_dirs(localization_dir)
    )
    for path, data in sorted(generated.items()):
        write_json_object(path, data)
        print(f"generated: {path}")

    print(f"deleted {deleted} and generated {len(generated)} localization JSON files")


if __name__ == "__main__":
    main()
