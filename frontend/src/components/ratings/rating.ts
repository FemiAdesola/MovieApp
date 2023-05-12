export interface RatingsProps {
  maximumValue: number;
  selectedValue: number;
  onChange(rate: number): void;
}