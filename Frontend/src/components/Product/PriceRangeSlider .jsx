import { useState } from "react";

const PriceRangeSlider = () => {
  const [minValue, setMinValue] = useState(0);
  const [maxValue, setMaxValue] = useState(1000);

  const handleMinChange = (e) => {
    const value = Math.max(0, Math.min(Number(e.target.value), maxValue - 1));
    setMinValue(value);
  };

  const handleMaxChange = (e) => {
    const value = Math.min(
      1000,
      Math.max(Number(e.target.value), minValue + 1)
    );
    setMaxValue(value);
  };

  const handleMinSlide = (e) => {
    setMinValue(Math.min(Number(e.target.value), maxValue - 1));
  };

  const handleMaxSlide = (e) => {
    setMaxValue(Math.max(Number(e.target.value), minValue + 1));
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <div className="flex justify-between mb-4">
        <div className="flex items-center">
          <span className="mr-2">$</span>
          <input
            type="number"
            className="border p-2 w-24"
            value={minValue}
            onChange={handleMinChange}
            min="0"
            max="999"
          />
        </div>
        <div className="flex items-center">
          <span className="mr-2">$</span>
          <input
            type="number"
            className="border p-2 w-24"
            value={maxValue}
            onChange={handleMaxChange}
            min="1"
            max="1000"
          />
        </div>
      </div>
      <div className="relative w-full">
        <input
          type="range"
          min="0"
          max="1000"
          value={minValue}
          onChange={handleMinSlide}
          className="absolute pointer-events-none w-full h-2 opacity-0"
        />
        <input
          type="range"
          min="0"
          max="1000"
          value={maxValue}
          onChange={handleMaxSlide}
          className="absolute pointer-events-none w-full h-2 opacity-0"
        />
        <div className="relative z-10 h-2 bg-gray-300 rounded-full">
          <div
            className="absolute h-2 bg-orange-500 rounded-full"
            style={{
              left: `${(minValue / 1000) * 100}%`,
              right: `${100 - (maxValue / 1000) * 100}%`,
            }}
          ></div>
        </div>
      </div>
    </div>
  );
};

export default PriceRangeSlider;
