export function oxford(items: Array<string>, listSeparator: string = ",", listTerminator: string = "and")
{
  //special case, e.g. "A and B", which should not have commas
  if (items.length === 2) {
    return items.join(` ${listTerminator} `);
  }
  var lastItemIndex = items.length - 1;
  var commaSet = items.slice(0, lastItemIndex).map(i => `${i}${listSeparator}`);
  var lastItem = items.slice(lastItemIndex).map(i => `${listTerminator} ${i}`);
  return [...commaSet, ...lastItem].join(' ');
}
