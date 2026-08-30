import { useState } from 'react';

const Calculator = () => {
  const [a, setA] = useState('0');
  const [op, setOp] = useState('+');
  const [b, setB] = useState('0');
  const [ans, setAns] = useState(0);

  function handleSubmit() {
    setAns(eval(a + op + b));
  }

  return (
    <>
      <h1 className="font-bold mb-4">Simple Calculator</h1>

      <form
        className="*:text-center"
        onSubmit={(e) => {
          e.preventDefault();
          handleSubmit();
        }}
      >
        <input
          name="a"
          type="number"
          className="w-20 border"
          value={a}
          onChange={(e) => {
            setA(e.target.value);
          }}
        />
        <select
          name="op"
          className="border border-gray-500 mx-4 bg-gray-100 px-4"
          value={op}
          onChange={(e) => {
            setOp(e.target.value);
          }}
        >
          <option value="+">+</option>
          <option value="-">-</option>
          <option value="*">*</option>
        </select>
        <input
          name="b"
          type="number"
          className="w-20 border"
          value={b}
          onChange={(e) => setB(e.target.value)}
        />
        <br />
        <div className="font-semibold text-lg pt-4">Result: {ans}</div>
        <button
          type="submit"
          className="text-white bg-green-800 px-4 py-2 rounded m-4 font-bold"
        >
          Calculate
        </button>
      </form>
    </>
  );
};

export default Calculator;
