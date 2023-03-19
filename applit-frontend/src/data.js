const data = [
    {
      name: 'Folder 1',
      type: 'folder',
      children: [
        {
          name: 'Subfolder 1',
          type: 'folder',
          children: [
            {
              name: 'File 1',
              type: 'file',
              size: '10 KB',
            },
            {
              name: 'File 2',
              type: 'file',
              size: '20 KB',
            },
          ],
        },
        {
          name: 'Subfolder 2',
          type: 'folder',
          children: [
            {
              name: 'File 3',
              type: 'file',
              size: '30 KB',
            },
          ],
        },
      ],
    },
    {
      name: 'Folder 2',
      type: 'folder',
      children: [
        {
          name: 'File 4',
          type: 'file',
          size: '15 KB',
        },
        {
          name: 'File 5',
          type: 'file',
          size: '25 KB',
        },
      ],
    },
  ];
  
  export default data;
  