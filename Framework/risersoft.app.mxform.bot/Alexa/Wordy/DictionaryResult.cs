using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveServiceRsMx.Speech.Wordy
{
    public partial class DictionaryResult
    {
        public Metadata Metadata { get; set; }
        public Result[] Results { get; set; }
    }

    public class Metadata
    {
        public string Provider { get; set; }
    }

    public partial class Result
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public LexicalEntry[] LexicalEntries { get; set; }
        public Pronunciation[] Pronunciations { get; set; }
        public string Type { get; set; }
        public string Word { get; set; }
    }

    public partial class LexicalEntry
    {
        public Derivative[] DerivativeOf { get; set; }
        public Derivative[] Derivatives { get; set; }
        public Entry[] Entries { get; set; }
        public Note[] Notes { get; set; }
        public Pronunciation[] Pronunciations { get; set; }
        public VariantForm[] VariantForms { get; set; }
        public List<GrammaticalFeature> GrammaticalFeatures { get; set; }
        public List<InflectionOf> InflectionOf { get; set; }
        public string Language { get; set; }
        public LexicalCategory LexicalCategory { get; set; }
        public string Text { get; set; }
    }
    public partial class LexicalCategory
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
    public partial class Derivative
    {
        public string[] Domains { get; set; }
        public string Id { get; set; }
        public string Language { get; set; }
        public string[] Regions { get; set; }
        public string[] Registers { get; set; }
        public string Text { get; set; }
    }

    public partial class Entry
    {
        public string[] Etymologies { get; set; }
        public GrammaticalFeature[] GrammaticalFeatures { get; set; }
        public string HomographNumber { get; set; }
        public Note[] Notes { get; set; }
        public Pronunciation[] Pronunciations { get; set; }
        public Sense[] Senses { get; set; }
        public VariantForm[] VariantForms { get; set; }
    }

    public partial class GrammaticalFeature
    {
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public partial class Note
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public partial class Pronunciation
    {
        public string AudioFile { get; set; }
        public string[] Dialects { get; set; }
        public string PhoneticNotation { get; set; }
        public string PhoneticSpelling { get; set; }
        public string[] Regions { get; set; }
    }
    public partial class Sense
    {
        public List<Compound> Antonyms { get; set; }
        public List<VariantForm> Constructions { get; set; }
        public List<string> CrossReferenceMarkers { get; set; }
        public List<GrammaticalFeature> CrossReferences { get; set; }
        public List<string> Definitions { get; set; }
        public List<LexicalCategory> DomainClasses { get; set; }
        public List<LexicalCategory> Domains { get; set; }
        public List<string> Etymologies { get; set; }
        public List<Example> Examples { get; set; }
        public string Id { get; set; }
        public List<Inflection> Inflections { get; set; }
        public List<GrammaticalFeature> Notes { get; set; }
        public List<Pronunciation> Pronunciations { get; set; }
        public List<LexicalCategory> Regions { get; set; }
        public List<LexicalCategory> Registers { get; set; }
        public List<LexicalCategory> SemanticClasses { get; set; }
        public List<string> ShortDefinitions { get; set; }
        public List<Metadata> Subsenses { get; set; }
        public List<Compound> Synonyms { get; set; }
        public List<ThesaurusLink> ThesaurusLinks { get; set; }
        public List<VariantForm> VariantForms { get; set; }
        public Translation[] Translations { get; set; }
    }
    public partial class Compound
    {
        public List<LexicalCategory> Domains { get; set; }
        public string Id { get; set; }
        public string Language { get; set; }
        public List<LexicalCategory> Regions { get; set; }
        public List<LexicalCategory> Registers { get; set; }
        public string Text { get; set; }
    }
    public partial class ThesaurusLink
    {
        public string EntryId { get; set; }
        public string SenseId { get; set; }
    }

    public partial class Example
    {
        public string[] Definitions { get; set; }
        public string[] Domains { get; set; }
        public Note[] Notes { get; set; }
        public string[] Regions { get; set; }
        public string[] Registers { get; set; }
        public string[] SenseIds { get; set; }
        public string Text { get; set; }
        public Translation[] Translations { get; set; }
    }

    public partial class Translation
    {
        public string[] Domains { get; set; }
        public GrammaticalFeature[] GrammaticalFeatures { get; set; }
        public string Language { get; set; }
        public Note[] Notes { get; set; }
        public string[] Regions { get; set; }
        public string[] Registers { get; set; }
        public string Text { get; set; }
    }

    public partial class VariantForm
    {
        public string[] Regions { get; set; }
        public string Text { get; set; }
    }
    public class InflectionOf
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
    public partial class Inflection
    {
        public List<LexicalCategory> Domains { get; set; }
        public List<GrammaticalFeature> GrammaticalFeatures { get; set; }
        public string InflectedForm { get; set; }
        public LexicalCategory LexicalCategory { get; set; }
        public List<Pronunciation> Pronunciations { get; set; }
        public List<LexicalCategory> Regions { get; set; }
        public List<LexicalCategory> Registers { get; set; }
    }
}